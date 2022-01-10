using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private double timeInstantiated;
    public float assignedTime;
    public GameObject badEffect, goodEffect, perfectEffect, missEffect;

    private void Start()
    {
        timeInstantiated = GameManager.GetAudioSourceTime();
        timeInstantiated = assignedTime - GameManager.instance.noteTime;
    }
    void Update()
    {
        double timeSinceInstantiated = GameManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (GameManager.instance.noteTime * 2));

        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.right * GameManager.instance.noteSpawnX, Vector3.right * GameManager.instance.noteDespawnX, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void BadHit()
    {
        Instantiate(badEffect, transform.position, perfectEffect.transform.rotation);
    }
    public void GoodHit()
    {
        Instantiate(goodEffect, transform.position, perfectEffect.transform.rotation);
    }
    public void PerfectHit()
    {
        Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
    }
}
