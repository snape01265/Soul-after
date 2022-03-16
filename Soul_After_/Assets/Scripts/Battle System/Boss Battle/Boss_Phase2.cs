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
    private bool isInPlace = true;
    private bool isCooldown = false;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
    }
    private void Update()
    {
        if (transform.position != originalPos)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, originalPos.x, 0.5f * Time.deltaTime), transform.position.y, transform.position.z);
            isInPlace = true;
        }
        if (isInPlace)
        {
            if (!isCooldown && !boss.cooldown)
            {
                StartCoroutine(TraumaAttack());
            }
            else if (boss.cooldown)
            {

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
    }
    public void TraumaSweep()
    {
        Instantiate(traumaPrefab, firePoint.position, firePoint.rotation);
    }
}
