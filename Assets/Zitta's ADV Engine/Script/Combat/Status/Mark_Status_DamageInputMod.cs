using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DamageInputMod : Mark_Status_Mod {

        public override void InputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Damage"))
            {
                float Value = S.GetKey("Damage") * GetFinalMod() + GetKey("DamageValue");
                if (Value < 0)
                    Value = 0;
                S.SetKey("Damage", Value);
                if (HasKey("TriggerCount"))
                {
                    ChangeKey("TriggerCount", -1);
                    if (GetKey("TriggerCount") <= 0)
                        Source.RemoveStatus(this);
                }
            }
            base.InputSignal(S);
        }

        public float GetFinalMod()
        {
            float a = GetKey("DamageMod");
            if (HasKey("Stack") && HasKey("StackChange"))
                a += GetKey("Stack") * GetKey("StackChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "DamageMod": Damage multiply rate
            // "DamageValue": Damage value change
            // "StackChange": Damage multiplier change per stack
            // "TriggerCount": Remaining trigger count
            base.CommonKeys();
        }
    }
}