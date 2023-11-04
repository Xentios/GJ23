using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AnimTrigger : Signal {
        public string AnimKey;

        public override void EndEffect()
        {
            if (Target && Target.GetAnim())
                Target.GetAnim().GetAnimator().SetTrigger(AnimKey);
            base.EndEffect();
        }
    }
}