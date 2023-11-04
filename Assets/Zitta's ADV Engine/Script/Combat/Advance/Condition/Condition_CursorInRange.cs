using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_CursorInRange : Condition {

        public override bool Pass(Card Source)
        {
            return (!HasKey("Positive") || GetKey("Positive") == 1) == (Source.GetPosition() - Cursor.Main.GetPosition()).magnitude <= GetKey("Range");
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass if the cursor is in range
            // "Range": Range of the condition
            base.CommonKeys();
        }
    }
}