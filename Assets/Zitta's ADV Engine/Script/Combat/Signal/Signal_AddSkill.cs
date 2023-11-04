using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AddSkill : Signal {
        public GameObject SkillPrefab;

        public override void EndEffect()
        {
            Target.AddSkill(SkillPrefab.GetComponent<Mark_Skill>());
            base.EndEffect();
        }
    }
}