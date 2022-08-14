using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public Texture2D cursorArrow;

    private Vector2 cursorHotspot;

    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorArrow.width / 2.8f, cursorArrow.height / 2.8f);
        Cursor.SetCursor(cursorArrow, cursorHotspot, CursorMode.ForceSoftware);
    }
}
