using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thief
{
    public class ThiefController : MonoBehaviour
    {
        public ThiefType thiefType;
        public float speed;
        public Rigidbody2D rb;
        public Animator animator;
        ThiefBase thief;

        public Vector2 RightEndPointTarget;
        public Vector2 LeftEndPointTarget;

        public GameObject Player;
        public GameObject gift;

        public RuntimeAnimatorController GrinchAnimator;
        public RuntimeAnimatorController PongracAnimator;
        public RuntimeAnimatorController JulcsikaAnimator;

        bool isActive = true;

        public bool IsActive { get => isActive; set => isActive = value; }
        public ThiefBase Thief { get => thief; set => thief = value; }

        private void Start()
        {
            switch (thiefType)
            {
                case ThiefType.PunchPongrac:
                    thief = new PunchPongrac(speed, thiefType, animator, GetComponent<SpriteRenderer>(), rb);
                    animator.runtimeAnimatorController = PongracAnimator;
                    break;
                case ThiefType.Julcsika:
                    thief = new PunchPongrac(speed - 0.5f, thiefType, animator, GetComponent<SpriteRenderer>(), rb);
                    animator.runtimeAnimatorController = JulcsikaAnimator;
                    break;
                case ThiefType.GrinchGery:
                    thief = new GrinchGery(speed - 0.25f, thiefType, gift, rb, animator);
                    animator.runtimeAnimatorController = GrinchAnimator;
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if (thief.IsTargeted && isActive)
            {
                switch (thiefType)
                {
                    case ThiefType.PunchPongrac:
                        MoveToExit();
                        if (Random.Range(0, 100) < 50) thief.SpecialAttack();
                        break;
                    case ThiefType.Julcsika:
                        MoveToExit();
                        if (Random.Range(0, 100) < 50) thief.SpecialAttack();
                        break;
                    case ThiefType.GrinchGery:
                        MoveToExit();
                        if (Random.Range(0, 500) < 5) thief.SpecialAttack();
                        break;
                    default:
                        break;
                }
            }
            else if (isActive)
            {
                switch (thiefType)
                {
                    case ThiefType.PunchPongrac:
                        if (Random.Range(0, 100) < 5) thief.Rage();
                        break;
                    case ThiefType.Julcsika:
                        if (Random.Range(0, 1500) < 50) thief.Rage();
                        break;
                    case ThiefType.GrinchGery:
                        break;
                    default:
                        break;
                }
            }
        }

        void SetTarget()
        {
            if(Vector2.Distance(Player.transform.position, LeftEndPointTarget) > Vector2.Distance(gameObject.transform.position, LeftEndPointTarget))
            {
                thief.ExitTarget = LeftEndPointTarget;
            }
            else
            {
                thief.ExitTarget = RightEndPointTarget;
            }
        }

        void MoveToExit()
        {
            if(thief.ExitTarget == Vector2.zero)
            {
                SetTarget();
            }
            Vector2 direction = (new Vector2(thief.ExitTarget.x, thief.ExitTarget.y) - new Vector2(rb.transform.position.x, rb.transform.position.y)).normalized;
            if(Vector2.Distance(thief.ExitTarget, transform.position) >= 0.1 && thief.CanMove)
            {
                transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
                GetComponent<SpriteRenderer>().flipX = direction.x > 0;
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
                if(Vector2.Distance(thief.ExitTarget, transform.position) <= 0.1)
                {
                    GameObject.Find("ScriptHandler").GetComponent<ThiefSpawner>().Spawn(); 
                    Destroy(gameObject);
                }
            }
        }

        public void StartStun()
        {
            thief.Stun();
            isActive = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

