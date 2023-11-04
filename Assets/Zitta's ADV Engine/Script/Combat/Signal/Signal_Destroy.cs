using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Destroy : Signal {

        public override void EndEffect()
        {
            Target.Destroy();
            base.EndEffect();
        }
    }
}