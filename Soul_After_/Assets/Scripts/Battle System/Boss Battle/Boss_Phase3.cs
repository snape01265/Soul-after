using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase3 : MonoBehaviour
{
	public Waypoint[] waypoints;
	public float speed;
	public float voidCD;
	[HideInInspector]
	public Boss boss;
	[HideInInspector]
	public Transform firePoint;
	[HideInInspector]
	public GameObject voidPrefab;
	[HideInInspector]
	public GameObject bossObject;
	[HideInInspector]
	public bool isCooldown = false;

	private bool inReverse = true;
	private Vector3 bossPosition;
	private Waypoint currentWaypoint;
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

	void FixedUpdate()
	{
		if (boss.cooldown)
		{
			bossPosition = Vector3.MoveTowards(bossPosition, currentWaypoint.transform.position, speed * Time.deltaTime);
			bossObject.transform.position = bossPosition;
			CheckWaypoint();
		}
		else if (!isCooldown && !boss.cooldown)
		{
			StartCoroutine(VoidAttack(voidCD));
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
	IEnumerator VoidAttack(float cd)
	{
		isCooldown = true;
		FireVoid();
		yield return new WaitForSeconds(cd);
		boss.cooldown = false;
	}
	public void FireVoid()
	{
		Instantiate(voidPrefab, firePoint.position, firePoint.rotation);
	}

}
