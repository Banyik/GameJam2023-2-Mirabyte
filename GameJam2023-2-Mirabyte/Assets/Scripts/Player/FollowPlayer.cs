using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public float maxY;
    public float minY;
    public float maxX;
    public float minX;
    private void FixedUpdate()
    {
        if(Player.transform.position.y + 3 < maxY && Player.transform.position.y + 3 > minY)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 3, -10);
        }
        else if(Player.transform.position.y + 3 < maxY)
        {
            transform.position = new Vector3(Player.transform.position.x, minY, -10);
        }
        else
        {
            transform.position = new Vector3(Player.transform.position.x, maxY, -10);
        }

        if (Player.transform.position.x < maxX && Player.transform.position.x > minX)
        {
            transform.position = new Vector3(Player.transform.position.x, gameObject.transform.position.y, -10);
        }
        else if (Player.transform.position.x < maxX)
        {
            transform.position = new Vector3(minX, gameObject.transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(maxX, gameObject.transform.position.y, -10);
        }
    }
}
