using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorHandler : MonoBehaviour
{
    public Texture2D taser;
    public Texture2D cross;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;
    string currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "Menu":
                Cursor.SetCursor(taser, hotSpot, cursorMode);
                break;
            case "MovementTest":
                Cursor.SetCursor(cross, hotSpot, cursorMode);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (currentScene == "MovementTest")
        {
            InGameCursorChange();
        }
    }
    void InGameCursorChange() 
    {
        if (gameObject.GetComponent<PauseMenu>().GameIsPaused)
        {
            Cursor.SetCursor(taser, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(cross, hotSpot, cursorMode);
        }
    }
}
