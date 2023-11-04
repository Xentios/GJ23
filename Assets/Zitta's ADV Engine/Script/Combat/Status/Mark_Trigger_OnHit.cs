using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_OnHit : Mark_Trigger {
        public List<string> RequiredKeys;
        public List<string> AvoidedKeys;

        public override void ReturnSignal(Signal S)
        {
            if (!S.Target)
                return;
            if (S.HasKey("Damage") && S.GetKey("Damage") > 0 && Trigger(S))
            {
                float Chance = GetChance();
                if (S.HasKey("ProcChance"))
                    Chance *= S.GetKey("ProcChance");
                TryTrigger(S.Target, Chance);
            }
            base.ReturnSignal(S);
        }

        public virtual void TryTrigger(Card Target, float Chance)
        {
            float a = Random.Range(0.001f, 0.999f);
            if (a <= Chance)
            {
                if (!HasKey("Target") || GetKey("Target") == 1)
                    OnTrigger(Target);
                else
                    OnTrigger(Source);
            }
        }

        public virtual float GetChance()
        {
            float a = 1;
            if (HasKey("Chance"))
                a = GetKey("Chance");
            return a;
        }

        public virtual bool Trigger(Signal S)
        {
            bool T = false;
            if (RequiredKeys.Count <= 0)
                T = true;
            foreach (string s in RequiredKeys)
                if (S.GetKey(s) > 0)
                    T = true;
            foreach (string s in AvoidedKeys)
                if (S.GetKey(s) > 0)
                    T = false;
            return T;
        }

        public override void CommonKeys()
        {
            // "Chance": Trigger chance
            // "Target": Whether to trigger on signal target or source
            base.CommonKeys();
        }
    }
}