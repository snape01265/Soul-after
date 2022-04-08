using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGen : MonoBehaviour
{
    public DefenseGameManager gameManager;
    public GameObject MobPrefab;
    private int count = 0;

    public void SpawnMob()
    {
        float randY = Random.Range(gameManager.SpawnBorderYLower, gameManager.SpawnBorderYUpper);
        transform.localPosition = new Vector3(0, randY, 0);
        Instantiate(MobPrefab, transform.position, Quaternion.identity);

        count++;
        Debug.Log("Made a mob" + count);
    }
}
