using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DefeatLock : Mark_Status {
        public List<GameObject> Conditions;

        public override float PassValue(string Key, float Value)
        {
            if (Key == "DefeatLock")
            {
                foreach (GameObject G in Conditions)
                {
                    Condition Con = G.GetComponent<Condition>();
                    if (!Con.Pass(Source))
                        return Value;
                }
                return 1;
            }
            return base.PassValue(Key, Value);
        }
    }
}