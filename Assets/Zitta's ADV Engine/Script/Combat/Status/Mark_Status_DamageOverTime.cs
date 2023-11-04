using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DamageOverTime : Mark_Status {

        public override void TimePassed(float Value)
        {
            Source.ChangeLife(-Value * GetKey("Damage"));
            base.TimePassed(Value);
        }

        public override void CommonKeys()
        {
            // "Damage": Damage (per second) of the effect
            base.CommonKeys();
        }
    }
}
