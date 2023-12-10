using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public byte point;
    bool otodikUtes;
    public GameObject player;
    void Start()
    {
        point = 0;
        otodikUtes = false;
    }
    void Update()
    {
        if (player.GetComponent<PlayerBehaviour>().thiefStunned == 3 && point == 0 && !otodikUtes)
        {
            point += 1;
            otodikUtes = true;
        }
        else if (player.GetComponent<PlayerBehaviour>().thiefStunned % 3 == 0 && point != 0 && !otodikUtes) 
        {
            point += 1;
            otodikUtes = true;
        }
        else if(otodikUtes && player.GetComponent<PlayerBehaviour>().thiefStunned % 3 != 0)
        {
            otodikUtes = false;
        }
    }
}
