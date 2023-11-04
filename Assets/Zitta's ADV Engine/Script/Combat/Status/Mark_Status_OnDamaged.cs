using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_OnDamaged : Mark_Status_Mod {
        public List<GameObject> SignalPrefabs;

        public override void InputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Damage"))
            {
                for (int i = 0; i < SignalPrefabs.Count; i++)
                    SendSignal(SignalPrefabs[i]);
            }
            base.InputSignal(S);
            if (GetKey("Removed") != 0)
                Source.RemoveStatus(this);
        }

        public override void CommonKeys()
        {
            // "Removed": Whether the status should be removed after triggering
            base.CommonKeys();
        }
    }
}