using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_StatMod_Hero : Mark_Status_StatMod {

        public override float GetAddDamage()
        {
            float V = base.GetAddDamage();
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                Card C = CombatControl.Main.Cards[i];
                if (!C || !C.CombatActive())
                    continue;

                if (C.HasKey("HeroDamage"))
                    V += C.GetKey("HeroDamage");
            }
            return V;
        }
    }
}