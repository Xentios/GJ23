using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_ReplaceSummon : Mark_Status {
        public string OriKey;
        public string NewKey;

        public override void OutputSignal(Signal S)
        {
            if (S.GetComponent<Signal_Summon>() && S.GetComponent<Signal_Summon>().Key == OriKey)
                S.GetComponent<Signal_Summon>().Key = NewKey;
            base.OutputSignal(S);
        }
    }
}