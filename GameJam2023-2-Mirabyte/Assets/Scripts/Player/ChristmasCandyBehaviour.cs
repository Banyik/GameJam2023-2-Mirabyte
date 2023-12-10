using System.Collections;
using System.Collections.Generic;
using Thief;
using UnityEngine;

namespace Player
{
    public class ChristmasCandyBehaviour : MonoBehaviour
    {
        bool isShooting = false;
        float lifeTime = 0;
        float maxTime = 10;
        public void Shoot(Vector3 from, Vector3 to)
        {
            transform.LookAt(to);
            isShooting = true;
        }

        private void FixedUpdate()
        {
            if (isShooting)
            {
                gameObject.transform.Rotate(0, 0, 5, Space.Self);
                lifeTime += Time.deltaTime;
                transform.position += transform.forward * 20 * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                if (lifeTime >= maxTime)
                {
                    Destroy(gameObject);
                }

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "thief")
            {
                collision.gameObject.GetComponent<ThiefController>().StartStun();
                GameObject.Find("Player").GetComponent<PlayerBehaviour>().thiefStunned++;
                GameObject.Find("Player").GetComponent<PlayerBehaviour>().TargetedThief = null;
                GameObject.Find("ScriptHandler").GetComponent<ThiefSpawner>().LateSpawn();
                Destroy(gameObject);
            }
        }
    }
}

