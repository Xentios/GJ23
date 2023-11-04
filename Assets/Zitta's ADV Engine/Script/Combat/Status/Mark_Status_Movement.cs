using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Movement : Mark_Status {

        public override void OnAssign()
        {
            if (HasKey("TargetX") && HasKey("TargetY"))
            {
                if (GetKey("FixDirection") == 1)
                {
                    Vector2 Direction = (new Vector2(GetKey("TargetX"), GetKey("TargetY")) - Source.GetPosition()).normalized;
                    SetKey("DirectionX", Direction.x);
                    SetKey("DirectionY", Direction.y);
                }
            }
            if (HasKey("ReferenceDistance"))
            {
                float Distance = (new Vector2(GetKey("TargetX"), GetKey("TargetY")) - Source.GetPosition()).magnitude;
                float Scale = Distance / GetKey("ReferenceDistance");
                SetKey("Speed", GetKey("Speed") * Scale);
                if (HasKey("SpeedDecay"))
                    SetKey("SpeedDecay", GetKey("SpeedDecay") * Scale);
            }
            base.OnAssign();
        }

        public override void TimePassed(float Value)
        {
            if (Source.PassValue("IgnoreMovement") >= 1)
                return;
            if (HasKey("DirectionX") && HasKey("DirectionY"))
            {
                Vector2 Direction = new Vector2(GetKey("DirectionX"), GetKey("DirectionY"));
                Source.ChangePosition(Direction.normalized * GetKey("Speed") * Value);
            }
            else if (HasKey("TargetX") && HasKey("TargetY"))
            {
                Vector2 Target = new Vector2(GetKey("TargetX"), GetKey("TargetY"));
                Vector2 Direction = Target - Source.GetPosition();
                float Distance = GetKey("Speed") * Value;
                if (Direction.magnitude < Distance)
                    Distance = Direction.magnitude;
                Source.ChangePosition(Direction.normalized * Distance);
            }
            if (HasKey("SpeedDecay"))
                ChangeKey("Speed", GetKey("SpeedDecay") * Value);
            base.TimePassed(Value);
        }

        public override void Stack(Mark_Status M)
        {
            base.Stack(M);
            if (HasKey("TargetX") && HasKey("TargetY"))
            {
                SetKey("TargetX", M.GetKey("TargetX"));
                SetKey("TargetY", M.GetKey("TargetY"));
                if (GetKey("FixDirection") == 1)
                {
                    Vector2 Direction = (new Vector2(GetKey("TargetX"), GetKey("TargetY")) - Source.GetPosition()).normalized;
                    SetKey("DirectionX", Direction.x);
                    SetKey("DirectionY", Direction.y);
                }
            }
        }

        public override void CommonKeys()
        {
            // "TargetX": Target point X
            // "TargetY": Target point Y
            // "FixDirection": Whether to fix direction when applying status
            // "Speed": Speed of movement
            // "SpeedDecay": Decay of speed
            // "ReferenceDistance": Whether to scale speed based on reference distance
            base.CommonKeys();
        }
    }
}