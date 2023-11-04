using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_InheritLife : Mark_Status {

        public override void OnAssign()
        {
            if (!CombatControl.Main.GlobalCard.HasKey("Life_" + Source.GetID()))
                SetKey("AddLife", 0);
            else
            {
                float Life = CombatControl.Main.GlobalCard.GetKey("Life_" + Source.GetID());
                SetKey("AddLife", Life - Source.OriLife);
            }
            base.OnAssign();
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "AddLife")
                return GetKey("AddLife") + Value;
            return base.PassValue(Key, Value);
        }
    }
}