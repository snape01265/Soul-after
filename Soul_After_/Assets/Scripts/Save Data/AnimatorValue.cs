using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class AnimatorValue : ScriptableObject
{
    public AnimatorOverrideController initialAnimator;
    public AnimatorOverrideController defaultAnimator;
}
