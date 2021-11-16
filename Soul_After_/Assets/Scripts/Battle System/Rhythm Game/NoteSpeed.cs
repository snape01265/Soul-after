using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpeed : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;

    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        if(hasStarted)
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        }
    }
}
