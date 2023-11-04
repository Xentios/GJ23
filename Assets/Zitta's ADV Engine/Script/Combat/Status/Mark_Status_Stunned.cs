using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Stunned : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Stunned")
                return Value + GetKey("Stack");
            return base.PassValue(Key, Value);
        }

        public override void Stack(Mark_Status M)
        {
            StackCount(M);
        }

        public void UpdateCount()
        {
            if (GetKey("Stack") <= 0)
            {
                CombatRemove();
                Source.RemoveStatus(this);
            }
        }

        public override void CommonKeys()
        {
            // "Stunned": Whether the card is stunned
            base.CommonKeys();
        }
    }
}