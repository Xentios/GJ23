using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Switch : Condition {
        public string Key;
        public int Value;

        public override bool Pass(KeyBase KB)
        {
            return KB.GetKey(Key) == Value;
        }
    }
}