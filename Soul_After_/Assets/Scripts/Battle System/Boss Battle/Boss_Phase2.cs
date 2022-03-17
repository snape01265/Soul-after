using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase2 : MonoBehaviour
{
    public Transform firePoint;
    public TraumaSweep traumaPrefab;
    public Vector3 originalPos;
    public float patternCD;

    private Boss boss;
    private bool isInPlace = false;
    private bool isCooldown = false;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
    }
    private void Update()
    {
        if (transform.position != originalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, 2 * Time.deltaTime);
        }
        else if (transform.position == originalPos) isInPlace = true;
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
        yield return new WaitForEndOfFrame();
        TraumaSweep();
        yield return new WaitForSeconds(patternCD);
        isCooldown = false;
        boss.cooldown = false;
    }
    public void TraumaSweep()
    {
        Instantiate(traumaPrefab, firePoint.position, firePoint.rotation);
    }
}
