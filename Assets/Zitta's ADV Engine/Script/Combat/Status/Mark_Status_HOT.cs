using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_HOT : Mark_Status {

        public override void TimePassed(float Value)
        {
            if (Source.CombatActive())
            {
                Source.ChangeLife(GetKey("Heal") * Value);
            }
            base.TimePassed(Value);
        }

        public override void Stack(Mark_Status M)
        {
            StackDuration(M);
        }

        public override void CommonKeys()
        {
            // Heal: Heal amount per second
            base.CommonKeys();
        }
    }
}