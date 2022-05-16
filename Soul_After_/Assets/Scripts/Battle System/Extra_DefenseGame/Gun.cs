using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Player player;
    public DefenseGameManager GameManager;
    public GameObject BulletPrefab;
    public AudioSource GunFireSFX;

    private float gunSpread = 0;
    private bool spreadCD = false;
    private bool isSlowed = false;
    private float defSpeed;
    private readonly float SPEEDDECREASEMULTI = .66f;

    private void Start()
    {
        defSpeed = player.speed;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            if (!isSlowed)
            {
                player.speed *= SPEEDDECREASEMULTI;
                isSlowed = true;
            }

            if (gunSpread <= GameManager.MaxGunSpreadDeg && !spreadCD)
            {
                float randAngle = Random.Range(-gunSpread, gunSpread);
                StartCoroutine(GunSpread());
                GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                GunFireSFX.Play();
                bullet.GetComponent<GunBullet>().FireAtAngle(randAngle);
            }
        } else if (gunSpread > 0 && !spreadCD)
        {
            if (isSlowed)
            {
                player.speed = defSpeed;
                isSlowed = false;
            }
            StartCoroutine(SpreadCD());
        }
    }

    IEnumerator GunSpread()
    {
        spreadCD = true;
        gunSpread = Mathf.Clamp(gunSpread + GameManager.MaxGunSpreadDeg / GameManager.MaxGunSpreadTime * GameManager.GunAtkSpd, 0, GameManager.MaxGunSpreadDeg);
        yield return new WaitForSeconds(GameManager.GunAtkSpd);
        spreadCD = false;
    }

    IEnumerator SpreadCD()
    {
        spreadCD = true;
        gunSpread = Mathf.Clamp(gunSpread - GameManager.MaxGunSpreadDeg / GameManager.MaxGunSpreadTime * GameManager.GunAtkSpd, 0, GameManager.MaxGunSpreadDeg);
        yield return new WaitForSeconds(GameManager.GunAtkSpd);
        spreadCD = false;
    }
}
