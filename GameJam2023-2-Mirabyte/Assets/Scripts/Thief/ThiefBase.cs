using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thief
{
    public class ThiefBase
    {
        float speed;
        ThiefType thiefType;
        bool isTargeted;
        Vector2 exitTarget;
        public ThiefBase(float speed, ThiefType thiefType)
        {
            this.speed = speed;
            this.thiefType = thiefType;
            this.isTargeted = true;
            this.exitTarget = Vector2.zero;
        }

        public virtual void SpecialAttack(Vector2 position) { }

        public float Speed { get => speed; set => speed = value; }
        public ThiefType ThiefType { get => thiefType; set => thiefType = value; }
        public bool IsTargeted { get => isTargeted; set => isTargeted = value; }
        public Vector2 ExitTarget { get => exitTarget; set => exitTarget = value; }
    }
}

