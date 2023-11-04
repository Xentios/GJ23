using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_Instant : Medium {

        public override void EffectUpdate(float Value)
        {
            /*if (!Target)
            {
                SetKey("Delay", 0);
                Effect(null);
                EndEffect();
                return;
            }*/

            ChangeKey("Delay", -Value);
            //SetKey("PositionX", Target.GetPosition().x);
            //SetKey("PositionY", Target.GetPosition().y);
            if (GetKey("Delay") <= 0)
            {
                Effect(Target);
                EndEffect();
            }
        }

        public override void CommonKeys()
        {
            // "Delay": The delay before final effect
            base.CommonKeys();
        }
    }
}