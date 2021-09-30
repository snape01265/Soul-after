using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSpawner : MonoBehaviour
{
    public GameObject[] flowers;

    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }
    IEnumerator SpawnRandomGameObject()
    {
        yield return null;

        int randomFlower = Random.Range(0, flowers.Length);

        Instantiate(flowers[randomFlower], Vector3.zero, Quaternion.identity);
    }
}
