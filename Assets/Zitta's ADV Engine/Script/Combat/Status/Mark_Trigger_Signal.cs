using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_Signal : Mark_Trigger {
        public List<string> IgnoreKeys;
        public string TriggerKey;

        public override void ConfirmSignal(Signal S)
        {
            if (!S.Source)
                return;
            if (GetKey("Alive") == 1 && !Source.CombatActive())
                return;
            if (GetKey("Death") == 1 && Source.CombatActive() && Source.GetLife() > 0)
                return;
            if (GetKey("IgnoreSelf") == 1 && S.Source == Source)
                return;
            if (GetKey("IgnoreTagged") == 1 && S.GetKey("Tag") == 1)
                return;
            foreach (string s in IgnoreKeys)
            {
                if (S.GetKey(s) == 1)
                    return;
            }
            
            bool Trigger = false;
            if (TriggerKey.IndexOf("[") != -1)
            {
                string Key = KeyBase.Translate(TriggerKey, out string Value);
                if (S.GetKey(Key, true) == Value)
                    Trigger = true;
            }
            else if (S.GetKey(TriggerKey) == 1)
                Trigger = true;

            if (Trigger)
            {
                bool Pass;
                if (GetKey("Target") == 0)
                    Pass = OnTrigger(Source);
                else
                    Pass = OnTrigger(S.Source);
                if (Pass && GetKey("Tag") == 1)
                    S.SetKey("Tag", 1);
            }
            base.ConfirmSignal(S);
        }

        public override void CommonKeys()
        {
            // "Alive": Whether to only trigger when the source is alive
            // "Death": Whether to only trigger when the source is in death
            // "IgnoreSelf": Ignore signals that are send by source
            // "Target": Whether to trigger on source (0) or signal's source (1)
            // "Tag": Whether to tag the signal after triggering
            // "IgnoreTagged": Whether to only trigger on untagged signals

            // "OnFriendlyDeath": Trigger when a friendly card dies
            // "OnEnemyDeath": Trigger when an enemy card dies
            // "OnBossDeath": Trigger when a boss card dies
            // "OnConsume": Trigger when fate is used
            // "OnFateGain": Trigger when fate is gained
            // "OnPotionUse": Trigger when a potion is used
            // "OnCoreDamage": Trigger when the core takes damage
            // "OnActionUse": Trigger when a action card is used
            // "OnNewRecruit": Trigger when a follower is recruited
            // "OnFriendlyRemove": Trigger when a follower is removed
            // "OnAbilityUse": Trigger when a follower's ability is used
            // "OnSculpture": Trigger when a follower is turned into sculpture
            // "OnWispDeath": Trigger when a wisp dies
            base.CommonKeys();
        }
    }
}