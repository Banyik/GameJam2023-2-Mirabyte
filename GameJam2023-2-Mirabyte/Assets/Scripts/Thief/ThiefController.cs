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
        private void Start()
        {
            switch (thiefType)
            {
                case ThiefType.PunchPongrac:
                    break;
                case ThiefType.Julcsika:
                    break;
                case ThiefType.GrinchGery:
                    thief = new GrinchGery(3, thiefType, gift);
                    animator.runtimeAnimatorController = GrinchAnimator;
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if (thief.IsTargeted)
            {
                MoveToExit();
                if(Random.Range(0, 500) < 1) thief.SpecialAttack(transform.position);
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
            if(Vector2.Distance(thief.ExitTarget, transform.position) >= 0.1)
            {
                transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
                GetComponent<SpriteRenderer>().flipX = direction.x > 0;
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }
}

