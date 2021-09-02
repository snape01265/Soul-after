using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    public RPGTalk rpgTalk;
    public void FatherEnd()
    {
        rpgTalk.NewTalk("success4_1_start", "success4_1_end", rpgTalk.txtToParse);
    }
}
