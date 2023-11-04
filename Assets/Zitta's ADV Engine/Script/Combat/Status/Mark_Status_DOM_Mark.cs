using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DOM_Mark : Mark_Status_DamageOutputMod {

        public override bool Trigger(Signal S)
        {
            if (!S.Target || !S.Target.HasMark(StaticAssign.GetLockKey()))
                return false;
            return base.Trigger(S);
        }
    }
}