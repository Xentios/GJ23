using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_PassValue : Condition {
        public string Key;

        public override bool Pass(Card Source)
        {
            float a = Source.PassValue(Key, 0);
            return ((a >= GetKey("MinValue") || !HasKey("MinValue")) && (a <= GetKey("MaxValue") || !HasKey("MaxValue"))) == (GetKey("Positive") == 1);
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass only when value is in range
            // "MaxValue": Max value allowed (inclusive)
            // "MinValue": Min value allowed (inclusive)
            base.CommonKeys();
        }
    }
}