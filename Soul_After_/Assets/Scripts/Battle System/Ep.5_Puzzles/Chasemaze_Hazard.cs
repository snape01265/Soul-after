using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasemaze_Hazard : MonoBehaviour
{
    public Transform Hazard;
    public float HazardSpeed;

    [HideInInspector]
    public bool isMoving = false;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rigidbody.velocity = new Vector2(HazardSpeed, 0);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
