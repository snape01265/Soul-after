using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorv2 : MonoBehaviour
{
    public float speed;

    private Vector3 startPos;
    private Vector3 targetPos;
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity + new Vector3((startPos.x - targetPos.x) * speed, 0, 0);
        transform.rotation = LookAt2D(targetPos - transform.position);

    }
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
