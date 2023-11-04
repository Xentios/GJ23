using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_MoveTowards : Signal {
        public List<GameObject> StatusPrefabs;

        public override void EndEffect()
        {
            if (!Target)
                return;
            List<string> AddKeys = new List<string>();
            AddKeys.Add(KeyBase.Compose("TargetX", Position.x));
            AddKeys.Add(KeyBase.Compose("TargetY", Position.y));
            for (int i = 0; i < StatusPrefabs.Count; i++)
            {
                if (GetKey("AddToSource") == 1)
                    Source.AddStatus(StatusPrefabs[i].GetComponent<Mark_Status>(), AddKeys, Source);
                else
                    Target.AddStatus(StatusPrefabs[i].GetComponent<Mark_Status>(), AddKeys, Source);
            }
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "AddToSource": Whether to always add movement to source
            base.CommonKeys();
        }
    }
}