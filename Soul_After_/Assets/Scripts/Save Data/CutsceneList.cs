using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class CutsceneList : ScriptableObject
{
    public List<string> initialValue = new List<string>();
    public List<string> defaultValue = new List<string>();
}