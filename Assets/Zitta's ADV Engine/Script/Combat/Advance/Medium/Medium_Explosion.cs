using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_Explosion : Medium {
        public List<GameObject> SubSignals;

        public override void EffectUpdate(float Value)
        {
            ChangeKey("Delay", -Value);
            if (GetKey("Delay") <= 0)
            {
                ExplosionEffect();
                EndEffect();
            }
        }

        public void ExplosionEffect()
        {
            if (!HasKey("Range"))
                SetKey("Range", 99999);
            List<Card> Cards = new List<Card>();
            List<Card> TempList = Targeting.TargetInRange(Position, GetKey("Range"));
            if (GetKey("TargetEnemy") > 0)
            {
                for (int i = 0; i < TempList.Count; i++)
                {
                    if (TempList[i].GetSide() != Source.GetSide())
                        Cards.Add(TempList[i]);
                }
            }
            if (GetKey("TargetFriendly") > 0)
            {
                for (int i = 0; i < TempList.Count; i++)
                {
                    if (TempList[i].GetSide() == Source.GetSide())
                        Cards.Add(TempList[i]);
                }
            }
            if (GetKey("TargetGlobalEnemy") > 0)
            {
                for (int i = 0; i < TempList.Count; i++)
                {
                    if (TempList[i].GetSide() != CombatControl.Main.GlobalCard.GetSide())
                        Cards.Add(TempList[i]);
                }
            }
            if (HasKey("TargetID"))
            {
                for (int i = 0; i < TempList.Count; i++)
                {
                    if (TempList[i].GetID() == GetKey("TargetID", true) && !Cards.Contains(TempList[i]))
                        Cards.Add(TempList[i]);
                }
            }
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (GetKey("IgnoreTarget") > 0 && Cards[i] == Target)
                    Cards.Remove(Cards[i]);
                else if (GetKey("IgnoreSource") > 0 && Cards[i] == Source)
                    Cards.Remove(Cards[i]);
            }
            if (HasKey("MinRange"))
            {
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    float Distance = (Cards[i].GetPosition() - Position).magnitude;
                    Distance += Cards[i].GetRadius();
                    if (Distance < GetKey("MinRange"))
                        Cards.RemoveAt(i);
                }
            }
            if (HasKey("VerticalPointX") || HasKey("VerticalPointY") || HasKey("HorizontalPointX") || HasKey("HorizontalPointY"))
            {
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    Card C = Cards[i];
                    Vector2 P = C.GetPosition();
                    float HDistance = StaticAssign.LineDistance(P, Position + new Vector2(GetKey("VerticalPointX"), GetKey("VerticalPointY")), Position);
                    float VDistance = StaticAssign.LineDistance(P, Position + new Vector2(GetKey("HorizontalPointX"), GetKey("HorizontalPointY")), Position);
                    HDistance -= C.GetRadius();
                    VDistance -= C.GetRadius();
                    if (HDistance > new Vector2(GetKey("HorizontalPointX"), GetKey("HorizontalPointY")).magnitude
                        || VDistance > new Vector2(GetKey("VerticalPointX"), GetKey("VerticalPointY")).magnitude)
                        Cards.RemoveAt(i);
                }
            }
            if (GetKey("AddCondition") == 1)
            {
                List<Condition> Conditions = new List<Condition>();
                foreach (Condition Con in GetComponentsInChildren<Condition>())
                    Conditions.Add(Con);
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    foreach (Condition con in Conditions)
                    {
                        if (!con.Pass(Cards[i]))
                        {
                            Cards.Remove(Cards[i]);
                            break;
                        }
                    }
                }
            }
            if (HasKey("MaxCount"))
            {
                while (Cards.Count > 0 && Cards.Count > GetKey("MaxCount"))
                    Cards.RemoveAt(Random.Range(0, Cards.Count));
            }
            SetKey("FinalCount", Cards.Count);
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Cards[i] == Target)
                    Effect(Cards[i]);
                else
                    SubEffect(Cards[i]);
            }
        }

        public virtual void SubEffect(Card Target)
        {
            if (!Source)
                return;
            List<string> AddKeys = new List<string>();
            foreach (GameObject G in SubSignals)
                Source.SendSignal(G, AddKeys, Target, Position);
        }

        public override void CommonKeys()
        {
            // "TargetEnemy": Whether the medium should target enemies
            // "TargetFriendly": Whether the medium should target friendly
            // "TargetGlobalEnemy": Whether the medium should target enemies of gloabl card
            // "TargetID": Whether to target card with specific id
            // "TargetUntargeted": Whether the explosion should include untargeted
            // "IgnoreTarget": Whether the explosion should exclude the original target
            // "IgnoreSource": Whether the explosion should exclude the source
            // "AddCondition": Whether there are additional conditions for targeting
            // "Range": Range of the explosion
            // "MinRange": Minimum range of the explosion
            // "VerticalPointX": Vertical end point of the explosion
            // "VerticalPointY": Vertical end point of the explosion
            // "HorizontalPointX": Vertical end point of the explosion
            // "HorizontalPointY": Vertical end point of the explosion
            // "MaxCount": Max target count
            // "Delay": The delay before final effect
            // "FinalCount": Final target count
            base.CommonKeys();
        }
    }
}