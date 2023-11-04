using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_OnHit_TargetKey : Mark_Trigger_OnHit {
        public string TargetKey;

        public override bool Trigger(Signal S)
        {
            return base.Trigger(S) && S.Target && S.Target.GetKey(TargetKey) == 1;
        }
    }
}