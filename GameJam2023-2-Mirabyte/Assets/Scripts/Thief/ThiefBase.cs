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
        bool canMove;
        Vector2 exitTarget;
        public ThiefBase(float speed, ThiefType thiefType)
        {
            this.speed = speed;
            this.thiefType = thiefType;
            this.isTargeted = false;
            this.canMove = true;
            this.exitTarget = Vector2.zero;
        }

        public virtual void SpecialAttack() { }
        public virtual void Rage() { }
        public virtual void Stun() { }

        public float Speed { get => speed; set => speed = value; }
        public ThiefType ThiefType { get => thiefType; set => thiefType = value; }
        public bool IsTargeted { get => isTargeted; set => isTargeted = value; }
        public Vector2 ExitTarget { get => exitTarget; set => exitTarget = value; }
        public bool CanMove { get => canMove; set => canMove = value; }
    }
}

