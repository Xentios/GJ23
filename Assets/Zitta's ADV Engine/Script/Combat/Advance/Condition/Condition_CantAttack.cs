using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_CantAttack : Condition {

        public override bool Pass(Card Source)
        {
            return Source.GetKey("IgnoreDamage") == 1;
        }
    }
}