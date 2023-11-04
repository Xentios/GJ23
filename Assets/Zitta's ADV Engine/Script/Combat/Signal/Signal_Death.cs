using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Death : Signal {

        public override void EndEffect()
        {
            if (!Target || Target.AlreadyDead || (GetKey("CombatActive") > 0 && !Target.CombatActive()))
                return;
            Target.Death();
            Target.SetLife(0);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "CombatActive": Whether to ignore inactive cards
            base.CommonKeys();
        }
    }
}