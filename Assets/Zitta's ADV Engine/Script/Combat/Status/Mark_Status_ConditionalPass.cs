using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_ConditionalPass : Mark_Status {
        public string TargetKey;
        public List<GameObject> Conditions;

        public override float PassValue(string Key, float Value)
        {
            if (Key == TargetKey)
            {
                bool ConditionPass = true;
                foreach (GameObject G in Conditions)
                {
                    Condition C = G.GetComponent<Condition>();
                    if (!C.Pass(Source))
                        ConditionPass = false;
                }
                if (ConditionPass)
                    return GetKey("PositiveValue");
                else
                    return GetKey("NegativeValue");
            }
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "PositiveValue": Return value if conditions passed
            // "NegativeValue": Return value if conditions not passed
            base.CommonKeys();
        }
    }
}