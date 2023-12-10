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
        PlayerAudioHandler audioHandler;
        float reachDistance;
        public byte hp, shield;
        public byte thiefStunned;
        [SerializeField]
        State currentState;

        ThiefSpawner thiefSpawner;
        public GameObject PowerUp;
        void Start()
        {
            GameObject.Find("ScriptHandler").GetComponent<Save>().LoadGame();
            thiefSpawner = GameObject.Find("ScriptHandler").GetComponent<ThiefSpawner>();
            character = GameObject.Find("ScriptHandler").GetComponent<Save>().character;
            weapon = (Weapon)GameObject.Find("ScriptHandler").GetComponent<Save>().weapon;
            audioHandler = gameObject.GetComponent<PlayerAudioHandler>();
            baseSpeed = speed;
            animator.runtimeAnimatorController =  character == "boy" ?  boy : girl;
			hp = 3;
            shield = 3;
            thiefStunned = 0;
            animator.SetInteger("meleeType", (int)weapon);
            switch (weapon)
            {
                case Weapon.Taser:
                    reachDistance = 1.8f;
                    break;
                case Weapon.Baton:
                    reachDistance = 3.5f;
                    break;
                case Weapon.CandyCane:
                    reachDistance = 2.2f;
                    break;
                default:
                    break;
            }
            thiefSpawner.LateSpawn();
        }

        void StartAttackSound(bool hit, bool overrideSound)
        {
            switch (weapon)
            {
                case Weapon.Taser:
                    audioHandler.PlayClip(PlayerSounds.Taser, overrideSound);
                    break;
                case Weapon.Baton:
                    if (hit)
                    {
                        audioHandler.PlayClip(PlayerSounds.Hit, overrideSound);
                    }
                    else
                    {
                        audioHandler.PlayClip(PlayerSounds.Miss, overrideSound);
                    }
                    break;
                case Weapon.CandyCane:
                    if (hit)
                    {
                        audioHandler.PlayClip(PlayerSounds.Hit, overrideSound);
                    }
                    else
                    {
                        audioHandler.PlayClip(PlayerSounds.Miss, overrideSound);
                    }
                    break;
                case Weapon.Cannon:
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
            if(GetAnimationName().Contains("hit") || GetAnimationName().Contains("crouch"))
            {
                return;
            }
            
            if(currentState == State.Stunned)
            {
                return;
            }
            if (Input.GetAxisRaw("Attack") == 1)
            {
                Debug.DrawRay(rb.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), reachDistance);
                if (weapon != Weapon.Cannon)
                {
                    if (hit.collider != null && hit.collider.CompareTag("thief") && hit.collider.gameObject == TargetedThief)
                    {
                        StartAttackSound(true, true);
                        TargetedThief.GetComponent<ThiefController>().StartStun();
						thiefStunned += 1;
                        TargetedThief = null;
                        thiefSpawner.Spawn();
                        if (hp == 1)
                        {
                            PowerUp.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        StartAttackSound(false, false);
                    }
                }
                else if(!GetAnimationName().Contains("cannon"))
                {
                    StartAttackSound(false, true);
                    float addToX = GetComponent<SpriteRenderer>().flipX ? 0.59f : -0.59f;
                    var candy = Instantiate(christmasCandy, new Vector3(transform.position.x + addToX, transform.position.y + 1f, 0), new Quaternion(0, 0, 0, 0), null);
                    candy.SetActive(true);
                    candy.GetComponent<ChristmasCandyBehaviour>().Shoot(new Vector3(transform.position.x + addToX, transform.position.y + 1f, 0), Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                ChangeState(State.Attack);
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
            if (rb.velocity != new Vector2(0, 0) && currentState != State.Shielded)
            {
                if (horizontal != 0)
                {
                    GetComponent<SpriteRenderer>().flipX = horizontal != -1;
                }
                ChangeState(State.Move);
                animator.SetBool("isMoving", true);
            }
            else if(currentState != State.Stunned && currentState != State.Shielded)
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
                TargetedThief == null || !TargetedThief.GetComponent<ThiefController>().IsActive || 
                currentState == State.Stunned)
            {
                return;
            }
            hp -= 1;
            animator.SetBool("isStunned", true);
            ChangeState(State.Stunned);
            StopMovement();
            if(hp == 0)
            {
                Invoke(nameof(StartHospital), 2);
            }
            Invoke(nameof(EndStun), 2);
        }

        void StartHospital()
        {
            GameObject.Find("ScriptHandler").GetComponent<RoundOver>().ShowHospitalPanel();
        }

        public void InvokeStun(float seconds, bool ableToDefend, bool checkDistance)
        {
            if (shield > 0)
            {
                shield -= 1;
                ChangeState(State.Shielded);
                Invoke(nameof(EndStun), 0.5f);
                return;
            }
            if (currentState == State.Shielded) 
            {
                return;
            }
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
            if(TargetedThief != null)
            {
                return Vector2.Distance(gameObject.transform.position, TargetedThief.transform.position) < maxDistance;
            }
            else
            {
                return false;
            }
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "thief")
            {
                if (collision.gameObject.GetComponent<ThiefController>().IsActive)
                {
                    TargetedThief = collision.gameObject;
                    TargetedThief.GetComponent<ThiefController>().Thief.IsTargeted = true;
                }
            }
            if(collision.tag == "PowerUp")
            {
                collision.gameObject.SetActive(false);
                hp++;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "thief")
            {
                if (collision.gameObject.GetComponent<ThiefController>().IsActive && !collision.gameObject.GetComponent<ThiefController>().Thief.IsTargeted)
                {
                    TargetedThief = collision.gameObject;
                    TargetedThief.GetComponent<ThiefController>().Thief.IsTargeted = true;
                }
            }
        }
    }
}
