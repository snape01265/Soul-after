using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//특정 바운더리에서 NPC를 움직이게 하는 스크립트
public class NpcController : MonoBehaviour
{
    public float moveSpeed;
    public float walkTime;
    public float waitTime;
    public bool canMove;
    [HideInInspector]
    public Collider2D walkZone;

    private Transform player;
    private bool isWalking;
    private Vector3 directionVector;
    private int walkDirection;
    private float walkCounter;
    private float waitCounter;

    private Rigidbody2D myRigidbody;
    private Transform myTransform;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        walkCounter = walkTime;
        ChooseDirection();
    }

    void Update()
    {
        if (canMove)
        {
            if (isWalking)
            {
                walkCounter -= Time.deltaTime;
                Move();
                UpdateAnimation();
                if (walkCounter < 0)
                {
                    isWalking = false;
                    waitCounter = waitTime;
                    walkCounter = walkTime;
                    anim.SetBool("Moving", false);
                }
            }
            else
            {
                waitCounter -= Time.deltaTime;
                myRigidbody.velocity = Vector2.zero;
                if (waitCounter < 0)
                {
                    ChooseDirection();
                }
            }
        }
    }

    public void Move()
    {
        Vector3 temp = myTransform.position + directionVector * moveSpeed * Time.deltaTime;
        if (walkZone.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChooseDirection();
        }
    }
    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        switch(walkDirection)
        {
            case 0:
                isWalking = true;
                anim.SetBool("Moving", false);
                directionVector = Vector3.up;
                break;
            case 1:
                isWalking = true;
                anim.SetBool("Moving", false);
                directionVector = Vector3.down;
                break;
            case 2:
                isWalking = true;
                anim.SetBool("Moving", false);
                directionVector = Vector3.left;
                break;
            case 3:
                isWalking = true;
                anim.SetBool("Moving", false);
                directionVector = Vector3.right;
                break;
            default:
                break;
        }
    }
    void UpdateAnimation()
    {
        anim.SetFloat("Move_X", directionVector.x);
        anim.SetFloat("Move_Y", directionVector.y);
        anim.SetBool("Moving", true);
    }
    void UpdateAnimationIdle(Vector3 vector)
    {
        anim.SetFloat("Move_X", vector.x);
        anim.SetFloat("Move_Y", vector.y);
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
        Vector3 vector = player.position - transform.position;

        if (vector.x > 0 && Mathf.Abs(vector.y) < 1)
        {
            //right
            UpdateAnimationIdle(vector);
        }
        else if (vector.x < 0 && Mathf.Abs(vector.y) < 1)
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

