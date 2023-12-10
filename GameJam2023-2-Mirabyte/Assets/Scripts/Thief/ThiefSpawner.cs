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
    public List<GameObject> Clones = new List<GameObject>();



    private void Start()
    {
        save = gameObject.GetComponent<Save>();
        Player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }
    public void KillAll()
    {
        foreach (var item in Clones)
        {
            Destroy(item);
        }
    }
    public void LateSpawn()
    {
        Invoke(nameof(Spawn), 2);
    }
    public void Spawn()
    {
        Vector2 pos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        while (Vector2.Distance(pos, Player.gameObject.transform.position) < 10)
        {
            pos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
        if (save.map < 5)
        {
            SpawnThief(pos, ThiefType.GrinchGery);
        }
        else if (save.map < 10)
        {
            float range = Random.Range(0, 100);
            if (range < 50)
            {
                SpawnThief(pos, ThiefType.GrinchGery);
            }
            else
            {
                SpawnThief(pos, ThiefType.Julcsika);
            }
        }
        else
        {
            float range = Random.Range(0, 100);
            if (range < 33)
            {
                SpawnThief(pos, ThiefType.GrinchGery);
            }
            else if (range < 66)
            {
                SpawnThief(pos, ThiefType.PunchPongrac);
            }
            else
            {
                SpawnThief(pos, ThiefType.Julcsika);
            }
        }
    }

    void SpawnThief(Vector2 pos, ThiefType thiefType)
    {
        Clone = Instantiate(Thief);
        Clone.name = "thief";
        Clone.transform.position = pos;
        Clone.GetComponent<ThiefController>().thiefType = thiefType;
        Clone.gameObject.SetActive(true);
        Clones.Add(Clone);
    }
}
