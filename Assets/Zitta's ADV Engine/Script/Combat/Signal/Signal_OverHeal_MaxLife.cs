using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_OverHeal_MaxLife : Signal_OverHeal {

        public override float GetHealValue(float Base)
        {
            return Base + Source.GetMaxLife() * GetKey("SourceMaxLifeRate") + Target.GetMaxLife() * GetKey("TargetMaxLifeRate");
        }

        public override void CommonKeys()
        {
            // "SourceMaxLifeRate": Amount of source's max life add to heal value
            // "TargetMaxLifeRate": Amount of target's max life add to heal value
            base.CommonKeys();
        }
    }
}