using System;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public VectorValue startingPosition;
    public StringValue lastScene;

    private bool inRange;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            Transform playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            startingPosition.initialValue = new Vector2(playerPos.transform.position.x, playerPos.transform.position.y);
            lastScene.initialValue = gameObject.scene.name;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
