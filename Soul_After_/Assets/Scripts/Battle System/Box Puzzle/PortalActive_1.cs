using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActive_1 : MonoBehaviour
{
    public Transform PortalBuddy;
    public AudioSource PortalSFX;

    private BoxPush_1 pushBox;
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
        if (!pushBox.teled && collision.gameObject.CompareTag("PushBox"))
        {
            StartCoroutine(Tele(collision.gameObject.GetComponent<BoxPush_1>()));
        }
    }

    IEnumerator Tele(BoxPush_1 pushBox)
    {
        PortalSFX.Play();
        pushBox.teled = true;
        pushBox.targetPos = PortalBuddy.transform.position;
        pushBox.transform.position = PortalBuddy.position;
        int pushPos = 5 - pushBox.PushedPos;
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
        yield return null;
    }
}
