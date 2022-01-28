using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxPuzzleManager : MonoBehaviour
{
    public KeyCode keyForMirror;

    private PushBox box;
    private GameObject player;
    private GameObject mainCamera;
    private bool isMirrorWorld = false;
    private bool isAvailable = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        box = GameObject.FindGameObjectWithTag("PushBox").GetComponent<PushBox>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyForMirror) && isAvailable && !box.pushing)
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
            player.transform.position = new Vector3(player.transform.position.x + 22, player.transform.position.y, player.transform.position.z);
            box.targetPos = new Vector3(box.transform.position.x + 22, box.transform.position.y, box.transform.position.z);
            box.transform.position = new Vector3(box.transform.position.x + 22, box.transform.position.y, box.transform.position.z);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 22, mainCamera.transform.position.y, mainCamera.transform.position.z);
            isMirrorWorld = true;
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x - 22, player.transform.position.y, player.transform.position.z);
            box.targetPos = new Vector3(box.transform.position.x - 22, box.transform.position.y, box.transform.position.z);
            box.transform.position = new Vector3(box.transform.position.x - 22, box.transform.position.y, box.transform.position.z);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 22, mainCamera.transform.position.y, mainCamera.transform.position.z);
            isMirrorWorld = false;
        }
    }
    IEnumerator MirrorWorldTransition()
    {
        isAvailable = false;
        //Play some glowing animation
        isAvailable = true;
        yield return null;
    }
}
