using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flameshot : MonoBehaviour
{
	public GameObject Boss;
	public Waypoint[] wayPoints;
	public float speed = 3f;

	private SpriteRenderer sprite;
	private CircleCollider2D collider;
	private Vector3 prevPos;
	private Rigidbody2D myRigidbody;
	private Animator anim;
	private Light2D light;
	private Waypoint currentWaypoint;
	private bool isWaiting = false;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		collider = GetComponent<CircleCollider2D>();
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		light = transform.Find("Point Light 2D").GetComponent<Light2D>();

		sprite.enabled = false;
		collider.enabled = false;
		light.enabled = false;
	}

	void FixedUpdate()
	{
		if (currentWaypoint != null && !isWaiting)
		{
			MoveTowardsWaypoint();
			float xvelocity = (transform.position.x - prevPos.x);
			float yvelocity = (transform.position.y - prevPos.y);
			myRigidbody.velocity = new Vector2(xvelocity, yvelocity);
			if (anim.GetFloat("Move Y") != 0 && anim.GetFloat("Move Y") != 0)
				UpdateAnimation();
			prevPos = transform.position;
		}
	}

	public void FireFlames()
    {
		transform.position = Boss.transform.position;
		sprite.enabled = true;
		collider.enabled = true;
		light.enabled = true;
		currentWaypoint = wayPoints[0];
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
			sprite.enabled = false;
			collider.enabled = false;
			light.enabled = false;
			isWaiting = true;
		}
	}

	void UpdateAnimation()
	{
		anim.SetFloat("Move X", myRigidbody.velocity.x);
		anim.SetFloat("Move Y", myRigidbody.velocity.y);
	}
}
