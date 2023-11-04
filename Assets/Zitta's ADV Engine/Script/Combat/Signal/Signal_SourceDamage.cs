using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_SourceDamage : Signal {

        public override void StartEffect()
        {
            if (GetKey("BaseScaling") == 1 && Source)
                SetKey("Value", GetKey("Value") * Source.GetBaseDamage());
            if (HasKey("ChangeTo"))
                SetKey("Value", GetKey("ChangeTo") - Target.GetSourceDamage());
            if (GetKey("LifeScaling") == 1 && Source)
                SetKey("Value", GetKey("Value") * Source.GetLife());
            SetKey("Value", GetFinalValue(GetKey("Value")));
        }

        public override void EndEffect()
        {
            SetKey("Value", StaticAssign.RoundUp(GetKey("Value")));

            if (!Target || !Target.CombatActive())
                return;

            Target.ChangeBaseDamage(GetKey("Value"));

            if (GetKey("NumberEffect") == 1)
            {
                if (GetKey("Value") >= 0)
                    UIControl.Main.NumberEffect("DamageUp", ((int)GetKey("Value")).ToString(), Target);
                else
                    UIControl.Main.NumberEffect("DamageDown", ((int)GetKey("Value")).ToString(), Target);
            }
        }

        public virtual float GetFinalValue(float Base)
        {
            return Base;
        }

        public override void CommonKeys()
        {
            // "Value": Final source damage change
            // "BaseScaling": Whether the heal is affected by Base Damage
            // "LifeScaling": Whether the heal is affected by source's life
            // "ChangeTo": Value to change to
            // "NumberEffect": Whether this signal should create number effects
            base.CommonKeys();
        }
    }
}