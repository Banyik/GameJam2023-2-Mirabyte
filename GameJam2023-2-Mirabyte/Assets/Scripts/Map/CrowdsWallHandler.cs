using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdWallHandler : MonoBehaviour
{
    public Sprite[] crowds;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = crowds[Random.Range(0, crowds.Length)];
        gameObject.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 100) > 50;
    }
}
