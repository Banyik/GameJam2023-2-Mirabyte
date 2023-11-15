using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed;
        [SerializeField]
        State currentState;
        void Start()
        {
            SnowyTilesHandler.Generate();
        }

        private void FixedUpdate()
        {
            CheckMovement();
            CheckState();
        }

        void CheckState()
        {
            switch (currentState)
            {
                case State.Idle:
                    if (CheckForSnow())
                    {
                        StopMovement();
                    }
                    break;
                case State.Move:
                    break;
                default:
                    break;
            }
        }

        void ChangeState(State state)
        {
            currentState = state;
        }

        bool CheckForSnow()
        {
            return SnowyTilesHandler.IsCurrentTileSnowy(new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y));
        }

        void CheckMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical);
            if(CheckForSnow())
            {
                rb.velocity = Vector2.Lerp(rb.velocity, movement, Time.deltaTime / (speed * 0.3f));
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, movement, speed) * speed;
            }
            Debug.Log(rb.velocity);
            if (rb.velocity != new Vector2(0, 0))
            {
                if (horizontal != 0)
                {
                    GetComponent<SpriteRenderer>().flipX = horizontal == -1;
                }
                ChangeState(State.Move);
            }
            else
            {
                ChangeState(State.Idle);
            }
        }
        void StopMovement()
        {
            rb.velocity = Vector2.zero;
        }
    }
}
