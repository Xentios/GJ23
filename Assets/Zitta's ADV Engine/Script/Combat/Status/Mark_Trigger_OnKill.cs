using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_OnKill : Mark_Trigger {
        public List<string> RequiredKeys;

        public override void ReturnSignal(Signal S)
        {
            if (!S.Target)
                return;
            if (S.Target == Source && GetKey("IgnoreSource") == 1)
                return;
            bool RequiredPass = false;
            if (RequiredKeys.Count <= 0)
                RequiredPass = true;
            foreach (string s in RequiredKeys)
            {
                if (S.GetKey(s) > 0)
                    RequiredPass = true;
            }
            if (!RequiredPass)
                return;
            if (S.HasKey("Damage") && S.GetKey("Damage") > 0 && S.Target.GetLife() <= 0 && S.GetKey("KillingHit") == 1)
            {
                if (!HasKey("Target") || GetKey("Target") == 1)
                    OnTrigger(S.Target);
                else
                    OnTrigger(Source);
            }
            base.ReturnSignal(S);
        }

        public override void CommonKeys()
        {
            // "IgnoreSource": Whether to ignore a self kill
            // "Target": Whether to trigger on signal target or source
            base.CommonKeys();
        }
    }
}