using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_EffectBullet : Signal {
        public GameObject EffectPrefab;
        public Color EffectColor;
        [Space]
        public AnimationCurve Curve;
        public bool OverrideCurve;

        public override void EndEffect()
        {
            Vector2 P;
            if (!HasKey("ParticleScale"))
                SetKey("ParticleScale", 1);
            if (Target && GetKey("IgnoreTarget") <= 0)
                P = Target.GetPosition();
            else
                P = Position;
            if (!HasKey("Size"))
                SetKey("Size", 0.8f);
            if (!HasKey("Time"))
                SetKey("Time", 1);
            if (!HasKey("ParticleScale"))
                SetKey("ParticleScale", 1);
            if (GetKey("InversePosition") == 1)
                BulletActive(P, Position);
            else
                BulletActive(Position, P);
        }

        public void BulletActive(Vector2 StartPoint, Vector2 EndPoint)
        {
            if (!OverrideCurve)
                EffectBullet.NewBullet(StartPoint, EndPoint, EffectPrefab, EffectColor, GetKey("Size"), GetKey("Time"), GetKey("AddDelay"), GetKey("ParticleScale"));
            else
                EffectBullet.NewBullet(StartPoint, EndPoint, EffectPrefab, EffectColor, GetKey("Size"), GetKey("Time"), GetKey("AddDelay"), GetKey("ParticleScale"), Curve);
        }

        public override void CommonKeys()
        {
            // "Size": Size of the effect bullet
            // "Time": Bullet duration
            // "AddDelay": Start delay
            // "ParticleScale": Particle life time scale
            // "IgnoreTarget": Whether the bullet should be position based
            // "InversePosition": Whether to inverse the source and target position
            base.CommonKeys();
        }
    }
}