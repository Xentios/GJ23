using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_SummonLifeMod : Mark_Status_Mod {

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S) && S.GetComponent<Signal_Summon>() && S.HasKey("OverrideLife"))
            {
                S.SetKey("OverrideLife", S.GetKey("OverrideLife") * GetKey("LifeMod"));
            }
            base.OutputSignal(S);
        }

        public override void CommonKeys()
        {
            // "LifeMod": Summon life multiplier
            base.CommonKeys();
        }
    }
}