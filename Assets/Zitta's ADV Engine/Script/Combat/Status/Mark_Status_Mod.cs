using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ADV
{
    public class Mark_Status_Mod : Mark_Status {
        public List<string> RequiredKeys;
        public List<string> AvoidedKeys;

        public virtual bool Trigger(Signal S)
        {
            bool T = false;
            if (RequiredKeys.Count <= 0)
                T = true;
            foreach (string s in RequiredKeys)
                if (S.HasKey(s))
                    T = true;
            foreach (string s in AvoidedKeys)
                if (S.HasKey(s))
                    T = false;
            return T && ConditionPass();
        }

        public bool ConditionPass()
        {
            if (GetKey("AddCondition") == 1)
            {
                List<Condition> Conditions = new List<Condition>();
                foreach (Condition Con in GetComponentsInChildren<Condition>())
                    if (Con.transform.parent == transform)
                        Conditions.Add(Con);
                foreach (Condition con in Conditions)
                {
                    if (!con.Pass(Source))
                        return false;
                }
            }
            if (GetKey("RequireCaster") == 1)
            {
                if (!Caster || !Caster.CombatActive())
                    return false;
            }
            return true;
        }

        public override void CommonKeys()
        {
            // "RequireCaster": Whether to only active when caster is alive
            // "AddCondition": Whether there are additional conditions for triggering
            base.CommonKeys();
        }
    }
}