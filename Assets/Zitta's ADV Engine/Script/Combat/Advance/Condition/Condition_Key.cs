using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Key : Condition {
        public string KeyName;

        public override bool Pass(Card Source)
        {
            return Source.GetKey(KeyName) >= GetKey("MinValue") && Source.GetKey(KeyName) <= GetKey("MaxValue");
        }

        public override void CommonKeys()
        {
            // "MinValue": Minimum value to pass (inclusive)
            // "MaxValue": Maximum value to pass (inclusive)
            base.CommonKeys();
        }
    }
}