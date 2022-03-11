using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Turret : MonoBehaviour
{
    [HideInInspector]
    public CinemachineVirtualCamera playerCam;
    [HideInInspector]
    public CinemachineVirtualCamera turretCam;

    private bool playerInRange = false;
    private GameObject playerPosition;
    private bool onTurret = false;
    private Player player;
    private Animator playerAnim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !onTurret && playerInRange)
        {
            Vector3 turretPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            playerPosition.transform.position = turretPosition;
            player.CancelControl();
            playerAnim.SetFloat("Move_X", 0);
            playerAnim.SetFloat("Move_Y", 1);
            playerAnim.SetBool("Moving", false);
            onTurret = true;
            playerCam.Priority = 0;
            turretCam.Priority = 1;
        }
        else if (Input.GetKeyDown(KeyCode.F) && onTurret)
        {
            player.GiveBackControl();
            playerCam.Priority = 1;
            turretCam.Priority = 0;
            onTurret = false;
        }
    }
    private void OnDisable()
    {
        if(onTurret)
        {
            player.GiveBackControl();
            playerCam.Priority = 1;
            turretCam.Priority = 0;
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
