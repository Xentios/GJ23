using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger : Mark_Status {
        public List<GameObject> MainSignals;

        public override void TimePassed(float Value)
        {
            if (HasKey("CoolDown"))
                ChangeKey("CCD", -Value);
            base.TimePassed(Value);
        }

        public virtual bool OnTrigger(Card Target)
        {
            if (!CanTrigger(Target))
                return false;

            List<string> AddKeys = new List<string>();
            if (Target)
            {
                AddKeys.Add(KeyBase.Compose("TargetPositionX", Target.GetPosition().x));
                AddKeys.Add(KeyBase.Compose("TargetPositionY", Target.GetPosition().y));
            }
            if (HasKey("ItemCount"))
                AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("ItemCount")));
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, Target, Target.GetPosition());

            if (HasKey("TriggerCount"))
            {
                ChangeKey("TriggerCount", -1);
                if (GetKey("TriggerCount") <= 0)
                {
                    CombatRemove();
                    Source.RemoveStatus(this);
                }
            }

            if (HasKey("CoolDown"))
                SetKey("CCD", GetKey("CoolDown"));

            return true;
        }

        public virtual bool CanTrigger(Card Target)
        {
            if (GetKey("AddCondition") == 1)
            {
                List<Condition> Conditions = new List<Condition>();
                foreach (Condition Con in GetComponentsInChildren<Condition>())
                    if (Con.transform.parent == transform)
                        Conditions.Add(Con);
                foreach (Condition con in Conditions)
                {
                    if (GetKey("SourceCondition") == 1 || con.GetKey("SourceCondition") == 1)
                    {
                        if (!con.Pass(Source))
                            return false;
                    }
                    else
                    {
                        if (!con.Pass(Target))
                            return false;
                    }
                }
            }
            if (GetKey("Disable") == 1)
                return false;
            return true;
        }

        public override void CommonKeys()
        {
            // "CoolDown": Original cool down
            // "CCD": Current cool down
            // "TriggerCount": Remaining trigger count
            // "AddCondition": Whether there are additional conditions for triggering
            // "SourceCondition": Whether the conditions should be passed for source
            // "Disable": Whether to disable the trigger
            base.CommonKeys();
        }
    }
}