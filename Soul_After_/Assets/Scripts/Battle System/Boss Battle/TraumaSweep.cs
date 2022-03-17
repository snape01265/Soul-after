using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaSweep : MonoBehaviour
{
	public Vector3 targetPos;
	public float speed = 10;

	private Boss boss;
	private Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
		boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
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
		Destroy(gameObject);
	}

	static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	}
}
