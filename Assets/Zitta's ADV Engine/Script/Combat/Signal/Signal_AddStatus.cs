using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AddStatus : Signal {
        public List<GameObject> StatusPrefabs;
        public List<string> InheritKeys;

        public override void EndEffect()
        {
            if (!Target)
                return;
            List<string> AddKeys = new List<string>();
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                {
                    if ((s == "Damage" || s == "Heal") && GetKey("BaseScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s) * Source.GetBaseDamage()));
                    else if (s == "InheritDamage" && GetKey("BaseScaling") != 0)
                        AddKeys.Add(KeyBase.Compose("Damage", GetKey("InheritDamage") * Source.GetBaseDamage()));
                    else if (s == "InheritHeal" && GetKey("BaseScaling") != 0)
                        AddKeys.Add(KeyBase.Compose("Heal", GetKey("InheritHeal") * Source.GetBaseDamage()));
                    else if (s == "Stack" && HasKey("RandomStackMin") && HasKey("RandomStackMax"))
                        AddKeys.Add(KeyBase.Compose(s, Random.Range((int)GetKey("RandomStackMin"), (int)GetKey("RandomStackMax") + 1)));
                    else if (s == "AddDamage" && GetKey("SourceStatScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddDamage") + Source.GetBaseDamage() * GetKey("SourceStatScaling"))));
                    else if (s == "AddLife" && GetKey("SourceStatScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddLife") + Source.GetMaxLife() * GetKey("SourceStatScaling"))));
                    else if (s == "AddDamage" && GetKey("TargetStatScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddDamage") + Target.GetBaseDamage() * GetKey("TargetStatScaling"))));
                    else if (s == "AddLife" && GetKey("TargetStatScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddLife") + Target.GetMaxLife() * GetKey("TargetStatScaling"))));
                    else if (s == "AddDamage" && GetKey("TargetBaseStatScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddDamage") + Target.BaseDamage * GetKey("TargetBaseStatScaling"))));
                    else if (s == "AddLife" && GetKey("TargetBaseStatScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddLife") + Target.MaxLife * GetKey("TargetBaseStatScaling"))));
                    else if (s == "AddDamage" && GetKey("LifeToDamage") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddDamage") + Source.GetMaxLife() * GetKey("LifeToDamage"))));
                    else if (s == "AddDamage" && GetKey("LifeOverrideDamage") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(Source.GetMaxLife() * GetKey("LifeOverrideDamage") - Source.GetBaseDamage())));
                    else if (s == "AddLife" && GetKey("DamageOverrideLife") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(Source.GetBaseDamage() * GetKey("DamageOverrideLife") - Source.GetMaxLife())));
                    else if (s == "AddDamage" && GetKey("OverrideDamage") != 0)
                        AddKeys.Add(KeyBase.Compose(s, GetKey("AddDamage") - Target.GetBaseDamage()));
                    else if (s == "AddLife" && GetKey("OverrideLife") != 0)
                        AddKeys.Add(KeyBase.Compose(s, GetKey("AddLife") - Target.GetMaxLife()));
                    else if (s == "AddDamage" && GetKey("EffectValueScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddDamage") + Source.GetKey("EffectValue") * GetKey("EffectValueScaling"))));
                    else if (s == "AddLife" && GetKey("EffectValueScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, StaticAssign.RoundUp(GetKey("AddLife") + Source.GetKey("EffectValue") * GetKey("EffectValueScaling"))));
                    else
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
                }
            }
            for (int i = 0; i < StatusPrefabs.Count; i++)
                Target.AddStatus(StatusPrefabs[i].GetComponent<Mark_Status>(), AddKeys, Source);
        }

        public override void CommonKeys()
        {
            // "InheritDamage": Damage key to inherit
            // "InheritHeal": Heal key to inherit
            // "BaseScaling": Whether to scale Damage and Heal key base on Base Damage
            // "StressScaling": Whether to scale DamageMod key base on Stress
            // "StressScalingChange": Value change per stress
            // "StressToStack": Whether to change Stack base on Stress
            // "RandomStackMin": Minimum random stack
            // "RandomStackMax": Maximum random stack
            // "SourceStatScaling": Whether to add stats base on source's stats (Scale value)
            // "TargetStatScaling": Whether to add stats base on target's stats (Scale value)
            // "TargetBaseStatScaling": Whether to add stats base on target's base stats (Scale value)
            // "LifeToDamage": Whether to add life to damage (Scale value)
            // "LifeOverrideDamage": Whether to override damage with life value (Scale value)
            // "DamageOverrideLife": Whether to override life with damage (Scale value)
            // "OverrideDamage": Whether to override ori damage
            // "OverrideLife": Whether to override ori max life
            // "EffectValueScaling": Whether to scale base on EffectValue
            base.CommonKeys();
        }
    }
}