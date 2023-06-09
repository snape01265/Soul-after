using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    private CameraMovement playerCamera;
    private void Start()
    {
        playerCamera = GetComponent<CameraMovement>();
    }

    void LateUpdate()
    {
        if(!transform.position.Equals(target.position))
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
    public void DisableComponent()
    {
        playerCamera.enabled = false;
    }
    
    public void EnableComponent()
    {
        playerCamera.enabled = true;
    }
}
