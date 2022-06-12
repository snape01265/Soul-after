using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public float Intensity;
    public float Duration;

    private CinemachineVirtualCamera Camera;

    private void Awake()
    {
        Camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Intensity;
        if (Duration > 0)
        {
            StartCoroutine(ReduceShake());
        }
    }

    IEnumerator ReduceShake()
    {
        yield return new WaitForSeconds(Duration);
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
