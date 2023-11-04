using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Heal : Signal {

        public override void StartEffect()
        {
            if (HasKey("Variance"))
                SetKey("Heal", GetKey("Heal") * Random.Range(1 - GetKey("Variance"), 1 + GetKey("Variance")));
            SetKey("Heal", GetHealValue(GetKey("Heal")));
        }

        public override void EndEffect()
        {
            if (GetKey("Heal") < 0)
                SetKey("Heal", 0);
            SetKey("Heal", StaticAssign.RoundUp(GetKey("Heal")));

            if (!Target || !Target.CombatActive())
                return;

            Target.ChangeLife(GetKey("Heal"));

            if (GetKey("NumberEffect") == 1)
                UIControl.Main.NumberEffect("Heal", ((int)GetKey("Heal")).ToString(), Target);
        }

        public virtual float GetHealValue(float Base)
        {
            return Base;
        }

        public override void CommonKeys()
        {
            // "Heal": Final healing value
            // "Variance": Healing variance scale
            // "NumberEffect": Whether this signal should create number effects
            base.CommonKeys();
        }
    }
}