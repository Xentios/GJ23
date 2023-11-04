using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DeathEffect : Mark_Status {
        public List<GameObject> DeathSignals;

        public override void Death()
        {
            if (!Source)
                return;
            List<string> AddKeys = new List<string>();
            foreach (string s in KB.Keys)
                AddKeys.Add(s);
            if (HasKey("ItemCount"))
                AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("ItemCount")));
            for (int i = 0; i < DeathSignals.Count; i++)
                Source.SendSignal(DeathSignals[i], AddKeys, Source, Source.GetPosition());
            base.Death();
        }
    }
}