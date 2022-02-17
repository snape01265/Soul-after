using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActive_1 : MonoBehaviour
{
    public Transform PortalBuddy;
    public AudioSource PortalSFX;

    private List<BoxPush_1> pushBoxes = new List<BoxPush_1>();

    private void Start()
    {
        foreach(GameObject pushBox in GameObject.FindGameObjectsWithTag("PushBox"))
        {
            pushBoxes.Add(pushBox.GetComponent<BoxPush_1>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushBox") && !collision.gameObject.GetComponent<BoxPush_1>().teled)
        {
            StartCoroutine(Tele(collision.gameObject.GetComponent<BoxPush_1>()));
        }
    }

    IEnumerator Tele(BoxPush_1 pushBox)
    {
        yield return new WaitForSeconds(.2f);
        PortalSFX.Play();
        pushBox.teled = true;
        pushBox.transform.position = PortalBuddy.position;
        pushBox.targetPos = PortalBuddy.transform.position;
        int pushPos = 5 - pushBox.PushedPos;
        pushBox.PushedPos = pushPos;
        switch (pushPos)
        {
            case 1:
                pushBox.DestCalcUp();
                break;
            case 2:
                pushBox.DestCalcRight();
                break;
            case 3:
                pushBox.DestCalcLeft();
                break;
            case 4:
                pushBox.DestCalcDown();
                break;
        }
        yield return new WaitForSeconds(.2f);
        pushBox.teled = false;
    }
}
