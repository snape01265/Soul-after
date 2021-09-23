using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    public int tileId;
    public Vector3 targetPosition;
    [SerializeField] private Vector3 correctPosition;
    public bool inRightPlace;
    private SpriteRenderer _sprite;
    private void Awake()
    {
        targetPosition = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
        if (targetPosition == correctPosition)
        {
            _sprite.color = Color.green;
            inRightPlace = true;
        }
        else
        {
            _sprite.color = Color.white;
            inRightPlace = false;
        }
    }
}
