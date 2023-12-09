using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpAndShieldUI : MonoBehaviour
{
    public Image[] hearts, Shield;
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < gameObject.GetComponent<PlayerBehaviour>().hp)
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
            if (i < gameObject.GetComponent<PlayerBehaviour>().shield)
            {
                Shield[i].enabled = true;
            }
            else
            {
                Shield[i].enabled = false;
            }
        }
    }
}
