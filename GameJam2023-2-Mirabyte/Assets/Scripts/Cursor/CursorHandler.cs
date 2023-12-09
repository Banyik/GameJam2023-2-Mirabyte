using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    public Texture2D taser;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(taser, hotSpot, cursorMode);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(taser, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
