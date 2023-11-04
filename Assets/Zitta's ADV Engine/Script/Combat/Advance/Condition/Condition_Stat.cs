using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Stat : Condition {

        public override bool Pass(Card Source)
        {
            if (HasKey("MinDamage") && Source.GetBaseDamage() < GetKey("MinDamage"))
                return false;
            if (HasKey("MaxDamage") && Source.GetBaseDamage() > GetKey("MaxDamage"))
                return false;
            if (HasKey("MinLife") && Source.GetLife() < GetKey("MinLife"))
                return false;
            if (HasKey("MaxLife") && Source.GetLife() > GetKey("MaxLife"))
                return false;
            if (HasKey("MinDamage_Permanent") && Source.GetBaseDamage_Permanent() < GetKey("MinDamage_Permanent"))
                return false;
            if (HasKey("MaxDamage_Permanent") && Source.GetBaseDamage_Permanent() > GetKey("MaxDamage_Permanent"))
                return false;
            if (HasKey("MinLife_Permanent") && Source.GetMaxLife_Permanent() < GetKey("MinLife_Permanent"))
                return false;
            if (HasKey("MaxLife_Permanent") && Source.GetMaxLife_Permanent() > GetKey("MaxLife_Permanent"))
                return false;
            return true;
        }

        public override void CommonKeys()
        {
            // "MinDamage": Minimum damage value to pass (inclusive)
            // "MaxDamage": Maximum damage value to pass (inclusive)
            // "MinLife": Minimum life value to pass (inclusive)
            // "MaxLife": Maximum life value to pass (inclusive)
            // "MinDamage_Permanent": Minimum permanent damage value to pass (inclusive)
            // "MaxDamage_Permanent": Maximum permanent damage value to pass (inclusive)
            // "MinLife_Permanent": Minimum permanent life to pass (inclusive)
            // "MaxLife_Permanent": Maximum permanent life to pass (inclusive)
            base.CommonKeys();
        }
    }
}