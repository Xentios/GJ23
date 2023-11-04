using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status : Mark {
        [HideInInspector] public Card Caster;

        public virtual void Stack(Mark_Status M)
        {
            StackDuration(M);
        }

        public void StackCount(Mark_Status M)
        {
            if (!HasKey("Stack") || !M.HasKey("Stack"))
                return;
            ChangeKey("Stack", M.GetKey("Stack"));
            if (HasKey("MaxStack") && GetKey("Stack") > GetKey("MaxStack"))
                SetKey("Stack", GetKey("MaxStack"));
        }

        public void StackDuration(Mark_Status M)
        {
            if (HasKey("Duration") && M.HasKey("Duration"))
            {
                if (M.GetKey("Duration") > GetKey("Duration"))
                    SetKey("Duration", M.GetKey("Duration"));
            }
        }

        public virtual void EndOfTurn()
        {
            if (HasKey("Duration"))
            {
                if (HasKey("Duration"))
                    ChangeKey("Duration", -1);
                if (GetKey("Duration") <= 0)
                {
                    CombatRemove();
                    Source.RemoveStatus(this);
                }
            }
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "EndOfTurn" && Value == 1)
                EndOfTurn();
            else if (Key == "Mod" && GetKey("Mod") == 1)
                return 1;
            return base.PassValue(Key, Value);
        }

        public virtual void CombatRemove()
        {

        }

        public override void EndOfCombat()
        {
            if (GetKey("Permanent") == 0)
                Source.RemoveStatus(this);
            if (HasKey("CombatDuration"))
            {
                ChangeKey("CombatDuration", -1);
                if (GetKey("CombatDuration") <= 0)
                    Source.RemoveStatus(this);
            }
            base.EndOfCombat();
        }

        public override void Death()
        {
            if (GetKey("RemoveOnDeath") == 1)
                Source.RemoveStatus(this);
            base.Death();
        }

        public override void TimePassed(float Value)
        {
            if (HasKey("Duration"))
                ChangeKey("Duration", -Value);
            base.TimePassed(Value);
            if (HasKey("Duration") && GetKey("Duration") <= 0)
            {
                CombatRemove();
                Source.RemoveStatus(this);
            }
        }

        public override void CommonKeys()
        {
            // "Render": Whether the status should be rendered
            // "Duration": Current duration
            // "Stack": Current stack
            // "MaxStack": Max stack
            // "IgnoreStack": Whether the status should ignore stacking
            // "CasterStack": Whether the status should only stack with status from the same caster
            // "MaxDuplicateCount": Max duplicate status allowed
            // "Passive": Whether the status comes from passive marks
            // "Permanent": Whether the status should not be removed after combat
            // "CombatDuration": Current combat duration
            // "RemoveOnDeath": Whether to remove the status on death
            // "Mod": Whether the status is a mod
            // "ModIndex": (Mod) index for rendering
            base.CommonKeys();
        }
    }
}