using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DOM_SourceCondition : Mark_Status_DamageOutputMod {
        public List<GameObject> Conditions;

        public override bool Trigger(Signal S)
        {
            if (!Source)
                return false;
            foreach (GameObject G in Conditions)
            {
                if (!G.GetComponent<Condition>().Pass(Source))
                    return false;
            }
            return base.Trigger(S);
        }
    }
}