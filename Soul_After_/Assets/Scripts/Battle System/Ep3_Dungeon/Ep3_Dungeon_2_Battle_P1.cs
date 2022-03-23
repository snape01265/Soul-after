using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_2_Battle_P1 : MonoBehaviour
{
	public GameObject FlameGen;
	public Waypoint[] wayPoints;
	public bool inReverse = true;
	public AudioSource sfx;

	public UnityEvent ActionOnWaypoint;

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
}
