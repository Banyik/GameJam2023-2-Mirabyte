using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    public float time;
    float timeLimit = 180;
    public PauseMenu pauseMenu;
    public RoundOver roundOver;

    private void FixedUpdate()
    {
        if(!pauseMenu.GameIsPaused && time < timeLimit)
        {
            time += Time.deltaTime;
        }
        else if(!pauseMenu.GameIsPaused)
        {
            pauseMenu.PauseOnly();
            if(roundOver.save.map + 1 >= 22)
            {
                roundOver.NextPanel();
            }
            else
            {
                roundOver.ShowShopPanel();
            }
        }
    }

    public void ResetTimer()
    {
        time = 0;
    }
}
