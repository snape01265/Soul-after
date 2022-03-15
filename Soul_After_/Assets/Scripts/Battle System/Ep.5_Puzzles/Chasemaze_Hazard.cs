using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasemaze_Hazard : MonoBehaviour
{
    public float HazardSpeed;
    public float ChaseEndXAxis = 40f;

    public bool isMoving = false;
    private Rigidbody2D Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Rigidbody.velocity = new Vector2(HazardSpeed, 0);
        } 
        else if (isMoving && transform.position.x < ChaseEndXAxis)
        {
            Rigidbody.velocity = Vector2.zero;
        }
        else
        {
            Rigidbody.velocity = Vector2.zero;
        }
    }
}
