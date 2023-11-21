using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_CDMod : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "CDMod")
                return Value * GetKey("CDMod");
            return base.PassValue(Key, Value);
        }
    }
}