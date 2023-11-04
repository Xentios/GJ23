using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_RemoveStun : Signal {

        public override void InputMark(Mark M)
        {
            if (M.GetType() == typeof(Mark_Status_Stunned) && GetKey("RemoveStack") > 0)
            {
                Mark_Status_Stunned MS = (Mark_Status_Stunned)M;
                if (MS.GetKey("Stack") >= GetKey("RemoveStack"))
                {
                    MS.ChangeKey("Stack", -GetKey("RemoveStack"));
                    SetKey("RemoveStack", 0);
                    MS.UpdateCount();
                }
                else
                {
                    float a = MS.GetKey("Stack");
                    MS.SetKey("Stack", 0);
                    ChangeKey("RemoveStack", -a);
                    MS.UpdateCount();
                }
            }
            base.InputMark(M);
        }

        public override void CommonKeys()
        {
            // "RemoveStack": Amount of stun to remove
            base.CommonKeys();
        }
    }
}