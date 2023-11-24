using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thief
{
    public class GrinchGery : ThiefBase
    {
        GameObject gift;
        public GrinchGery(float speed, ThiefType thiefType, GameObject gift) : base(speed, thiefType)
        {
            this.gift = gift;
        }

        public override void SpecialAttack(Vector2 position)
        {
            var giftClone = GameObject.Instantiate(gift);
            giftClone.GetComponent<GiftFall>().StartFall(position, Random.Range(0, 100) > 50 ? "blue" : "pink");
        }
    }
}

