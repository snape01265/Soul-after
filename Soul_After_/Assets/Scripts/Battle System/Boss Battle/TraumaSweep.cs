using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaSweep : MonoBehaviour
{
	public Vector3 targetPos;
	public float speed = 10;

	private Boss_Phase2 phase2;
	private Boss boss;

	void Start()
	{
		boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
		phase2 = GameObject.Find("Boss_Phase2").GetComponent<Boss_Phase2>();
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
		phase2.isCooldown = false;
		boss.cooldown = true;
		Destroy(gameObject);
	}

	static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg + 90);
	}
}
