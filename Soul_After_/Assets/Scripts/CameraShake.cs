using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnim;
    public Animator imageAnim;

    public void CamShakeWithImage()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                camAnim.SetTrigger("shake1");
                imageAnim.SetTrigger("flash1");
                break;
            case 1:
                camAnim.SetTrigger("shake2");
                imageAnim.SetTrigger("flash2");
                break;
            case 2:
                camAnim.SetTrigger("shake3");
                imageAnim.SetTrigger("flash3");
                break;
            case 3:
                camAnim.SetTrigger("shake4");
                imageAnim.SetTrigger("flash4");
                break;
        }
    }

    public void CamShake()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                camAnim.SetTrigger("shake1");
                break;
            case 1:
                camAnim.SetTrigger("shake2");
                break;
            case 2:
                camAnim.SetTrigger("shake3");
                break;
            case 3:
                camAnim.SetTrigger("shake4");
                break;
        }
    }
}
