using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Animator animator;
        public float speed;
        bool canDefend = true;
        bool checkDistanceForStun = false;
        float baseSpeed;
        public string character;
        public RuntimeAnimatorController boy;
        public RuntimeAnimatorController girl;
        public Weapon weapon;
        public GameObject TargetedThief;
        [SerializeField]
        State currentState;
        void Start()
        {
            animator.SetInteger("meleeType", (int)weapon);
            baseSpeed = speed;
            animator.runtimeAnimatorController =  character == "boy" ?  boy : girl;
        }

        private void FixedUpdate()
        {
            if (currentState != State.Stunned)
            {
                CheckMovement();
                CheckMeleeBehaviour();
            }
        }

        private void Update()
        {
            CheckState();
        }

        public State GetState()
        {
            return currentState;
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
                case State.Attack:
                    animator.SetTrigger("isAttacking");
                    StopMovement();
                    if (IsAnimationPlaying($"{character}_baton_hit"))
                    {
                        animator.ResetTrigger("isAttacking");
                        ChangeState(State.Idle);
                    }
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
            return TilesHandler.IsCurrentTileSnowy(new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y));
        }

        void CheckForCrowd()
        {
            if(TilesHandler.IsCurrentTileCrowdy(new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y)))
            {
                speed = baseSpeed / 2;
            }
            else
            {
                speed = baseSpeed;
            }
        }

        void CheckMeleeBehaviour()
        {
            if(Input.GetAxisRaw("Attack") == 1)
            {
                ChangeState(State.Attack);
            }
            else if(Input.GetAxisRaw("Defend") == 1)
            {
                Debug.Log("Defend");
            }
        }

        void CheckMovement()
        {
            if (!IsAnimationPlaying($"{character}_baton_hit"))
            {
                MovementHandler();
            }
            else
            {
                StopMovement();
            }
        }

        void MovementHandler()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical);
            CheckForCrowd();
            if (CheckForSnow())
            {
                rb.velocity = Vector2.Lerp(rb.velocity, movement, Time.deltaTime / (speed * 0.3f));
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, movement, speed) * speed;
            }
            if (rb.velocity != new Vector2(0, 0))
            {
                if (horizontal != 0)
                {
                    GetComponent<SpriteRenderer>().flipX = horizontal != -1;
                }
                ChangeState(State.Move);
                animator.SetBool("isMoving", true);
            }
            else if(currentState != State.Stunned)
            {
                ChangeState(State.Idle);
                animator.SetBool("isMoving", false);
            }
        }
        void StopMovement()
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }

        public void StartStun()
        {
            if (checkDistanceForStun && !CheckDistanceBetweenTarget(1.5f))
            {
                return;
            }
            animator.SetBool("isStunned", true);
            ChangeState(State.Stunned);
            StopMovement();
            Invoke(nameof(EndStun), 2);
        }

        public void InvokeStun(float seconds, bool ableToDefend, bool checkDistance)
        {
            canDefend = ableToDefend;
            checkDistanceForStun = checkDistance;
            Invoke(nameof(StartStun), seconds);
        }

        void EndStun()
        {
            ChangeState(State.Idle);
            animator.SetBool("isStunned", false);
        }

        bool CheckDistanceBetweenTarget(float maxDistance)
        {
            return Vector2.Distance(gameObject.transform.position, TargetedThief.transform.position) < maxDistance;
        }

        bool IsAnimationPlaying(string name)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
        }

    }
}
