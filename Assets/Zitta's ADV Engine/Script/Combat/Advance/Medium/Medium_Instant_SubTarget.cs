using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_Instant_SubTarget : Medium {
        public GameObject Targeting;
        public List<GameObject> AddTargetings;

        public override void EffectUpdate(float Value)
        {
            if (Source)
            {
                Target = Targeting.GetComponent<Targeting>().FindTarget(Source, Position);
                if (!Target)
                {
                    foreach (GameObject G in AddTargetings)
                    {
                        Target = G.GetComponent<Targeting>().FindTarget(Source, Position);
                        if (Target)
                            break;
                    }
                }
            }

            bool ConditionPass = true;
            if (GetKey("AddCondition") == 1)
            {
                List<Condition> Conditions = new List<Condition>();
                foreach (Condition Con in GetComponentsInChildren<Condition>())
                    Conditions.Add(Con);
                foreach (Condition con in Conditions)
                {
                    if (!Source || !con.Pass(Source))
                    {
                        ConditionPass = false;
                        break;
                    }
                }
            }

            if (!Source || !Target || !ConditionPass)
            {
                SetKey("Delay", 0);
                Effect(null);
                EndEffect();
                return;
            }

            ChangeKey("Delay", -Value);
            SetKey("PositionX", Target.GetPosition().x);
            SetKey("PositionY", Target.GetPosition().y);
            if (GetKey("Delay") <= 0)
            {
                Effect(Target);
                EndEffect();
            }
        }

        public override void CommonKeys()
        {
            // "Delay": The delay before final effect
            // "AddCondition": Whether there are additional source conditions
            base.CommonKeys();
        }
    }
}