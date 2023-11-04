using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Shield : Mark_Status {
        public List<GameObject> BreakSignals;

        public virtual float ProcessLifeChange(float LifeChange)
        {
            if (LifeChange >= 0)
                return LifeChange;
            if (GetKey("Shield") > -LifeChange)
            {
                ChangeKey("Shield", LifeChange);
                return 0;
            }
            Break();
            return LifeChange + GetKey("Shield");
        }

        public virtual void Break()
        {
            List<string> AddKeys = new List<string>();
            for (int i = 0; i < BreakSignals.Count; i++)
                Source.SendSignal(BreakSignals[i], AddKeys, Source, Source.GetPosition());
            Source.RemoveStatus(this);
        }

        public override void Stack(Mark_Status M)
        {
            if (GetKey("StackShield") == 1)
                ChangeKey("Shield", M.GetKey("Shield"));
            else
            {
                if (M.GetKey("Shield") > GetKey("Shield"))
                    SetKey("Shield", M.GetKey("Shield"));
            }
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Shield")
                return Value + GetKey("Shield");
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "Shield": Remaining shield amount
            // "StackShield": Whether shield amount will increase when stacking
            base.CommonKeys();
        }
    }
}