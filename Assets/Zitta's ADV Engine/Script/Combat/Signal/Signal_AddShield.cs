using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AddShield : Signal_AddStatus {

        public override void StartEffect()
        {
            SetKey("Shield", GetShieldValue());
        }

        public override void EndEffect()
        {
            List<string> AddKeys = new List<string>();
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                    AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
            }
            if (HasKey("Shield"))
                AddKeys.Add(KeyBase.Compose("Shield", GetKey("Shield")));
            for (int i = 0; i < StatusPrefabs.Count; i++)
                Target.AddStatus(StatusPrefabs[i].GetComponent<Mark_Status>(), AddKeys, Source);

            if (GetKey("NumberEffect") == 1)
                UIControl.Main.NumberEffect("Heal", ((int)GetKey("Shield")).ToString(), Target);
        }

        public virtual float GetShieldValue()
        {
            float a = GetKey("Shield");
            if (HasKey("BaseScaling"))
                a *= Source.GetBaseDamage();
            if (HasKey("MaxLifeRate"))
                a += Source.GetMaxLife() * GetKey("MaxLifeRate");
            if (HasKey("LifeRate"))
                a += Source.GetLife() * GetKey("LifeRate");
            return a;
        }

        public override void CommonKeys()
        {
            // "Shield": Ini shield amount
            // "BaseScaling": Whether the shield amount should be affected by base damage
            // "MaxLifeRate": Amount of max life add to shield
            // "LifeRate": Amount of life add to shield
            base.CommonKeys();
        }
    }
}