using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
	public GameObject wayPoint;
	public float speed = 10;
	public GameObject voidAttack;

	private Boss_Phase3 phase3;
	private Boss boss;
	private Vector3 targetPos;

	void Start()
	{
		boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
		phase3 = GameObject.Find("Boss_Phase3").GetComponent<Boss_Phase3>();
		targetPos = wayPoint.transform.position;
	}

	void FixedUpdate()
	{
		Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

		transform.rotation = LookAt2D(nextPos - transform.position);
		transform.position = nextPos;

		if (nextPos == targetPos)
		{
			Arrived();
		}
	}

	void Arrived()
	{
		boss.cooldown = true;
		phase3.isCooldown = false;
		Destroy(voidAttack);
	}

	static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0, 0, (Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg) - 90);
	}
}
