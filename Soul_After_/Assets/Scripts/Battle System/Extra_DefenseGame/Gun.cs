using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public DefenseGameManager GameManager;
    public GameObject BulletPrefab;

    private float gunSpread;
    private Quaternion gunRotation;
    private bool spreadCD = false;

    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            if (gunSpread <= GameManager.MaxGunSpread && !spreadCD)
            {
                StartCoroutine(GunSpread());
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.tag = "bullet";
            }
        } else if (gunSpread > 0 && !spreadCD)
        {
            StartCoroutine(SpreadCD());
        }
    }

    IEnumerator GunSpread()
    {
        spreadCD = true;
        gunSpread += GameManager.MaxGunSpread / GameManager.MaxGunSpreadTime * GameManager.GunAtkSpd;
        yield return new WaitForSeconds(GameManager.GunAtkSpd);
        spreadCD = false;
    }

    IEnumerator SpreadCD()
    {
        spreadCD = true;
        gunSpread -= GameManager.MaxGunSpread / GameManager.MaxGunSpreadTime * GameManager.GunAtkSpd;
        yield return new WaitForSeconds(GameManager.GunAtkSpd);
        spreadCD = false;
    }
}
