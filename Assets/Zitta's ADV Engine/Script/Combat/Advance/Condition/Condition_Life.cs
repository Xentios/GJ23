using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Life : Condition {

        public override bool Pass(Card Source)
        {
            if (HasKey("MaxLife") || HasKey("MinLife"))
            {
                float l = Source.GetLife();
                return ((l >= GetKey("MinLife") || !HasKey("MinLife")) && (l <= GetKey("MaxLife") || !HasKey("MaxLife"))) == (GetKey("Positive") != 0);
            }
            float a = Source.GetLife() / Source.GetMaxLife();
            return ((a >= GetKey("MinLifeRate") || !HasKey("MinLifeRate")) && (a <= GetKey("MaxLifeRate") || !HasKey("MaxLifeRate"))) == (GetKey("Positive") != 0);
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass only when value is in range
            // "MaxLife": Max life allowed (inclusive)
            // "MinLife": Min life allowed (inclusive)
            // "MaxLifeRate": Max life rate allowed (inclusive)
            // "MinLifeRate": Min life rate allowed (inclusive)
            base.CommonKeys();
        }
    }
}