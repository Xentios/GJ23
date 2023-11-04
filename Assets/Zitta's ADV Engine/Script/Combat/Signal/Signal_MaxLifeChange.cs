using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_MaxLifeChange : Signal {

        public override void EndEffect()
        {
            Target.SetMaxLife(Target.GetMaxLife() + GetKey("Value"));
            Target.SetLife(Target.GetMaxLife());
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Value": Max life change value
            base.CommonKeys();
        }
    }
}