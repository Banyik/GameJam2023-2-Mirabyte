using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image[] hearts, Shield;
    public GameObject dayCount,player;
    public TextMeshProUGUI dayCounter;
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.GetComponent<PlayerBehaviour>().hp)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        for (int i = 0; i < Shield.Length; i++)
        {
            if (i < player.GetComponent<PlayerBehaviour>().shield)
            {
                Shield[i].enabled = true;
            }
            else
            {
                Shield[i].enabled = false;
            }
        }
        dayCounter.text = dayCount.GetComponent<Save>().map + ".Nap";
    }
}
