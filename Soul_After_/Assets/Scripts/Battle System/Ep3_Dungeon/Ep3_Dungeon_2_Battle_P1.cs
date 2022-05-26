using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_2_Battle_P1 : MonoBehaviour
{
	public GameObject FlameGen;
	public Waypoint[] wayPoints;
	public bool inReverse = true;
	public UnityEvent ActionOnWaypoint;
	public AudioSource sfx;

	private Animator anim;
	private Waypoint currentWaypoint;
	private int currentIndex = 0;
	private bool isWaiting = false;
	private bool trigger = false;
	void Start()
	{
		anim = GetComponent<Animator>();
		if (wayPoints.Length > 0)
		{
			currentWaypoint = wayPoints[0];
		}
		anim.SetBool("Sitting", true);
		anim.SetTrigger("Disappear");
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
		if (trigger)
        {
			if (sfx)
				sfx.Play();
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

		transform.position = targetPosition;
		anim.SetTrigger("Attack");
		NextWaypoint();
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
	public void ResetAnimation()
    {
		anim.SetBool("Moving", false);
		anim.SetBool("Sitting", false);
    }
}
