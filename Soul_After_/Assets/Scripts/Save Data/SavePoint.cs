using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject gameSaveManager;
    public GameSaveManager gameSave;
    public VectorValue startingPosition;
    public bool inRange;
    private void Start()
    {
        gameSave = gameSaveManager.GetComponent<GameSaveManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            startingPosition.initialValue = new Vector2(transform.position.x, transform.position.y - 1);
            gameSave.SaveScriptables(gameSave.objToSave);
            Debug.Log("game saved");
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
