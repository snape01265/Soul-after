using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolList : ScriptableObject
{
    public List<bool> initialValue = new List<bool>();
    public List<bool> defaultValue = new List<bool>();
}