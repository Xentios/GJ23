using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_StatMod : Mark_Status {

        public override void OnAssign()
        {
            if (GetKey("HideEffect") == 0)
            {
                string s = "";
                if (GetKey("AddDamage") != 0 && GetKey("AddLife") != 0)
                    s = StaticAssign.GetColorCode("Yellow") + "+" + GetKey("AddDamage") + StaticAssign.GetColorCode("CE")
                        + " " + StaticAssign.GetColorCode("Green") + "+" + GetKey("AddLife") + StaticAssign.GetColorCode("CE");
                else if (GetKey("AddDamage") != 0)
                    s = StaticAssign.GetColorCode("Yellow") + "+" + GetKey("AddDamage") + StaticAssign.GetColorCode("CE");
                else if (GetKey("AddLife") != 0)
                    s = StaticAssign.GetColorCode("Green") + "+" + GetKey("AddLife") + StaticAssign.GetColorCode("CE");
                UIControl.Main.NumberEffect("General", s, Source);
            }
            base.OnAssign();
        }

        public override void Stack(Mark_Status M)
        {
            StackCount(M);
            StackDuration(M);
        }

        public override float PassValue(string Key, float Value)
        {
            if ((GetKey("RequireCaster") == 1 || GetKey("CasterAdjacent") == 1) && (!Caster || !Caster.CombatActive()))
                return base.PassValue(Key, Value);
            /*if (GetKey("CasterAdjacent") == 1)
            {
                CardSlot CS1 = Caster.GetCurrentSlot();
                CardSlot CS2 = Source.GetCurrentSlot();
                if (!CS1 || !CS2 || Mathf.Abs(((CombatSlot)CS1).Index - ((CombatSlot)CS2).Index) > 1)
                    return base.PassValue(Key, Value);
            }*/
            if (GetKey("CasterAdjacent") == 1 && Mathf.Abs(Source.GetKey("AggroIndex") - Caster.GetKey("AggroIndex")) > 1)
                return base.PassValue(Key, Value);
            if (Key == "DamageMod" && HasKey("DamageMod"))
                return Value * GetDamageMod();
            else if (Key == "LifeMod" && HasKey("LifeMod"))
                return Value * GetLifeMod();
            else if (Key == "AddDamage" && HasKey("AddDamage"))
            {
                float V = Value + GetAddDamage();
                if (HasKey("MaxDamage") && V > GetKey("MaxDamage"))
                    V = GetKey("MaxDamage");
                return V;
            }
            else if (Key == "AddLife" && HasKey("AddLife"))
            {
                float V = Value + GetAddLife();
                if (HasKey("MaxLife") && V > GetKey("MaxLife"))
                    V = GetKey("MaxLife");
                return V;
            }
            else if (Key == "AddDamage_Permanent" && HasKey("AddDamage") && GetKey("Permanent") == 1)
                return Value + GetAddDamage();
            else if (Key == "AddLife_Permanent" && HasKey("AddLife") && GetKey("Permanent") == 1)
                return Value + GetAddLife();
            return base.PassValue(Key, Value);
        }

        public float GetDamageMod()
        {
            float a = GetKey("DamageMod");
            if (HasKey("Stack") && HasKey("DamageStackChange"))
                a += GetKey("Stack") * GetKey("DamageStackChange");
            return a;
        }

        public float GetLifeMod()
        {
            float a = GetKey("LifeMod");
            if (HasKey("Stack") && HasKey("LifeStackChange"))
                a += GetKey("Stack") * GetKey("LifeStackChange");
            return a;
        }

        public virtual float GetAddDamage()
        {
            return GetKey("AddDamage");
        }

        public virtual float GetAddLife()
        {
            return GetKey("AddLife");
        }

        public override void CommonKeys()
        {
            // "DamageMod": Mod for base damage
            // "LifeMod": Mod for max life
            // "DamageStackChange": Damage mod change per stack
            // "LifeStackChange": Life mod change per stack
            // "AddDamage": Additional damage value
            // "AddLife": Additional life value
            // "MaxDamage": Maximum damage value
            // "MaxLife": Maximum life value
            // "HideEffect": Whether to hide number effects
            // "RequireCaster": Whether to only active when caster is alive
            // "CasterAdjacent": Whether to only active when caster is adjacent
            base.CommonKeys();
        }
    }
}