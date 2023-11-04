using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_ExecuteMedium : Signal {
        public List<string> RequiredKeys;
        public List<string> AvoidedKeys;

        public bool Trigger(Medium M)
        {
            bool T = false;
            if (RequiredKeys.Count <= 0)
                T = true;
            foreach (string s in RequiredKeys)
                if (M.HasKey(s))
                    T = true;
            foreach (string s in AvoidedKeys)
                if (M.HasKey(s))
                    T = false;
            return T;
        }

        public override void EndEffect()
        {
            for (int i = CombatControl.Main.Mediums.Count - 1; i >= 0; i--)
            {
                if (CombatControl.Main.Mediums[i] && Trigger(CombatControl.Main.Mediums[i]))
                    CombatControl.Main.Mediums[i].SetKey("Delay", 0);
            }
            base.EndEffect();
        }
    }
}