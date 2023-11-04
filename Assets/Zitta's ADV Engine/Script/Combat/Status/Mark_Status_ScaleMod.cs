using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_ScaleMod : Mark_Status_Mod {

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Scale"))
            {
                S.SetKey("Scale", S.GetKey("Scale") * GetKey("ScaleMod"));
            }
            base.OutputSignal(S);
        }

        public override void CommonKeys()
        {
            // "ScaleMod": Scale multiplier
            base.CommonKeys();
        }
    }
}