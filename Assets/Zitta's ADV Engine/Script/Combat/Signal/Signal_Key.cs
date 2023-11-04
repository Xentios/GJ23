using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Key : Signal {
        public string KeyName;

        public override void EndEffect()
        {
            if (GetKey("Global") == 1)
                Target = CombatControl.Main.GlobalCard;
            if (Target)
            {
                if (GetKey("Set") == 0)
                    Target.ChangeKey(KeyName, GetKey("Value"));
                else if (GetKey("Set") == 1)
                    Target.SetKey(KeyName, GetKey("Value"));
            }
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Value": Value of the key change
            // "Set": Whether to set key instead of change key
            // "Global": Whether to target global card
            base.CommonKeys();
        }
    }
}