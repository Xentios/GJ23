using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_SkillAvailable : Condition {
        public string SkillKey;

        public override bool Pass(Card Source)
        {
            if (GetKey("Global") == 1)
                Source = CombatControl.Main.GlobalCard;
            Mark_Skill S = Source.GetSkill(SkillKey, out _);
            if (!S)
                return false;
            return (GetKey("Positive") == 1 || !HasKey("Positive")) == S.GetKey("CCD") <= 0;
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass if the skill is avaliable
            // "Global": Whether to pass global card instead
            base.CommonKeys();
        }
    }
}