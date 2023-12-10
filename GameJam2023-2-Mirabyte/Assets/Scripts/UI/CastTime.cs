using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTime : MonoBehaviour
{
    public GameObject Day;
    public GameObject Night;

    public void CheckDayTime()
    {
        if(DateTime.Now.Hour < 16 && DateTime.Now.Hour > 7)
        {
            Day.gameObject.SetActive(true);
            Night.gameObject.SetActive(false);
        }
        else
        {
            Day.gameObject.SetActive(false);
            Night.gameObject.SetActive(true);
        }
    }
}
