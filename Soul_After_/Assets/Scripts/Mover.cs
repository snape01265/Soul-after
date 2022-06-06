using UnityEngine;
using System.Collections;
//NPC를 움직이게 하는 스크립트
public class Mover : MonoBehaviour
{
	public Waypoint[] wayPoints;
	public float speed = 3f;
	public bool isCircular;
	public bool isOneWay = false;
	public bool inReverse = true;
	public bool isWaiting = false;
	public AudioSource OneWaySFX;

	private Vector3 prevPos;
	private Rigidbody2D myRigidbody;
	private Animator anim;
	private Waypoint currentWaypoint;
	private int currentIndex = 0;

	private float speedStorage = 0;

	void Start()
	{
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		if (wayPoints.Length > 0)
		{
			currentWaypoint = wayPoints[0];
		}
	}

	void FixedUpdate()
	{
		if (currentWaypoint != null && !isWaiting)
		{
			MoveTowardsWaypoint();
			float xvelocity = (transform.position.x - prevPos.x);
			float yvelocity = (transform.position.y - prevPos.y);
			myRigidbody.velocity = new Vector2(xvelocity, yvelocity);
			UpdateAnimation();
			prevPos = transform.position;
		}
        else
        {		
			StopMovement();
        }
	}
	private void StopMovement()
    {
		myRigidbody.position = new Vector2(transform.position.x, transform.position.y);
		myRigidbody.velocity = new Vector2(0, 0);
		UpdateAnimation();
	}

	public void Pause()
	{
		isWaiting = true;
	}

	public void Unpause()
    {
		isWaiting = false;
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
			if (currentWaypoint.waitSeconds > 0)
			{
				Pause();
				Invoke("Unpause", currentWaypoint.waitSeconds);
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
		if (isOneWay)
        {
			currentIndex = (currentIndex + 1 >= wayPoints.Length) ? 0 : currentIndex + 1;
			if (currentIndex == 0)
            {
				if (OneWaySFX)
					OneWaySFX.Play();
				gameObject.transform.position = wayPoints[0].transform.position;
				currentWaypoint = wayPoints[1];
				return;
            }
		}
		else if (isCircular)
		{
			if (!inReverse)
			{
				currentIndex = (currentIndex + 1 >= wayPoints.Length) ? 0 : currentIndex + 1;
			}
			else
			{
				currentIndex = (currentIndex == 0) ? wayPoints.Length - 1 : currentIndex - 1;
			}
		}
		else
		{
			if ((!inReverse && currentIndex + 1 >= wayPoints.Length) || (inReverse && currentIndex == 0))
			{
				inReverse = !inReverse;
			}
			currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;
		}
		currentWaypoint = wayPoints[currentIndex];
	}
    void UpdateAnimation()
	{
		if (!isWaiting)
        {
			anim.SetBool("Moving", true);
			anim.SetFloat("Move_X", myRigidbody.velocity.x);
			anim.SetFloat("Move_Y", myRigidbody.velocity.y);
		}
        else
        {
			anim.SetBool("Moving", false);
			anim.SetFloat("Move_X", myRigidbody.velocity.x);
			anim.SetFloat("Move_Y", myRigidbody.velocity.y);
		}
		
	}
}