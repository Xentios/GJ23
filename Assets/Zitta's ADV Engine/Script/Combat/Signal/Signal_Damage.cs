using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage : Signal {

        public override void StartEffect()
        {
            if (!Target)
                return;
            if (HasKey("Variance"))
                SetKey("Damage", GetKey("Damage") * Random.Range(1 - GetKey("Variance"), 1 + GetKey("Variance")));
            SetKey("Damage", GetDamageValue(GetKey("Damage")));
            if (!HasKey("DamageScale"))
                SetKey("DamageScale", 1);
        }

        public override void EndEffect()
        {
            if (GetKey("Damage") < 0)
                SetKey("Damage", 0);
            SetKey("Damage", GetKey("Damage") * GetKey("DamageScale"));
            //SetKey("Damage", StaticAssign.RoundUp(GetKey("Damage")));

            if (!Target || !Target.CombatActive())
                return;

            bool CanKill = true;
            if (Target.GetLife() <= 0)
                CanKill = false;

            Target.ChangeLife(-GetKey("Damage"));

            if (Target.GetLife() <= 0 && CanKill)
                SetKey("KillingHit", 1);

            if (GetKey("NumberEffect") == 1)
            {
                UIControl.Main.NumberEffect("Damage", ((int)GetKey("Damage")).ToString(), Target);
            }
        }

        public virtual float GetDamageValue(float Base)
        {
            return Base;
        }

        public override void CommonKeys()
        {
            // "Damage": Final damage value
            // "Variance": damage variance scale
            // "DamageScale": damage scale from additive damage mod
            // "MainHit": Whether the damage is the main trigger damage
            // "SubHit": Whether the damage is the sub trigger damage
            // "NumberEffect": Whether this signal should create number effects
            // "KillingHit": Whether this signal is the killing hit
            base.CommonKeys();
        }
    }
}