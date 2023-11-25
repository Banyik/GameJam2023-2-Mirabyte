using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thief
{
    public class GrinchGery : ThiefBase
    {
        GameObject gift;
        Rigidbody2D rb;
        public GrinchGery(float speed, ThiefType thiefType, GameObject gift, Rigidbody2D rb) : base(speed, thiefType)
        {
            this.gift = gift;
            this.rb = rb;
        }

        public override void SpecialAttack()
        {
            var giftClone = GameObject.Instantiate(gift);
            giftClone.GetComponent<GiftFall>().StartFall(rb.position, Random.Range(0, 100) > 50 ? "blue" : "pink");
        }
    }
}

