using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Phase1 : MonoBehaviour
{
    public Waypoint[] waypoints;
	public int meteorNumber;
    public float speed = 1f;
	public float meteorCD;
	public float stunDuration;
	[HideInInspector]
	public Boss boss;
	[HideInInspector]
	public Transform firePoint;
	[HideInInspector]
	public GameObject meteorPrefab;
	[HideInInspector]
	public GameObject bossObject;
	[HideInInspector]
	public int meteorCount;

	private bool inReverse = true;
	private Vector3 bossPosition;
    private Waypoint currentWaypoint;
	private bool isWaiting;
	private bool isCooldown;
	private int currentIndex = 0;

	void Start()
    {
		bossPosition = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().transform.position;
		boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
		if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0];
        }
	}

    private void OnEnable()
    {
		isWaiting = false;
		isCooldown = false;
    }
    void FixedUpdate()
    {
        if (!isWaiting)
        {
			bossPosition = Vector3.MoveTowards(bossPosition, currentWaypoint.transform.position, speed * Time.deltaTime);
			bossObject.transform.position = bossPosition;
			CheckWaypoint();
        }
		if (!isWaiting && (meteorCount >= meteorNumber))
        {
			StartCoroutine(StopMovement(stunDuration));
        }
		if(!isWaiting && !isCooldown)
        {
			StartCoroutine(MeteorAttack(meteorCD));
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
	IEnumerator StopMovement(float duration)
    {
		isWaiting = true;
		boss.cooldown = true;
		meteorCount = 0;
		isCooldown = false;
		yield return new WaitForSeconds(duration);
		isWaiting = false;
		boss.cooldown = false;
	}
	IEnumerator MeteorAttack(float cd)
    {
		FireMeteor();
		isCooldown = true;
		yield return new WaitForSeconds(cd);
		isCooldown = false;
	}
	public void FireMeteor()
	{
		Instantiate(meteorPrefab, firePoint.position, firePoint.rotation);
	}
}
