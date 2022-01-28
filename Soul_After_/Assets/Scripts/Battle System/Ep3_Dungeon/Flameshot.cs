using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flameshot : MonoBehaviour
{
	public GameObject Boss;
	public Waypoint[] wayPoints;
	public float speed = 3f;
	public int damage;
	public PlayerHealth playerHealth;

	private SpriteRenderer sprite;
	private CircleCollider2D circleCollider;
	private Vector3 prevPos;
	private Rigidbody2D myRigidbody;
	private Animator anim;
	private new Light2D light;
	private Waypoint currentWaypoint;
	private bool isWaiting = false;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		circleCollider = GetComponent<CircleCollider2D>();
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		light = transform.Find("Point Light 2D").GetComponent<Light2D>();

		sprite.enabled = false;
		circleCollider.enabled = false;
		light.enabled = false;

		FireFlamesDuration(1f);
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
		circleCollider.enabled = true;
		light.enabled = true;
		currentWaypoint = wayPoints[0];
		isWaiting = false;
	}

	public void FireFlamesDuration(float time)
    {
		IEnumerator FlipAfterDuration(float time)
		{
			yield return new WaitForSeconds(time);
			isWaiting = true;
			sprite.enabled = false;
			circleCollider.enabled = false;
			light.enabled = false;
		}

		transform.position = Boss.transform.position;
		sprite.enabled = true;
		circleCollider.enabled = true;
		light.enabled = true;
		currentWaypoint = wayPoints[0];
		isWaiting = false;

		StartCoroutine(FlipAfterDuration(time));
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
			circleCollider.enabled = false;
			light.enabled = false;
			isWaiting = true;
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
			sprite.enabled = false;
			circleCollider.enabled = false;
			light.enabled = false;
			isWaiting = true;
		}
		else if (other.gameObject.CompareTag("Player"))
        {
			playerHealth.TakeDamage(damage);
        }
    }
    void UpdateAnimation()
	{
		anim.SetFloat("Move X", myRigidbody.velocity.x);
		anim.SetFloat("Move Y", myRigidbody.velocity.y);
	}


}
