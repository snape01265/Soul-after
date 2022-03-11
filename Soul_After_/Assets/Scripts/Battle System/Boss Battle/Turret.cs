using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [HideInInspector]
    public bool playerInRange = false;

    private GameObject playerPosition;
    private bool onTurret;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !onTurret)
        {
            Vector3 turretPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            playerPosition.transform.position = turretPosition;
            player.CancelControl();
            onTurret = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && onTurret)
        {
            player.GiveBackControl();
            onTurret = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
