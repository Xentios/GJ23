using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CopyDamage : Signal {

        public override void EndEffect()
        {
            if (!Source || !Target)
                return;

            float Ori = Source.GetSourceDamage();
            float Tar = Target.GetBaseDamage();
            float Change = Tar - Ori;
            Source.SetBaseDamage(Target.GetBaseDamage());

            if (GetKey("NumberEffect") == 1)
            {
                if (Change >= 0)
                    UIControl.Main.NumberEffect("DamageUp", ((int)Change).ToString(), Source);
                else
                    UIControl.Main.NumberEffect("DamageDown", ((int)Change).ToString(), Source);
            }
        }

        public override void CommonKeys()
        {
            // "AddValue": Additional source damage change
            // "NumberEffect": Whether this signal should create number effects
            base.CommonKeys();
        }
    }
}