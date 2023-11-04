using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Invoke : Signal {
        public string SkillKey;

        public override void EndEffect()
        {
            if (!Target)
                return;
            if (SkillKey != "")
                Target.InvokeSkill(SkillKey, Position);
            else
                Target.InvokeSkill(GetKey("Channel", true), true, Position);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Channel": Channel to invoke
            // [Channel == 0]: On recruit
            // [Channel == 0.1]: On recruit right
            // [Channel == -0.1]: On recruit left
            // [Channel == 1]: On auto attack
            // [Channel == 1.1]: On first attack
            // [Channel == 1.2]: On second attack
            // [Channel == 1.3]: On third attack
            // [Channel == 4]: On remove
            // [Channel == 5]: Use item
            // [Channel == 7]: On core damage
            // [Channel == 8]: On defeat lock trigger
            // [Channel == 10]: Start empower
            // [Channel == 11]: On enemy empower
            // [Channel == 14]: On energy depleted
            // [Channel == 19]: On defeat
            // [Channel == 20]: On victory

            // [Channel == 100]: On event load
            // [Channel == 101]: On event resolve (EventCard)
            // [Channel == 102]: On event interaction (InteractionCard)
            base.CommonKeys();
        }
    }
}