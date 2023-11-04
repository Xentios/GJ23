using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_HasMark : Condition {
        public string TargetID;

        public override bool Pass(Card Source)
        {
            if (GetKey("Global") == 1)
                Source = CombatControl.Main.GlobalCard;
            return (GetKey("Positive") == 1 || !HasKey("Positive")) == Source.HasMark(TargetID);
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass if the card has the mark
            // "Global": Whether to pass global card instead
            base.CommonKeys();
        }
    }
}