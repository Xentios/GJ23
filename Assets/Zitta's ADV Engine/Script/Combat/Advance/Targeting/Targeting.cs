using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting : Mark {

        public virtual Card FindTarget(Card Source, Vector2 Position)
        {
            return null;
        }

        public virtual bool CheckTarget(Card Source, Card Target)
        {
            return Target && Target.CardActive();
        }

        public override void CommonKeys()
        {
            // "Untargeted" (On KeyBase): Whether the card should be ignored by auto targeting
            base.CommonKeys();
        }

        public static List<Card> TargetInRange(Vector2 Origin, float Range)
        {
            List<Card> Temp = new List<Card>();
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                if (!CombatControl.Main.Cards[i])
                    continue;
                Card C = CombatControl.Main.Cards[i];
                if (!C.CombatActive() || C.PassValue("UnTargeted") > 0)
                    continue;
                if ((C.GetPosition() - Origin).magnitude - C.GetRadius() <= Range)
                    Temp.Add(C);
            }
            return Temp;
        }

        public static Card ClosestInRange(Vector2 Origin, float Range)
        {
            Card Temp = null;
            float MinRange = 9999;
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                if (!CombatControl.Main.Cards[i])
                    continue;
                Card C = CombatControl.Main.Cards[i];
                if (!C.CombatActive() || C.PassValue("UnTargeted") > 0)
                    continue;
                if ((C.GetPosition() - Origin).magnitude - C.GetRadius() > Range)
                    continue;
                if ((C.GetPosition() - Origin).magnitude - C.GetRadius() <= MinRange)
                {
                    MinRange = (C.GetPosition() - Origin).magnitude - C.GetRadius();
                    Temp = C;
                }
            }
            return Temp;
        }
    }
}