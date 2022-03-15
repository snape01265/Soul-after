using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Phase1 : MonoBehaviour
{
    public Waypoint[] waypoints;
    public float speed = 1f;
	public int meteorCD;
	public int patternCD;
	public int stunDuration;
	public Boss boss;

	private bool inReverse = true;
	private Vector3 bossPosition;
    private Waypoint currentWaypoint;
	private bool isWaiting = false;
	private bool isCooldown = false;
	private bool isDamaged = false;
	private int currentIndex = 0;

	void Start()
    {
		bossPosition = transform.position;
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0];
        }
		StartCoroutine(StopMovement(patternCD));
	}

	void Update()
    {
        if (!isWaiting)
        {
			bossPosition = Vector3.MoveTowards(bossPosition, currentWaypoint.transform.position, speed * Time.deltaTime);
			transform.position = bossPosition;
			CheckWaypoint();
        }
		else if (isWaiting)
        {
			isDamaged = true;
			StartCoroutine(StopMovement(patternCD));
        }
		if(!isWaiting && !isCooldown)
        {
			StartCoroutine(ShootMeteor(meteorCD));
        }
    }
	private void CheckWaypoint()
	{
		Vector3 currentPosition = bossPosition;
		Vector3 targetPosition = currentWaypoint.transform.position;

		if (Vector3.Distance(currentPosition, targetPosition) <= 0.1f)
        {
			NextWaypoint();
        }
	}

	private void NextWaypoint()
	{
		if ((!inReverse && currentIndex + 1 >= waypoints.Length) || (inReverse && currentIndex == 0))
		{
			inReverse = !inReverse;
		}
		currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;
		currentWaypoint = waypoints[currentIndex];
	}
	public void Pause()
	{
		isWaiting = !isWaiting;
	}
	IEnumerator StopMovement(int duration)
    {
		yield return new WaitForSeconds(stunDuration);
		isWaiting = false;
		yield return new WaitForSeconds(duration);
		isWaiting = true;
	}
	IEnumerator ShootMeteor(int cd)
    {
		boss.FireMeteor();
		isCooldown = true;
		yield return new WaitForSeconds(cd);
		isCooldown = false;
    }
}
