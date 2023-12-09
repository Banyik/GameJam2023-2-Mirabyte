using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public byte point;
    public TextMeshProUGUI counter;
    void Start()
    {
        point = 0;
        counter.text = point.ToString();
    }
    void Update()
    {
        /*if (gameObject.GetComponent<PlayerBehaviour>().thiefStunned % 5 == 0 && point != 0) 
        {
            point += 1;
            counter.text = point.ToString();
        }*/
    }
}
