using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasemaze_Hazard : MonoBehaviour
{
    public float HazardSpeed;

    [HideInInspector]
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
        else
        {
            Rigidbody.velocity = Vector2.zero;
        }
    }
}
