using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Turret : MonoBehaviour
{
    public float rotationSpeed;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool cooldown;
    [HideInInspector]
    public CinemachineVirtualCamera playerCam;
    [HideInInspector]
    public CinemachineVirtualCamera turretCam;

    private Quaternion originalRotation;
    private GameObject playerPosition;
    private bool playerInRange = false;
    private bool onTurret = false;
    private Player player;
    private Animator playerAnim;
    private Transform turretTransform;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        turretTransform = GetComponent<SpriteRenderer>().transform;
        originalRotation = transform.rotation;
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
            playerCam.Priority = 0;
            turretCam.Priority = 1;
            onTurret = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && onTurret)
        {
            player.GiveBackControl();
            playerCam.Priority = 1;
            turretCam.Priority = 0;
            onTurret = false;
        }
        if (onTurret)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !cooldown)
            {
                FireBullet();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                turretTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                turretTransform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
            }
            float minRotation = -75;
            float maxRotation = 75;
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            currentRotation.z = (currentRotation.z > 180) ? currentRotation.z - 360 : currentRotation.z;
            currentRotation.z = Mathf.Clamp(currentRotation.z, minRotation, maxRotation);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }
    private void FireBullet()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
        transform.rotation = originalRotation;
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
