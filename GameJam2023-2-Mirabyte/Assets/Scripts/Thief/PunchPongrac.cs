using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Thief
{
    public class PunchPongrac : ThiefBase
    {
        Animator animator;
        SpriteRenderer renderer;
        GameObject Player;
        Rigidbody2D rb;
        public PunchPongrac(float speed, ThiefType thiefType, Animator animator, SpriteRenderer renderer, Rigidbody2D rb) : base(speed, thiefType)
        {
            this.animator = animator;
            this.renderer = renderer;
            Player = GameObject.Find("Player");
            this.rb = rb;
        }
        public override void SpecialAttack()
        {
            Vector2 direction = (new Vector2(Player.transform.position.x, Player.transform.position.y) - new Vector2(rb.transform.position.x, rb.transform.position.y)).normalized;
            if (Vector2.Distance(Player.transform.position, rb.transform.position) < 1.5f && Player.GetComponent<PlayerBehaviour>().GetState() != State.Stunned && Player.GetComponent<PlayerBehaviour>().GetState() != State.Shielded)
            {
                renderer.flipX = direction.x > 0;
                CanMove = false;
                animator.SetTrigger("Hit");
                Player.GetComponent<PlayerBehaviour>().InvokeStun(0.3f, true, true);
            }
            else if(!animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("hit"))
            {
                CanMove = true;
            }
        }

        public override void Stun()
        {
            animator.SetTrigger("Stunned");
        }
    }
}

