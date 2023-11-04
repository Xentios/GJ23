using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_HealingOutputMod : Mark_Status_Mod {

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Heal"))
            {
                S.SetKey("Heal", S.GetKey("Heal") * GetFinalMod());
                if (HasKey("TriggerCount"))
                {
                    ChangeKey("TriggerCount", -1);
                    if (GetKey("TriggerCount") <= 0)
                        Source.RemoveStatus(this);
                }
            }
            base.OutputSignal(S);
        }

        public float GetFinalMod()
        {
            float a = GetKey("HealMod");
            if (HasKey("Stack") && HasKey("StackChange"))
                a += GetKey("Stack") * GetKey("StackChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "HealMod": Heal multiply rate
            // "StackChange": Heal multiplier change per stack
            // "TriggerCount": Remaining trigger count
            base.CommonKeys();
        }
    }
}