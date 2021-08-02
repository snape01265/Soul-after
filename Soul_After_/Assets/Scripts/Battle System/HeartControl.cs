using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControl : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    private Vector2 movePos;
    private Vector3 startingPos = Vector3.zero;
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CancelControl();
        SetHeart();
    }

    public void SetHeart()
    {
        transform.position = startingPos;
        movePos = startingPos;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * sensitivity;
        float vertical = Input.GetAxis("Vertical") * sensitivity;

        movePos.x += horizontal;
        movePos.y += vertical;

        movePos.x = Mathf.Clamp(movePos.x, minX, maxX);
        movePos.y = Mathf.Clamp(movePos.y, minY, maxY);

        transform.position = Vector2.Lerp(transform.position, movePos, speed * Time.deltaTime);
    }
}
