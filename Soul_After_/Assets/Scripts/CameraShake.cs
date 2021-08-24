using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnim;
    public Animator imageAnim;

    public void CamShakeWithImage()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            camAnim.SetTrigger("shake1");
            imageAnim.SetTrigger("flash1");
        }
        else if (rand == 1)
        {
            camAnim.SetTrigger("shake2");
            imageAnim.SetTrigger("flash2");
        }
        else if (rand == 2)
        {
            camAnim.SetTrigger("shake3");
            imageAnim.SetTrigger("flash3");
        }
        else if (rand == 3)
        {
            camAnim.SetTrigger("shake4");
            imageAnim.SetTrigger("flash4");
        }
    }
    public void CamShake()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            camAnim.SetTrigger("shake1");
        }
        else if (rand == 1)
        {
            camAnim.SetTrigger("shake2");
        }
        else if (rand == 2)
        {
            camAnim.SetTrigger("shake3");
        }
        else if (rand == 3)
        {
            camAnim.SetTrigger("shake4");
        }
    }
}
