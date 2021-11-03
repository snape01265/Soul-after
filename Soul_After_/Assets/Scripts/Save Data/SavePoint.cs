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
            startingPosition.initialValue = new Vector2(transform.position.x, transform.position.y - 1);
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
