using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchMovement : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D myRigidbody;
    private Transform myTransform;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        float y = Input.GetAxisRaw("Vertical");

        if (y > 0)
            player.transform.position = new Vector3(myTransform.position.x, 0.5f, myTransform.position.z);
        else if (y < 0)
            player.transform.position = new Vector3(myTransform.position.x, -3.5f, myTransform.position.z);
        else 
            player.transform.position = new Vector3(myTransform.position.x, -1.5f, myTransform.position.z);
    }
}
