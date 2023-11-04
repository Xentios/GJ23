using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_Death : Mark_Trigger {

        /*public override void ConfirmSignal(Signal S)
        {
            if (!S.Source)
                return;
            if ((S.HasKey("Damage") && Source.GetLife() <= 0) || S.GetType() == typeof(Signal_Death))
                OnTrigger(S.Target);
            base.ReturnSignal(S);
        }*/

        public override void Death()
        {
            OnTrigger(Source);
            base.Death();
        }
    }
}