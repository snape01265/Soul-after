using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Phase1 : StateMachineBehaviour
{
    public Waypoint[] wayPoints;
    public float speed = 1f;
	public bool inReverse = true;
	public UnityEvent ActionOnWaypoint;

	private BossHP bossHP;
	private Rigidbody2D myRigidbody;
	private Transform transform;
    private Waypoint currentWaypoint;
	private Vector3 prevPos;
	private bool isWaiting = false;
	private float speedStorage = 0;
	private int currentIndex = 0;
	private Animator anim;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myRigidbody = animator.GetComponent<Rigidbody2D>();
		transform = animator.GetComponent<Transform>();
		anim = animator.GetComponent<Animator>();
		bossHP = animator.GetComponent<BossHP>();
        if (wayPoints.Length > 0)
        {
            currentWaypoint = wayPoints[0];
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (currentWaypoint != null && !isWaiting)
        {
            MoveTowardsWaypoint();
            float xvelocity = (transform.position.x - prevPos.x);
            float yvelocity = (transform.position.y - prevPos.y);
            myRigidbody.velocity = new Vector2(xvelocity, yvelocity);
            prevPos = transform.position;
        }
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
			ActionOnWaypoint.Invoke();
			if (currentWaypoint.waitSeconds > 0)
			{
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
	public void Pause()
	{
		isWaiting = !isWaiting;
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		if(bossHP)
        {
			anim.SetInteger("Phase", 2);
		}
	}
}
