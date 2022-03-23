using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_2_Battle_P1 : MonoBehaviour
{
	public GameObject FlameGen;
	public Waypoint[] wayPoints;
	public float speed = 3f;
	public bool inReverse = true;
	public AudioSource sfx;

	public UnityEvent ActionOnWaypoint;

	private Vector3 prevPos;
	private Rigidbody2D myRigidbody;
	private Animator anim;
	private Waypoint currentWaypoint;
	private int currentIndex = 0;
	private bool isWaiting = false;
	private bool trigger = false;
	void Start()
	{
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		if (wayPoints.Length > 0)
		{
			currentWaypoint = wayPoints[0];
		}
		anim.SetBool("Sitting", true);
	}
	private void OnEnable()
	{
		FlameGen.SetActive(true);
	}

	private void OnDisable()
	{
		FlameGen.SetActive(false);
	}

	void FixedUpdate()
	{
		if (currentWaypoint != null && !isWaiting)
		{
			MoveTowardsWaypoint();
			/*float xvelocity = (transform.position.x - prevPos.x);
			float yvelocity = (transform.position.y - prevPos.y);
			myRigidbody.velocity = new Vector2(xvelocity, yvelocity);
			if (anim.GetFloat("Move Y") != 0 && anim.GetFloat("Move Y") != 0)
				UpdateAnimation();
			prevPos = transform.position;*/
		}
		if (trigger)
        {
			ActionOnWaypoint.Invoke();
			trigger = false;
		}
	}

	public void Pause()
	{
		isWaiting = !isWaiting;
	}

	private void MoveTowardsWaypoint()
	{
		Vector3 currentPosition = this.transform.position;

		Vector3 targetPosition = currentWaypoint.transform.position;

		if (Vector3.Distance(currentPosition, targetPosition) > .1f)
		{
			transform.position = targetPosition;
		}
		else
		{
			anim.SetTrigger("Attack");
			if (sfx)
				sfx.Play();
			if (currentWaypoint.waitSeconds > 0)
			{
				Pause();
				Invoke("Pause", currentWaypoint.waitSeconds);
			}
			NextWaypoint();
		}
	}

	private void NextWaypoint()
	{
		if ((!inReverse && currentIndex + 1 >= wayPoints.Length) || (inReverse && currentIndex == 0))
		{
			inReverse = !inReverse;
		}
		currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;
		currentWaypoint = wayPoints[currentIndex];
	}

	public void TriggerAction()
    {
		trigger = true;
	}
	void UpdateAnimation()
	{
		anim.SetBool("Moving", true);
		anim.SetFloat("Move X", myRigidbody.velocity.x);
		anim.SetFloat("Move Y", myRigidbody.velocity.y);
	}
}
