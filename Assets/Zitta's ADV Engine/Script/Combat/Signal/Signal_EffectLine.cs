using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_EffectLine : Signal {
        public GameObject EffectPrefab;
        public Color EffectColor;

        public override void EndEffect()
        {
            if (!Source)
                return;
            Vector3 RPosition = Position;
            if (Source.Anim)
                RPosition = Source.Anim.GetPivotPosition();
            Vector3 TPosition;
            if (HasKey("OverrideX") || HasKey("OverrideY"))
                TPosition = new Vector2(GetKey("OverrideX"), GetKey("OverrideY"));
            else if (Target && GetKey("IgnoreTarget") <= 0)
            {
                if (Target.Anim)
                    TPosition = Target.Anim.GetPivotPosition();
                else
                    TPosition = Target.GetPosition();
            }
            else
                TPosition = new Vector2(GetKey("TargetPositionX"), GetKey("TargetPositionY"));
            if (!HasKey("Width"))
                SetKey("Width", 0.5f);
            //if (HasKey("DeadDistance"))
                //Position += (TPosition - Position).normalized * GetKey("DeadDistance");
            LineActive(RPosition, TPosition);
        }

        public void LineActive(Vector3 StartPoint, Vector3 EndPoint)
        {
            if (EffectPrefab)
                EffectLine.NewLine(StartPoint, EndPoint, EffectPrefab, EffectColor, GetKey("Width"), GetKey("AddFade"), GetKey("AddDelay"));
            else
                EffectLine.NewLine(StartPoint, EndPoint, EffectColor, GetKey("Width"), GetKey("AddFade"), GetKey("AddDelay"));
        }

        public override void CommonKeys()
        {
            // "Width": Width of the effect line
            // "AddFade": Fade duration change
            // "AddDelay": Start delay
            // "IgnoreTarget": Whether the line should be position based
            // "OverrideX": Override target position x
            // "OverrideY": Override target position y
            // "DeadDistance": Dead distance from source
            base.CommonKeys();
        }
    }
}