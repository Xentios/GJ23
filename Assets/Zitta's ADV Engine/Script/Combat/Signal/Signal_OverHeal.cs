using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_OverHeal : Signal_Heal {

        public override void EndEffect()
        {
            if (GetKey("Heal") < 0)
                SetKey("Heal", 0);

            if (!Target || !Target.CombatActive())
                return;

            float Value = GetKey("Heal");

            if (Target.GetLife() + Value > Target.GetMaxLife())
                Target.SetMaxLife(Target.GetLife() + Value);

            Target.ChangeLife(Value);

            if (GetKey("NumberEffect") == 1)
                UIControl.Main.NumberEffect("Heal", ((int)Value).ToString(), Target);
        }

        public override void CommonKeys()
        {
            base.CommonKeys();
        }
    }
}