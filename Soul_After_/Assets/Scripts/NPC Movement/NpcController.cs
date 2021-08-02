using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//특정 바운더리에서 NPC를 움직이게 하는 스크립트
public class NpcController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public bool isWalking;
    public float walkTime;
    public float waitTime;
    public bool canMove;
    public Collider2D walkZone;

    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private bool hasWalkZone;
    private Rigidbody2D myRigidbody;
    private float walkCounter;
    private int WalkDirection;
    private float waitCounter;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDirection();
        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
    }

    void Update()
    {
        if (canMove)
        {
            if (isWalking)
            {
                walkCounter -= Time.deltaTime;
                switch (WalkDirection)
                {
                    case 0:
                        if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                            anim.SetBool("Moving", false);
                        }
                        else
                        {
                            myRigidbody.velocity = new Vector2(0, moveSpeed);
                        }
                        break;
                    case 1:
                        if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                            anim.SetBool("Moving", false);
                        }
                        else
                        {
                            myRigidbody.velocity = new Vector2(moveSpeed, 0);
                        }
                        break;
                    case 2:
                        if (hasWalkZone && transform.position.y < maxWalkPoint.y)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                            anim.SetBool("Moving", false);
                        }
                        else
                        {
                            myRigidbody.velocity = new Vector2(0, -moveSpeed);
                        }
                        break;
                    case 3:
                        if (hasWalkZone && transform.position.x < maxWalkPoint.x)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                            anim.SetBool("Moving", false);
                        }
                        else
                        {
                            myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                        }
                        break;
                }
                UpdateAnimation();
                if (walkCounter < 0)
                {
                    isWalking = false;
                    waitCounter = waitTime;
                    anim.SetBool("Moving", false);
                }
            }
            else
            {
                anim.SetBool("Moving", false);
                waitCounter -= Time.deltaTime;
                myRigidbody.velocity = Vector2.zero;
                if (waitCounter < 0)
                {
                    ChooseDirection();
                }
            }
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
    void UpdateAnimation()
    {
        anim.SetFloat("Move X", myRigidbody.velocity.x);
        anim.SetFloat("Move Y", myRigidbody.velocity.y);
        anim.SetBool("Moving", true);
    }
    void UpdateAnimationIdle(Vector3 vector)
    {
        anim.SetFloat("Move X", vector.x);
        anim.SetFloat("Move Y", vector.y);
        anim.SetBool("Moving", false);
    }
    public void StopMovement()
    {
        canMove = false;
        anim.SetBool("Moving", false);
        myRigidbody.velocity = Vector2.zero;
    }
    public void StartMovement()
    {
        canMove = true;
    }
    public void LookInDirection()
    {
        Vector3 vector = target.position - transform.position;

        if (vector.x > 0 && Mathf.Abs(vector.y) < 1.5)
        {
            //right
            UpdateAnimationIdle(vector);
        }
        else if (vector.x < 0 && Mathf.Abs(vector.y) < 1.5)
        {
            //left
            UpdateAnimationIdle(vector);
        }
        else if (Mathf.Abs(vector.x) < 1 && vector.y > 0)
        {
            //up
            UpdateAnimationIdle(vector);
        }
        else if (Mathf.Abs(vector.x) < 1 && vector.y < 0)
        {
            //down
            UpdateAnimationIdle(vector);
        }
    }
}
