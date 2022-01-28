using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxPuzzleManager : MonoBehaviour
{
    private Transform playerLoc;
    private Transform boxLoc;
    private bool isMirrorWorld = false;

    private void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").transform;
        boxLoc = GameObject.FindGameObjectWithTag("PushBox").transform;
    }

    private void FixedUpdate()
    {
        if(Input.GetButtonDown("F"))
        {
            if(!isMirrorWorld)
            {
                MirrorActivate(isMirrorWorld);
            }
            else if(isMirrorWorld)
            {
                MirrorActivate(isMirrorWorld);
            }
        }
    }
    public void MirrorActivate(bool mirrorWorld)
    {
        StartCoroutine(MirrorWorldTransition());
        if (mirrorWorld == false)
        {
            playerLoc.transform.position.Set(playerLoc.transform.position.x + 22, playerLoc.transform.position.y, playerLoc.transform.position.z);
            isMirrorWorld = true;
        }
        else
        {
            playerLoc.transform.position.Set(playerLoc.transform.position.x - 22, playerLoc.transform.position.y, playerLoc.transform.position.z);
            isMirrorWorld = false;
        }
    }
    IEnumerator MirrorWorldTransition()
    {
        //Play some glowing animation
        yield return null;
    }
}
