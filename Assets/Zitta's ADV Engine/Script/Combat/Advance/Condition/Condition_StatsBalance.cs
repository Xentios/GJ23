using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_StatsBalance : Condition {

        public override bool Pass(Card Source)
        {
            if (GetKey("HigherDamage") == 1)
                return Source.GetBaseDamage() > Source.GetMaxLife();
            if (GetKey("HigherLife") == 1)
                return Source.GetMaxLife() > Source.GetBaseDamage();
            return false;
        }

        public override void CommonKeys()
        {
            // "HigherDamage": Whether to pass if damage is higher than max life
            // "HigherLife": Whether to pass if max life is higher than damage
            base.CommonKeys();
        }
    }
}