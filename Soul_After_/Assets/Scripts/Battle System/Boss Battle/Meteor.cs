using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
	public float speed;
	public float arcHeight = 1;
	public GameObject shadow;

	private Vector3 startPos;
	private Vector3 targetPos;
	private Boss_Phase1 phase1;

	void Start()
	{
		startPos = transform.position;
		Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		targetPos = new Vector3(Random.Range(playerPos.x - 2, playerPos.x + 2), Random.Range(playerPos.y - 2, playerPos.y + 2), playerPos.z);
		speed = speed * Mathf.Abs(startPos.x - targetPos.x);
	}
    void FixedUpdate()
	{
		shadow.transform.position = targetPos;
		float x0 = startPos.x;
		float x1 = targetPos.x;
		float dist = x1 - x0;
		float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
		float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
		float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
		Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

		transform.rotation = LookAt2D(nextPos - transform.position);
		transform.position = nextPos;
		if (nextPos == targetPos)
		{
			Arrived();
		}
	}
    private void OnEnable()
    {
		phase1 = GameObject.Find("Boss_Phase1").GetComponent<Boss_Phase1>();
		phase1.meteorCount += 1;
	}
	void Arrived()
	{
		Destroy(gameObject);
	}

	static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	}
}
