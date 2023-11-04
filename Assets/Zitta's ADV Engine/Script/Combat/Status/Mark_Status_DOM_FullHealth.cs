using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DOM_FullHealth : Mark_Status_DamageOutputMod {

        public override bool Trigger(Signal S)
        {
            if (!S.Source || S.Source.GetLife() < S.Source.GetMaxLife())
                return false;
            return base.Trigger(S);
        }
    }
}