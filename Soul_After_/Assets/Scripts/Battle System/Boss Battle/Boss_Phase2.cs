using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase2 : MonoBehaviour
{
    public Transform[] firePoint;
    [HideInInspector]
    public TraumaSweep[] traumaPrefab;
    [HideInInspector]
    public GameObject warningPrefab;
    public Vector3 originalPos;
    public float patternCD;
    public GameObject bossObject;
    public AudioSource sfx;
    [HideInInspector]
    public bool isCooldown = false;

    private int rand;
    private Boss boss;
    private bool isInPlace = false;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
    }
    private void FixedUpdate()
    {
        if (boss.transform.position != originalPos)
        {
            Vector3 newPosition = Vector3.MoveTowards(boss.transform.position, originalPos, 2 * Time.deltaTime);
            bossObject.transform.position = newPosition;
        }
        else if (boss.transform.position == originalPos) isInPlace = true;
        if (isInPlace)
        {
            if (!isCooldown && !boss.cooldown)
            {
                StartCoroutine(TraumaAttack());
            }
        }
    }
    IEnumerator TraumaAttack()
    {
        isCooldown = true;
        rand = Random.Range(0, 2);
        Instantiate(warningPrefab, firePoint[rand + 2].position, firePoint[rand + 2].rotation);
        yield return new WaitForSeconds(1);
        TraumaSweep();
        yield return new WaitForSeconds(patternCD);
        boss.cooldown = false;
    }
    public void TraumaSweep()
    {
        if (sfx)
        {
            sfx.Play();
        }
        Instantiate(traumaPrefab[rand], firePoint[rand].position, firePoint[rand].rotation);
    }
}
