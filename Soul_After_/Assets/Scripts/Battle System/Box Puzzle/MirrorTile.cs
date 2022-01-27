using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTile : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        //if (collider tag = box && isMirrorWorld == false)
        //{
        //        StartCoroutine(MirrorWorldTransition())
        //        move player to mirror world on same grid
        //        move box to mirror world on same grid
        //        isMirrorWorld = true;
        //}
        //else if (collider tag = box && isMirrorWorld == true)
        //{
        //        StartCoroutine(MirrorWorldTransition())
        //        move player to original world on same grid
        //        move box to original world on same grid
        //        isMirrorWorld = false;
        //}
    }

    IEnumerator MirrorWorldTransition()
    {
        //Play some glowing animation
        yield return null;
    }
}
