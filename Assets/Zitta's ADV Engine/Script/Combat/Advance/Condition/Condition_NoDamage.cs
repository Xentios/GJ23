using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_NoDamage : Condition {

        public override bool Pass(Card Source)
        {
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                Card C = CombatControl.Main.Cards[i];
                if (C.GetSide() == Source.GetSide() && C.CombatActive() && C.GetKey("NoDamage") == 0)
                    return false;
            }
            return true;
        }
    }
}