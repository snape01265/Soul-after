using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_Phase_2_P1 : MonoBehaviour
{
	public GameObject FlameGen;
	public Waypoint[] wayPoints;
	public float speed = 6f;
	public bool inReverse = true;

	public UnityEvent ActionOnWaypoint;

	private Vector3 prevPos;
	private Rigidbody2D myRigidbody;
	private Animator bossAnim;
	private Waypoint currentWaypoint;
	private int currentIndex = 0;
	private bool isWaiting = false;
	private float speedStorage = 0;

	void Start()
	{
		bossAnim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		if (wayPoints.Length > 0)
		{
			currentWaypoint = wayPoints[0];
		}
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
			float xvelocity = (transform.position.x - prevPos.x);
			float yvelocity = (transform.position.y - prevPos.y);
			myRigidbody.velocity = new Vector2(xvelocity, yvelocity);
			if (bossAnim.GetFloat("Move Y") != 0 && bossAnim.GetFloat("Move Y") != 0)
				UpdateAnimation();
			prevPos = transform.position;
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

			Vector3 directionOfTravel = targetPosition - currentPosition;
			directionOfTravel.Normalize();

			this.transform.Translate(
				directionOfTravel.x * speed * Time.deltaTime,
				directionOfTravel.y * speed * Time.deltaTime,
				directionOfTravel.z * speed * Time.deltaTime,
				Space.World
			);
		}
		else
		{
			bossAnim.SetTrigger("Attack");
			ActionOnWaypoint.Invoke();
			if (currentWaypoint.waitSeconds > 0)
			{
				Pause();
				Invoke("Pause", currentWaypoint.waitSeconds);
			}

			if (currentWaypoint.speedOut > 0)
			{
				speedStorage = speed;
				speed = currentWaypoint.speedOut;
			}
			else if (speedStorage != 0)
			{
				speed = speedStorage;
				speedStorage = 0;
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

	void UpdateAnimation()
	{
		bossAnim.SetFloat("Move X", myRigidbody.velocity.x);
		bossAnim.SetFloat("Move Y", myRigidbody.velocity.y);
	}
}
