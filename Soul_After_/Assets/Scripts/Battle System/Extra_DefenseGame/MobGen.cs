using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGen : MonoBehaviour
{
    public DefenseGameManager gameManager;
    public GameObject MobPrefab;

    public void SpawnMob()
    {
        Instantiate(MobPrefab);
    }
}
