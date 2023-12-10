using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Save saveGame;
    public GameObject CharacterChoose;
    public void HasSave()
    {
        if (File.Exists(saveGame.path))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            CharacterChoose.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
