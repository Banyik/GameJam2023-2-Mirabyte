using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public byte point;
    void Start()
    {
        point = 0;
    }
    void Update()
    {
        if (gameObject.GetComponent<PlayerBehaviour>().thiefStunned % 5 == 0 && point != 0) 
        {
            point += 1;
        }
    }
}
