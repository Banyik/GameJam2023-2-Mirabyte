using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thief
{
    public class GrinchGery : ThiefBase
    {
        GameObject gift;
        Rigidbody2D rb;
        Animator animator;
        public GrinchGery(float speed, ThiefType thiefType, GameObject gift, Rigidbody2D rb, Animator animator) : base(speed, thiefType)
        {
            this.gift = gift;
            this.rb = rb;
            this.animator = animator;
        }

        public override void SpecialAttack()
        {
            var giftClone = GameObject.Instantiate(gift);
            giftClone.GetComponent<GiftFall>().StartFall(rb.position, Random.Range(0, 100) > 50 ? "blue" : "pink");
        }

        public override void Stun() 
        {
        
        }
    }
}

