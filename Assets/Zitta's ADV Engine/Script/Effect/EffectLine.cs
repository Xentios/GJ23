using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class EffectLine : MonoBehaviour {
        public Vector3 Ori;
        public Vector3 Target;
        public float BaseWidth;
        public float BaseAlpha;
        public Color MainColor;
        [Space]
        public AnimationCurve AlphaCurve;
        public AnimationCurve WidthCurve;
        public float StartDelay;
        public float FadeDelay;
        public float CurrentFadeDelay;
        [Space]
        public GameObject Top;
        public GameObject Middle;
        public GameObject Down;
        public List<SpriteRenderer> SRs;

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
                float Alpha = AlphaCurve.Evaluate(CurrentFadeDelay / FadeDelay) * BaseAlpha;
                SetColor(MainColor, Alpha);
                float Width = WidthCurve.Evaluate(CurrentFadeDelay / FadeDelay) * BaseWidth;
                SetWidth(Width);
            }
            else
                Destroy(gameObject);
        }

        public void Ini()
        {
            if (Target.x == Ori.x)
                Target.x -= 0.001f;
            if (Target.y == Ori.y)
                Target.y -= 0.001f;
            transform.position = Ori + (Target - Ori) * 0.5f;
            transform.up = Target - Ori;
            Down.transform.position = Ori;
            Top.transform.position = Target;
            Top.transform.localScale = new Vector3(BaseWidth * 5f, BaseWidth * 5f, 1);
            Down.transform.localScale = new Vector3(BaseWidth * 5f, BaseWidth * 5f, 1);
            Middle.transform.localScale = new Vector3(BaseWidth, (Target - Ori).magnitude, 1);
            BaseAlpha = MainColor.a;
            SetColor(MainColor);
        }

        public void SetWidth(float Value)
        {
            if (Value <= 0.15f)
                Value = 0f;
            Middle.transform.localScale = new Vector3(Value, Middle.transform.localScale.y, 1);
            Top.transform.localScale = new Vector3(Value * 5f, Value * 5f, 1);
            Down.transform.localScale = new Vector3(Value * 5f, Value * 5f, 1);
        }

        public void SetColor(Color C, float Alpha)
        {
            Color c = new Color(C.r, C.g, C.b, Alpha);
            SetColor(c);
        }

        public void SetColor(Color C)
        {
            foreach (SpriteRenderer SR in SRs)
                SR.color = C;
        }

        public static GameObject NewLine(Vector3 StartPoint, Vector3 EndPoint, Color MainColor, float Width, float AddFade, float AddDelay)
        {
            GameObject G = StaticAssign.GetMain().EffectLine;
            return NewLine(StartPoint, EndPoint, StaticAssign.GetMain().EffectLine, MainColor, Width, AddFade, AddDelay);
        }

        public static GameObject NewLine(Vector3 StartPoint, Vector3 EndPoint, GameObject LinePrefab, Color MainColor, float Width, float AddFade, float AddDelay)
        {
            GameObject G = Instantiate(LinePrefab);
            EffectLine EL = G.GetComponent<EffectLine>();
            EL.Ori = StartPoint;
            EL.Target = EndPoint;
            EL.MainColor = MainColor;
            EL.BaseWidth = Width;
            EL.FadeDelay += AddFade;
            EL.StartDelay += AddDelay;
            EL.Ini();
            return G;
        }
    }
}