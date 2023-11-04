using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_DeathLock : Mark_Trigger {

        public override void StartOfCombat()
        {
            if (HasKey("TriggerCount"))
                SetKey("CTC", GetKey("TriggerCount"));
            base.StartOfCombat();
        }

        public virtual float ProcessLifeChange(float LifeChange)
        {
            if (LifeChange >= 0 || Source.GetLife() + LifeChange > 0 || (HasKey("CTC") && GetKey("CTC") <= 0))
                return LifeChange;

            if (HasKey("CTC"))
                ChangeKey("CTC", -1);

            OnTrigger(Source);

            if (GetKey("LockValue") != 0)
                return GetKey("LockValue") - Source.GetLife();
            else
                return LifeChange;
        }

        public override void CommonKeys()
        {
            // "TriggerCount": Trigger count each combat
            // "CTC": Current trigger count
            // "LockValue": Remaining life to lock at
            base.CommonKeys();
        }
    }
}