using System.Collections;
using System.Collections.Generic;
using Thief;
using UnityEngine;

public class ChristmasCandyBehaviour : MonoBehaviour
{
    bool isShooting = false;
    GameObject TargetedThief;
    float lifeTime = 0;
    float maxTime = 50;
    public void Shoot(Vector3 from, Vector3 to, GameObject TargetedThief)
    {
        this.TargetedThief = TargetedThief;
        transform.LookAt(to);
        isShooting = true;
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            //gameObject.transform.Rotate(0, 0, 5, Space.Self);
            lifeTime += Time.deltaTime;
            transform.position += transform.forward * 20 * Time.deltaTime;
            if (Vector3.Distance(gameObject.transform.position, TargetedThief.transform.position) < 0.3f)
            {
                TargetedThief.GetComponent<ThiefController>().StartStun();
                Destroy(gameObject);
            }
            else if (lifeTime >= maxTime)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
