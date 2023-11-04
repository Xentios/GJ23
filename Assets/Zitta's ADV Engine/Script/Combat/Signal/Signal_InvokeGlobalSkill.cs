using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ADV
{
    public class Signal_InvokeGlobalSkill : Signal {
        public string Key;

        public override void EndEffect()
        {
            Mark_Skill S = CombatControl.Main.GlobalCard.GetSkill(Key, out int _);
            if (!S)
                return;
            if (!Target && GetKey("RequireTarget") == 1)
                return;
            if (GetKey("Intrinsic") == 1)
                S.IntrinsicForceUse(Target, Position);
            else
                S.ForceUse(Target, Position);
        }

        public override void CommonKeys()
        {
            // "RequireTarget": Whether to only trigger with a target
            // "Intrinsic": Whether to invoke in intrinsic mode (Trigger cooldown / Trigger proc / Ignore conditions)
            base.CommonKeys();
        }
    }
}