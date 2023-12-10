using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Thief;
public class ThiefSpawner : MonoBehaviour
{
    Save save;
    PlayerBehaviour Player;
    public GameObject Thief;

    public Vector2 max;
    public Vector2 min;
    public GameObject Clone;

    private void Start()
    {
        save = gameObject.GetComponent<Save>();
        Player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }
    private void Update()
    {
        if(save.map == -1 || Clone != null)
        {
            return;
        }
        Vector2 pos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        if(save.map < 5)
        {
            Clone = Instantiate(Thief);
            Clone.transform.position = pos;
            Clone.GetComponent<ThiefController>().thiefType = ThiefType.GrinchGery;
            Clone.gameObject.SetActive(true);
        }
        else if(save.map < 10)
        {
            float range = Random.Range(0, 100);
            if(range < 50)
            {
                Clone = Instantiate(Thief);
                Clone.transform.position = pos;
                Clone.GetComponent<ThiefController>().thiefType = ThiefType.GrinchGery;
                Clone.gameObject.SetActive(true);
            }
            else
            {
                Clone = Instantiate(Thief);
                Clone.transform.position = pos;
                Clone.GetComponent<ThiefController>().thiefType = ThiefType.Julcsika;
                Clone.gameObject.SetActive(true);
            }
        }
        else
        {
            float range = Random.Range(0, 100);
            if(range < 33)
            {
                Clone = Instantiate(Thief);
                Clone.transform.position = pos;
                Clone.GetComponent<ThiefController>().thiefType = ThiefType.GrinchGery;
                Clone.gameObject.SetActive(true);
            }
            else if(range < 66)
            {
                Clone = Instantiate(Thief);
                Clone.transform.position = pos;
                Clone.GetComponent<ThiefController>().thiefType = ThiefType.PunchPongrac;
                Clone.gameObject.SetActive(true);
            }
            else
            {
                Clone = Instantiate(Thief);
                Clone.transform.position = pos;
                Clone.GetComponent<ThiefController>().thiefType = ThiefType.Julcsika;
                Clone.gameObject.SetActive(true);
            }
        }
    }
}
