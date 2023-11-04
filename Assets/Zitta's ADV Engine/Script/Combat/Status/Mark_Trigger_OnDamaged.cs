using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_OnDamaged : Mark_Trigger {
        public List<string> RequiredKeys;
        public List<string> AvoidedKeys;

        public override void ConfirmSignal(Signal S)
        {
            if (!S.Source)
                return;
            if (S.HasKey("Damage") && S.GetKey("Damage") > 0 && Trigger(S))
            {
                float Chance = GetChance();
                if (S.HasKey("ProcChance"))
                    Chance *= S.GetKey("ProcChance");
                if (GetKey("Target") == 1)
                    TryTrigger(S.Source, Chance);
                else if (GetKey("Target") == 0)
                    TryTrigger(Source, Chance);
            }
            base.ReturnSignal(S);
        }

        public virtual void TryTrigger(Card Target, float Chance)
        {
            float a = Random.Range(0.001f, 0.999f);
            if (a <= Chance)
            {
                OnTrigger(Source);
            }
        }

        public virtual float GetChance()
        {
            float a = 1;
            if (HasKey("Chance"))
                a = GetKey("Chance");
            if (HasKey("Count"))
                a += GetKey("StackChange") * GetKey("Count");
            if (HasKey("ItemCount") && GetKey("ItemCountScaling") == 1)
                a += GetKey("StackChange") * GetKey("ItemCount");
            return a;
        }

        public virtual bool Trigger(Signal S)
        {
            bool T = true;
            if (RequiredKeys.Count > 0)
                T = false;
            foreach (string s in RequiredKeys)
                if (S.GetKey(s) > 0)
                    T = true;
            foreach (string s in AvoidedKeys)
                if (S.GetKey(s) > 0)
                    T = false;
            if (GetKey("AddCondition") == 1)
            {
                Card C;
                if (GetKey("Target") == 1)
                    C = S.Source;
                else
                    C = Source;
                List<Condition> Conditions = new List<Condition>();
                foreach (Condition Con in GetComponentsInChildren<Condition>())
                    Conditions.Add(Con);
                foreach (Condition con in Conditions)
                {
                    if (!con.Pass(C))
                        T = false;
                }
            }
            return T;
        }

        public override void CommonKeys()
        {
            // "Target": Whether to trigger on signal's source or trigger's source
            // "Chance": Trigger chance
            // "StackChange": Chance change per stack
            // "ItemCountScaling": Whether the proc chance should scale with "ItemCount"
            base.CommonKeys();
        }
    }
}