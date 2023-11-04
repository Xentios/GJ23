using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_TargetInRange : Condition {

        public Card FindTarget(Card Source, Vector2 Position)
        {
            List<Card> Cards = Targeting.TargetInRange(Position, GetKey("Range"));
            float MinDistance = 9999;
            Card Target = null;
            foreach (Card C in Cards)
            {
                bool SidePass = false;
                if (GetKey("TargetEnemy") == 1 && C.GetSide() != Source.GetSide())
                    SidePass = true;
                if (GetKey("TargetFriendly") == 1 && C.GetSide() == Source.GetSide())
                    SidePass = true;
                if (GetKey("TargetGlobalEnemy") == 1 && C.GetSide() != CombatControl.Main.GlobalCard.GetSide())
                    SidePass = true;
                if (!SidePass)
                    continue;
                if (GetKey("IgnoreSource") == 1 && C == Source)
                    continue;
                if (!CanTrigger(C))
                    continue;
                float Distance = (C.GetPosition() - Position).magnitude - C.Radius;
                if (Distance <= MinDistance)
                {
                    MinDistance = Distance;
                    Target = C;
                }
            }
            return Target;
        }

        public virtual bool CanTrigger(Card Target)
        {
            if (GetKey("AddCondition") == 1)
            {
                List<Condition> Conditions = new List<Condition>();
                foreach (Condition Con in GetComponentsInChildren<Condition>())
                    if (Con.transform.parent == transform)
                        Conditions.Add(Con);
                foreach (Condition con in Conditions)
                {
                    if (!con.Pass(Target))
                        return false;
                }
            }
            return true;
        }

        public override bool Pass(Card Source)
        {
            return FindTarget(Source, Source.GetPosition()) == (GetKey("Positive") == 1);
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass if there is a target
            // "Range": Targeting range
            // "TargetEnemy": Whether to target card on different side
            // "TargetFriendly": Whether to target card on the same side
            // "TargetGlobalEnemy": Whether to target enemies of global card
            // "IgnoreSource": Whether to ignore source
            // "AddCondition": Whether there are additional conditions for targeting
            base.CommonKeys();
        }
    }
}