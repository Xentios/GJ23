using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_HealReduction : Mark_Status_Mod {

        public override void InputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Heal"))
                S.SetKey("Heal", S.GetKey("Heal") * (1 - GetKey("HealReduction")));
            base.InputSignal(S);
        }

        public override void CommonKeys()
        {
            // "HealReduction": Healing reduction rate
            base.CommonKeys();
        }
    }
}