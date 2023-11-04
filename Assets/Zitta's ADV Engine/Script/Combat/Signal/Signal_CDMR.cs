using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CDMR : Signal {
        public Mark_Skill Skill;

        public override void StartEffect()
        {
            if (!Skill)
                return;
            SetKey("CCD", GetKey("CoolDown"));
            if (HasKey("RCD"))
                ChangeKey("CCD", Random.Range(-GetKey("RCD"), GetKey("RCD")));
            base.StartEffect();
        }

        public override void EndEffect()
        {
            if (!Skill)
                return;
            Skill.SetKey("CCD", GetKey("CCD"));
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "CoolDown": Base cool down to reset to
            // "RCD": Random cool down range
            // "CCD": Current pending cool down value
            base.CommonKeys();
        }
    }
}