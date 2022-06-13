using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private double timeInstantiated;
    public float assignedTime;
    public GameObject badEffect, goodEffect, perfectEffect, missEffect;
    private Vector3 hitLine;

    private void Start()
    {
        timeInstantiated = GameManager.GetAudioSourceTime();
        timeInstantiated = assignedTime - GameManager.instance.noteTime;
        hitLine = new Vector3(transform.position.x, -5f);
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
            transform.localPosition = Vector3.Lerp(Vector3.up * GameManager.instance.noteSpawnY, Vector3.up * GameManager.instance.noteDespawnY, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void BadHit()
    {
        Instantiate(badEffect, hitLine, badEffect.transform.rotation);
    }
    public void GoodHit()
    {
        Instantiate(goodEffect, hitLine, goodEffect.transform.rotation);
    }
    public void PerfectHit()
    {
        Instantiate(perfectEffect, hitLine, perfectEffect.transform.rotation);
    }
}
