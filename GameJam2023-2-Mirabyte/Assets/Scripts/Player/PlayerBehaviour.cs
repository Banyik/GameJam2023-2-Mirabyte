using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Thief;

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
        public GameObject christmasCandy;
        float reachDistance;
        [SerializeField]
        State currentState;
        void Start()
        {
            baseSpeed = speed;
            animator.runtimeAnimatorController =  character == "boy" ?  boy : girl;
            animator.SetInteger("meleeType", (int)weapon);
            switch (weapon)
            {
                case Weapon.Tazer:
                    reachDistance = 0.8f;
                    break;
                case Weapon.Baton:
                    reachDistance = 1.5f;
                    break;
                case Weapon.CandyCane:
                    reachDistance = 1.2f;
                    break;
                default:
                    break;
            }
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
                    AttackAim();
                    animator.SetTrigger("isAttacking");
                    StopMovement();
                    if (GetAnimationName().Contains("hit"))
                    {
                        animator.ResetTrigger("isAttacking");
                        ChangeState(State.Idle);
                    }
                    break;
                case State.Defend:
                    animator.SetTrigger("isDefending");
                    StopMovement();
                    if (IsAnimationPlaying($"{character}_crouch"))
                    {
                        animator.ResetTrigger("isDefending");
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
            Debug.DrawRay(rb.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red);
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Input.mousePosition, 1.5f, 3);
            if(currentState == State.Stunned)
            {
                return;
            }
            if (Input.GetAxisRaw("Attack") == 1)
            {
                ChangeState(State.Attack);
                if(weapon != Weapon.Cannon)
                {
                    if (hit.rigidbody == null)
                    {
                        return;
                    }
                    else if (hit.collider.CompareTag("thief"))
                    {
                        TargetedThief.GetComponent<ThiefController>().StartStun();
                    }
                }
                else if(!GetAnimationName().Contains("cannon"))
                {
                    float addToX = GetComponent<SpriteRenderer>().flipX ? 0.59f : -0.59f;
                    var candy = Instantiate(christmasCandy, new Vector3(transform.position.x + addToX, transform.position.y + 1f, 0), new Quaternion(0, 0, 0, 0), null);
                    candy.SetActive(true);
                    candy.GetComponent<ChristmasCandyBehaviour>().Shoot(new Vector3(transform.position.x + addToX, transform.position.y + 1f, 0), Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                
            }
            else if(Input.GetAxisRaw("Defend") == 1)
            {
                ChangeState(State.Defend);
                Debug.Log("Defend");
            }
        }

        void CheckMovement()
        {
            if (!GetAnimationName().Contains("hit") && !IsAnimationPlaying($"{character}_crouch"))
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
            if ((checkDistanceForStun && !CheckDistanceBetweenTarget(1.5f)) || currentState == State.Defend ||
                TargetedThief == null || !TargetedThief.GetComponent<ThiefController>().IsActive)
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

        string GetAnimationName()
        {
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }

        public void AttackAim() 
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<SpriteRenderer>().flipX = mousePos.x > rb.position.x;
        }
    }
}
