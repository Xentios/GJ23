using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DOM_ItemCount : Mark_Status_DamageOutputMod {
        public List<string> ItemKeys;

        public override float GetAddDamage()
        {
            float AddDamage = 0;
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                if (ItemKeys.Contains(CombatControl.Main.Cards[i].GetID()))
                    AddDamage += CombatControl.Main.Cards[i].GetLife();
            }
            return AddDamage;
        }
    }
}