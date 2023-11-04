using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CCDChange_Random : Signal_CCDChange {
        public List<string> SkillKeys;

        public override void StartEffect()
        {
            if (!Target)
                return;
            List<string> Pool = new List<string>();
            foreach (string s in SkillKeys)
            {
                Mark_Skill S = Target.GetSkill(s, out _);
                if (!S || S.GetKey("CCD") <= 0)
                    continue;
                Pool.Add(s);
            }
            if (Pool.Count > 0)
                SkillKey = Pool[Random.Range(0, Pool.Count)];
            base.StartEffect();
        }
    }
}