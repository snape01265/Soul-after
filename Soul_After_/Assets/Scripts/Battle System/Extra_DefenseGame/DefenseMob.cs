using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMob : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.localScale += Vector3.right * speed;
    }
}
