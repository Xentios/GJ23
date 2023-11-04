using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class EffectBullet : MonoBehaviour {
        public Vector2 Ori;
        public Vector2 Target;
        public float BaseSize;
        public float LifeTimeScale;
        public Color MainColor;
        [Space]
        public float StartDelay;
        public float FadeDelay;
        public float CurrentFadeDelay;
        public AnimationCurve PositionCurve;
        [Space]
        public ParticleSystem PS;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (StartDelay > 0)
            {
                StartDelay -= Time.deltaTime;
                return;
            }
            if (CurrentFadeDelay < FadeDelay)
            {
                CurrentFadeDelay += Time.deltaTime;
                transform.position = Ori + (Target - Ori) * PositionCurve.Evaluate(CurrentFadeDelay / FadeDelay);
            }
            else
                Destroy(gameObject, 3);
        }

        public void Ini()
        {
            if (Target.x == Ori.x)
                Target.x -= 0.001f;
            if (Target.y == Ori.y)
                Target.y -= 0.001f;
            transform.position = Ori;
            ParticleSystem.MainModule M = PS.main;
            M.startColor = new ParticleSystem.MinMaxGradient(MainColor);
            M.startSize = new ParticleSystem.MinMaxCurve(BaseSize);
            M.startLifetime = new ParticleSystem.MinMaxCurve(0.4f * LifeTimeScale);

            float Distance = (Target - Ori).magnitude;
            float Speed = Distance / CurrentFadeDelay;
            float Scale = Speed / 200f;
            ParticleSystem.EmissionModule EM = PS.emission;
            //M.startLifetime = new ParticleSystem.MinMaxCurve(0.04f * LifeTimeScale);
            //EM.rateOverTime = new ParticleSystem.MinMaxCurve(5f / Scale);
            //M.startLifetime = new ParticleSystem.MinMaxCurve(0.04f / Scale);
        }

        public static GameObject NewBullet(Vector2 StartPoint, Vector2 EndPoint, GameObject BulletPrefab, Color MainColor, float Size, float AddFade, float AddDelay, float LTS)
        {
            GameObject G = Instantiate(BulletPrefab);
            EffectBullet EB = G.GetComponent<EffectBullet>();
            EB.Ori = StartPoint;
            EB.Target = EndPoint;
            EB.MainColor = MainColor;
            EB.BaseSize = Size;
            EB.FadeDelay = AddFade;
            EB.StartDelay = AddDelay;
            EB.LifeTimeScale = LTS;
            EB.Ini();
            return G;
        }

        public static GameObject NewBullet(Vector2 StartPoint, Vector2 EndPoint, GameObject BulletPrefab, Color MainColor,
            float Size, float AddFade, float AddDelay, float LTS, AnimationCurve Curve)
        {
            GameObject G = Instantiate(BulletPrefab);
            EffectBullet EB = G.GetComponent<EffectBullet>();
            EB.Ori = StartPoint;
            EB.Target = EndPoint;
            EB.MainColor = MainColor;
            EB.BaseSize = Size;
            EB.FadeDelay = AddFade;
            EB.StartDelay = AddDelay;
            EB.LifeTimeScale = LTS;
            EB.PositionCurve = Curve;
            EB.Ini();
            return G;
        }
    }
}