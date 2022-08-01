using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<Image>().material;
    }
    void Update()
    {
        if (material != null)
        {
            offset = moveSpeed * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
    }
    public void ResetPosition()
    {
        material.mainTextureOffset = new Vector2(0f, 0f);
    }
}
