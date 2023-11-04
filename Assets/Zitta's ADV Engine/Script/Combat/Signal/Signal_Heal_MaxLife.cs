using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Heal_MaxLife : Signal_Heal {

        public override float GetHealValue(float Base)
        {
            if (!HasKey("MaxLifeRate") && !HasKey("LifeRate") && !HasKey("Heal"))
                SetKey("MaxLifeRate", 1);
            return base.GetHealValue(Base) + Target.GetMaxLife() * GetKey("MaxLifeRate") + Target.GetLife() * GetKey("LifeRate");
        }

        public override void CommonKeys()
        {
            // "MaxLifeRate": Amount of max life add to heal value
            // "LifeRate": Amount of life add to heal value
            base.CommonKeys();
        }
    }
}