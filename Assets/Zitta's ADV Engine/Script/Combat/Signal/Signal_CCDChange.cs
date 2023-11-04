using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CCDChange : Signal {
        public string SkillKey;

        public override void InputMark(Mark M)
        {
            if (M.GetID() == SkillKey)
            {
                if (GetKey("Set") == 1)
                    M.SetKey("CCD", GetKey("Value"));
                else
                {
                    float Final;
                    if (GetKey("Multiply") == 1)
                        Final = M.GetKey("CCD") * GetKey("Value");
                    else
                        Final = M.GetKey("CCD") + GetKey("Value");
                    if ((GetKey("Value") < 0 || GetKey("Set") == 1) && Final < GetKey("MinValue"))
                    {
                        if (M.GetKey("CCD") < GetKey("MinValue"))
                            Final = M.GetKey("CCD");
                        else
                            Final = GetKey("MinValue");
                    }
                    M.SetKey("CCD", Final);
                }
            }
            base.InputMark(M);
        }

        public override void CommonKeys()
        {
            // "Value": CCD value change
            // "Set": Whether to set value instead of changing
            // "Multiply": Whether to multiply the value instead
            // "MinValue": Minimum cool down value to reduce to
            base.CommonKeys();
        }
    }
}