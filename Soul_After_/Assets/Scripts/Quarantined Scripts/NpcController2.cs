using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController2 : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D myRigidBody;
    private Animator anim;
    private bool up;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingUp())
        {
            myRigidBody.velocity = new Vector2(0f, moveSpeed);
        }
        else
        {
            myRigidBody.velocity = new Vector2(0f, -moveSpeed);
        }
        bool IsFacingUp()
        {
            return up;
        }
        UpdateAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (up == true)
            up = false;
        else
            up = true;
    }
    private void UpdateAnimation()
    {
        anim.SetFloat("Move Y", myRigidBody.velocity.y);
    }
}
