using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition : Mark {

        public virtual bool Pass(KeyBase KB)
        {
            return true;
        }

        public virtual bool Pass(Card Source)
        {
            return true;
        }

        public virtual bool Pass(Signal S)
        {
            return true;
        }

        public override void CommonKeys()
        {
            // "SourceCondition": Whether to apply to source when cheked by Trigger_Signal
            base.CommonKeys();
        }
    }
}