using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_ChangeDirection : Signal {

        public override void EndEffect()
        {
            if (!Source || (!Target && GetKey("RequireTarget") == 1))
                return;
            Vector2 P = Position;
            if (GetKey("Reset") == 1)
                Source.Movement.CurrentDirection = new Vector2();
            else if (HasKey("FixedDirectionX") || HasKey("FixedDirectionY"))
                Source.Movement.CurrentDirection = new Vector2(GetKey("FixedDirectionX"), GetKey("FixedDirectionY"));
            else if (GetKey("Reverse") == 1)
                Source.Movement.CurrentDirection = (Source.GetPosition() - P).normalized;
            else
                Source.Movement.CurrentDirection = (P - Source.GetPosition()).normalized;
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "RequireTarget": Whether to only effect with a target
            // "Reverse": Whether to inverse the direction
            // "Reset": Whether to reset direction
            // "FixedDirectionX": Fixed direction value X
            // "FixedDirectionY": Fixed direction value Y
            base.CommonKeys();
        }
    }
}