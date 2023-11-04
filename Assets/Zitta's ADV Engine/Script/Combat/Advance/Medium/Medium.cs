using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium : Mark {
        [HideInInspector] public Card Target;
        [HideInInspector] public Vector2 Position;
        public List<GameObject> Signals;

        public virtual void Ini(Card S, Card T, Vector2 P)
        {
            Source = S;
            Target = T;
            if (Target && GetKey("TargetSnap") == 1)
                Position = Target.GetPosition();
            else
                Position = P;
            if (GetKey("Inverse") == 1)
            {
                Target = S;
                Source = T;
            }
            CombatControl.Main.AddMedium(this);
            if (!HasKey("Delay"))
                Update();
        }

        public override void Update()
        {
            if (CombatControl.Main.MediumInCombat(this))
                TimePassed(CombatControl.Main.CombatTime());
            base.Update();
        }

        public override void TimePassed(float Value)
        {
            if (GetKey("Follow") == 1 && Target)
                Position = Target.GetPosition();
            if (GetKey("AlreadyDead") != 1 && Source)
                EffectUpdate(Value);
            else
                InterruptEffect();
            base.TimePassed(Value);
        }

        public virtual void EffectUpdate(float Value)
        {

        }

        public virtual void InterruptEffect()
        {
            EndEffect();
        }

        public virtual void Effect(Card Target)
        {
            if (!Source)
                return;
            if ((!Target || !Target.CombatActive()) && GetKey("RequireTarget") == 1)
                return;
            List<string> AddKeys = new List<string>();
            AddKeys.Add(KeyBase.Compose("PositionX", GetKey("PositionX")));
            AddKeys.Add(KeyBase.Compose("PositionY", GetKey("PositionY")));
            if (HasKey("ItemCount"))
                AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("ItemCount")));
            foreach (GameObject G in Signals)
                Source.SendSignal(G, AddKeys, Target, Position);
        }

        public virtual void EndEffect()
        {
            if (GetKey("AlreadyDead") != 0)
                return;
            SetKey("AlreadyDead", 1);
            CombatControl.Main.RemoveMedium(this);
            Destroy(gameObject, 5);
        }

        public override void CommonKeys()
        {
            // "TargetSnap": Whether to snap to the target's position
            // "AlreadyDead": Whether the effect has ended
            // "Follow": Whether the medium should follow the target
            // "RequireTarget": Whether to only trigger effect with a target
            // "Inverse": Whether to set target as source
            base.CommonKeys();
        }
    }
}