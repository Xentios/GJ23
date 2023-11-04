using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DelayMod : Mark_Status_Mod {

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("DelayScale"))
            {
                S.SetKey("DelayScale", S.GetKey("DelayScale") * GetKey("DelayMod"));
            }
            if (Trigger(S) && S.HasKey("Delay"))
            {
                S.SetKey("Delay", S.GetKey("Delay") * GetKey("DelayMod"));
            }
            base.OutputSignal(S);
        }

        public override void CommonKeys()
        {
            // "DelayMod": Delay multiplier
            base.CommonKeys();
        }
    }
}