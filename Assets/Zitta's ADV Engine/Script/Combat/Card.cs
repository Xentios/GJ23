using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace ADV
{
    public class Card : MonoBehaviour {
        public float MaxLife;
        public float Life;
        [HideInInspector] public float BaseDamage;
        [HideInInspector] public float OriLife;
        [HideInInspector] public float OriDamage;
        [Space]
        public int Side;
        [HideInInspector] public CardInfo Info;
        [HideInInspector] public bool AlreadyDead;
        [HideInInspector] public bool AlreadyRemoved;
        [HideInInspector] public bool Acting;
        [HideInInspector] public float AnimLock;
        [Space]
        public Vector2 Position;
        public float Radius;
        public CardAnim Anim;
        public CardMovement Movement;
        [Space]
        [HideInInspector] public Card Summoner;
        [HideInInspector] public Card Source;
        public List<GameObject> IniDefaults;
        public List<GameObject> IniSkills;
        public List<GameObject> IniStatus;
        [Space]
        public Mark KeyMark;
        [Space]
        public List<Mark_Skill> Skills;
        public List<Mark_Status> Status;
        [HideInInspector] public List<Mark_Skill> RenderSkills;
        [HideInInspector] public List<Mark_Status> RenderStatus;
        [HideInInspector] public Mark_Skill Spell;
        public List<Signal> WaitingSignals;

        public void Awake()
        {
            if (CombatControl.Main)
                CombatControl.Main.AllCards.Add(this);

            if (Position.x == 0 && Position.y == 0)
                Position = transform.position;
            OriLife = MaxLife;
            OriDamage = BaseDamage;

            Skills = new List<Mark_Skill>();
            RenderSkills = new List<Mark_Skill>();
            for (int i = 0; i < IniDefaults.Count; i++)
            {
                if (IniDefaults[i] && IniDefaults[i].GetComponent<Mark_Skill>())
                    Skills.Add(IniDefaults[i].GetComponent<Mark_Skill>());
            }
            for (int i = 0; i < IniSkills.Count; i++)
            {
                if (IniSkills[i])
                    Skills.Add(IniSkills[i].GetComponent<Mark_Skill>());
            }
            foreach (Mark_Skill M in Skills)
            {
                if (M)
                {
                    M.Source = this;
                    M.OnAssign();
                }
            }

            Status = new List<Mark_Status>();
            RenderStatus = new List<Mark_Status>();
            if (KeyMark)
                Status.Add((Mark_Status)KeyMark);
            for (int i = 0; i < IniDefaults.Count; i++)
            {
                if (IniDefaults[i] && IniDefaults[i].GetComponent<Mark_Status>())
                    Status.Add(IniDefaults[i].GetComponent<Mark_Status>());
            }
            for (int i = 0; i < IniStatus.Count; i++)
            {
                if (IniStatus[i])
                    Status.Add(IniStatus[i].GetComponent<Mark_Status>());
            }
            foreach (Mark_Status M in Status)
            {
                if (M)
                {
                    M.Source = this;
                    M.OnAssign();
                }
            }

            float Scale = Life / MaxLife;
            SetLife(Scale * GetMaxLife());

            SetKey("Life", Life);
            SetKey("MaxLife", MaxLife);
            SetKey("BaseDamage", BaseDamage);
        }

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            CombatUpdate(CombatControl.Main.CombatTime());
        }

        public void LateUpdate()
        {
            LateCombatUpdate();
        }

        public void CombatUpdate(float Value)
        {
            if (!CombatControl.Main.InCombat || !CombatControl.Main.CardInCombat(this))
                return;

            TimePassed(Value);
            
            for (int i = 0; i < Skills.Count; i++)
            {
                if (Skills[i] && Skills[i].GetKey("Auto") == 1)
                {
                    if (!Skills[i].HasKey("CurrentAutoDelay") || Skills[i].GetKey("CurrentAutoDelay") <= 0)
                    {
                        Vector2 P = GetPosition();
                        if (Skills[i].GetKey("CursorPosition") == 1)
                            P = Cursor.Main.GetCombatPosition();
                        Skills[i].TryUse(P);
                        if (Skills[i].HasKey("AutoDelay"))
                            Skills[i].SetKey("CurrentAutoDelay", Skills[i].GetKey("AutoDelay"));
                    }
                }
            }

            for (int i = WaitingSignals.Count - 1; i >= 0; i--)
            {
                if (WaitingSignals[i].GetKey("Delay") >= 0)
                    WaitingSignals[i].ChangeKey("Delay", -Value);
                else
                {
                    Signal S = WaitingSignals[i];
                    WaitingSignals.RemoveAt(i);
                    RealSendSignal(S);
                }
            }

            SetKey("Life", Life);
            SetKey("MaxLife", MaxLife);

            if (HasKey("ActingDelay"))
                ChangeKey("ActingDelay", -Value);

            DeathUpdate();
        }

        public void TimePassed(float Value)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].TimePassed(Value);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].TimePassed(Value);

            if (Movement)
                Movement.MovementUpdate(Value);

            float Decay = PassValue("Decay");
            if (Decay > 0 && GetLife() > 0)
                ChangeLife(-Decay * Value);

            AnimLock -= Value;
            if (AnimLock < 0)
                AnimLock = 0;
        }

        public void LateCombatUpdate()
        {
            if (!CombatControl.Main.InCombat || !CombatControl.Main.CardInCombat(this))
                DeathUpdate();
        }

        public void StartOfCombat()
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i])
                    Skills[i].StartOfCombat();
            }
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].StartOfCombat();
        }

        public void EndOfCombat()
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i])
                    Skills[i].EndOfCombat();
            }
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].EndOfCombat();
            RemoveWaitingSignals();
        }

        public IEnumerator DelayRemoveSkill(Mark_Skill Skill)
        {
            yield return new WaitForSeconds(5);
            RemoveSkill(Skill);
        }

        public void UseSkill(Mark_Skill Skill, Vector2 Position)
        {
            Skill.TryUse(Position);
        }

        public void SendSignal(GameObject Prefab, List<string> AddKeys, Card Target, Vector2 Position)
        {
            GameObject G = Instantiate(Prefab);
            Signal S = G.GetComponent<Signal>();
            S.Source = this;
            S.IniAddKeys(AddKeys);
            if (S.GetKey("Target") == 1)
                S.Target = Target;
            else if (S.GetKey("Target") == 0)
                S.Target = this;
            S.Position = Position;
            S.Ini();
            if (S.GetKey("Delay") <= 0)
                RealSendSignal(S);
            else
                WaitingSignals.Add(S);
        }

        public void RealSendSignal(Signal S)
        {
            /*if (Source && S.GetKey("RealSource") == 0)
            {
                S.Source = Source;
                Source.RealSendSignal(S);
                return;
            }*/
            if (S.GetKey("RequireTarget") == 1 && !S.Target)
                return;
            S.StartEffect();
            if (S.Source)
                S.Source.OutputSignal(S);
            if (Source)
                Source.OutputSignal(S);
            if (S.Target)
                S.Target.InputSignal(S);
            S.EndEffect();
            if (!S)
                return;
            if (S.Source)
                S.Source.ReturnSignal(S);
            if (Source)
                Source.ReturnSignal(S);
            if (S.Target)
                S.Target.ConfirmSignal(S);
            Destroy(S.gameObject, 5);
        }

        public void RemoveWaitingSignals()
        {
            for (int i = WaitingSignals.Count - 1; i >= 0; i--)
            {
                Destroy(WaitingSignals[i].gameObject, 5);
                WaitingSignals.RemoveAt(i);
            }
        }

        public float PassValue(string Key)
        {
            return PassValue(Key, 0);
        }

        public float PassValue(string Key, float Value)
        {
            float V = Value;
            for (int i = Status.Count - 1; i >= 0; i--)
                V = Status[i].PassValue(Key, V);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    V = Skills[i].PassValue(Key, V);
            return FinalPassValue(Key, V);
        }

        public float FinalPassValue(string Key, float Value)
        {
            if (Key == "Life")
                return Life;
            else if (Key == "MaxLife")
                return GetMaxLife();
            else
                return Value;
        }

        public void InvokeSkill(string ID, Vector2 Position)
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (!Skills[i])
                    continue;
                if (Skills[i].GetID() == ID)
                    Skills[i].TryUse(Position);
            }
        }

        public void InvokeSkill(float Channel, Vector2 Position)
        {
            List<Mark_Skill> S = new List<Mark_Skill>();
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (!Skills[i])
                    continue;
                if (Skills[i].HasKey("InvokeChannel") && Skills[i].GetKey("InvokeChannel") == Channel)
                    S.Add(Skills[i]);
            }
            for (int i = S.Count - 1; i >= 0; i--)
                S[i].TryUse(Position);
        }

        public void InvokeSkill(string Channel, bool StringMode, Vector2 Position)
        {
            List<Mark_Skill> S = new List<Mark_Skill>();
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (!Skills[i])
                    continue;
                if (Skills[i].HasKey("InvokeChannel") && Skills[i].GetKey("InvokeChannel", StringMode) == Channel)
                    S.Add(Skills[i]);
            }
            for (int i = S.Count - 1; i >= 0; i--)
                S[i].TryUse(Position);
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public void ChangePosition(Vector2 Value)
        {
            ChangePosition(Value.x, Value.y);
        }

        public void ChangePosition(float X, float Y)
        {
            Vector2 a = GetPosition();
            SetPosition(a.x + X, a.y + Y);
        }

        public void SetPosition(Vector2 Value)
        {
            SetPosition(Value.x, Value.y);
        }

        public void SetPosition(float X, float Y)
        {
            if (CombatControl.Main.InCombat)
            {
                if (X + GetRadius() > CombatControl.CombatRangeRight)
                    X = CombatControl.CombatRangeRight - GetRadius();
                if (X - GetRadius() < CombatControl.CombatRangeLeft)
                    X = CombatControl.CombatRangeLeft + GetRadius();
                if (Y + GetRadius() > CombatControl.CombatRangeUp)
                    Y = CombatControl.CombatRangeUp - GetRadius();
                if (Y - GetRadius() < CombatControl.CombatRangeDown)
                    Y = CombatControl.CombatRangeDown + GetRadius();
            }

            Position = new Vector2(X, Y);
            transform.position = new Vector3(Position.x, Position.y, transform.position.z);
        }

        public float GetRadius()
        {
            return Radius;
        }

        public CardAnim GetAnim()
        {
            return Anim;
        }

        public void DeathUpdate()
        {
            if (GetLife() <= 0 && GetLife() != -999 && !AlreadyDead)
                Death();
        }

        public void Death()
        {
            if (AlreadyDead)
                return;

            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (!Skills[i])
                    continue;
                if (Skills[i].GetKey("OnDeath") == 1)
                    Skills[i].TryUse(GetPosition());
            }

            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].Death();
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].Death();

            if (GetAnim())
                GetAnim().OnDeath();

            AlreadyDead = true;
            CombatControl.Main.OnCardDeath(this);
        }

        public void Destroy()
        {
            AlreadyRemoved = true;
            CombatControl.Main.OnCardDestroy(this);
            Destroy(gameObject, 1f);
            gameObject.SetActive(false);
        }

        public void Revive()
        {
            //print("OnRevive");
            SetLife(GetMaxLife());
            if (AlreadyDead)
            {
                AlreadyDead = false;
            }
        }

        public float GetLife()
        {
            if (GetKey("AbsLifeOverride") == 1)
                return StaticAssign.RoundUp(PassValue("AbsLifeOverride", 2f));
            return Life;
        }

        public void ChangeLife(float Value)
        {
            float V = Value;
            for (int i = Status.Count - 1; i >= 0; i--)
            {
                if (V < 0 && Status[i].GetComponent<Mark_Status_Shield>())
                    V = Status[i].GetComponent<Mark_Status_Shield>().ProcessLifeChange(V);
                if (V < 0 && Life + V <= 0 && Status[i].GetComponent<Mark_Trigger_DeathLock>())
                    V = Status[i].GetComponent<Mark_Trigger_DeathLock>().ProcessLifeChange(V);
            }
            if (Life + V > GetMaxLife())
                V = GetMaxLife() - Life;
            SetLife(Life + V);
            SetKey("Life", Life);
        }

        public void SetLife(float Value)
        {
            Life = Value;
        }

        public float GetMaxLife()
        {
            if (GetKey("LifeOverride") == 1)
                return StaticAssign.RoundUp(PassValue("LifeOverride"));
            return StaticAssign.RoundUp((MaxLife + PassValue("AddLife", 0)) * PassValue("LifeMod", 1));
        }

        public float GetMaxLife_Permanent()
        {
            if (GetKey("LifeOverride") == 1)
                return StaticAssign.RoundUp(PassValue("LifeOverride"));
            return StaticAssign.RoundUp((MaxLife + PassValue("AddLife_Permanent", 0)) * PassValue("LifeMod", 1));
        }

        public void SetMaxLife(float Value)
        {
            float Scale = GetLife() / GetMaxLife();
            MaxLife = Value;
            SetLife(Scale * MaxLife);
            if (Life > GetMaxLife())
                Life = GetMaxLife();
            SetKey("Life", Life);
            SetKey("MaxLife", MaxLife);
        }

        public float GetShield()
        {
            return PassValue("Shield", 0);
        }

        public float GetBaseDamage()
        {
            if (GetKey("DamageOverride") == 1 || PassValue("TryDamageOverride") == 1)
                return StaticAssign.RoundUp(PassValue("DamageOverride"));
            return StaticAssign.RoundUp((BaseDamage + PassValue("AddDamage", 0)) * PassValue("DamageMod", 1));
        }

        public float GetBaseDamage_Permanent()
        {
            if (GetKey("DamageOverride") == 1 || PassValue("TryDamageOverride") == 1)
                return StaticAssign.RoundUp(PassValue("DamageOverride"));
            return StaticAssign.RoundUp((BaseDamage + PassValue("AddDamage_Permanent", 0)) * PassValue("DamageMod", 1));
        }

        public void SetBaseDamage(float Value)
        {
            if (Value < 0)
                Value = 0;
            BaseDamage = Value;
            SetKey("BaseDamage", BaseDamage);
        }

        public void ChangeBaseDamage(float Value)
        {
            SetBaseDamage(BaseDamage + Value);
        }

        public float GetSourceDamage()
        {
            if (GetKey("DamageOverride") == 1 || PassValue("TryDamageOverride") == 1)
                return StaticAssign.RoundUp(PassValue("DamageOverride"));
            return BaseDamage;
        }

        public int GetSide()
        {
            return Side;
        }

        public bool CombatActive()
        {
            return !AlreadyDead;
        }

        public bool CardActive()
        {
            return !AlreadyDead;
        }

        //-------------------------------------------------------------------

        public void OutputSignal(Signal S)
        {
            if (S.GetKey("IgnoreSource") != 0)
                return;

            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].OutputSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].OutputSignal(S);
        }

        public void InputSignal(Signal S)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].InputSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].InputSignal(S);

            for (int i = Status.Count - 1; i >= 0; i--)
                S.InputMark(Status[i]);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    S.InputMark(Skills[i]);
        }

        public void ReturnSignal(Signal S)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].ReturnSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].ReturnSignal(S);
        }

        public void ConfirmSignal(Signal S)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].ConfirmSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].ConfirmSignal(S);
        }

        public Mark_Skill GetSkill(int Index)
        {
            if (Index < 0 || Index >= Skills.Count)
                return null;
            return Skills[Index];
        }

        public Mark_Skill GetSkill(string ID, out int Index)
        {
            Index = -1;
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i] && Skills[i].GetID() == ID)
                {
                    Index = i;
                    return Skills[i];
                }
            }
            return null;
        }

        public Mark_Skill GetRenderSkill(int Index)
        {
            if (Index < 0 || Index >= RenderSkills.Count)
                return null;
            return RenderSkills[Index];
        }

        public Mark_Skill AddSkill(Mark_Skill M, int Index)
        {
            if (Index >= Skills.Count)
                return null;
            GameObject G = Instantiate(M.gameObject);
            G.transform.parent = transform;
            Mark_Skill S = G.GetComponent<Mark_Skill>();
            S.Source = this;
            if (S.HasKey("Count") && S.GetInfo() && !S.HasKey("IgnoreStack"))
            {
                for (int i = Skills.Count - 1; i >= 0; i--)
                {
                    if (!Skills[i] || !Skills[i].GetInfo())
                        continue;
                    if (Skills[i].GetID() == S.GetID())
                    {
                        if (Skills[i].CanItemStack(S))
                        {
                            Skills[i].ItemStack(S);
                            Destroy(S.gameObject);
                            return null;
                        }
                        else
                            break;
                    }
                }
            }
            if (Index < 0)
            {
                Destroy(G);
                return null;
            }
            if (Skills[Index])
                RemoveSkill(Index);
            Skills[Index] = S;
            S.OnAssign();
            return S;
        }

        public Mark_Skill AddSkill(Mark_Skill M)
        {
            int n = GetNextSkillIndex();
            return AddSkill(M, n);
        }

        public int GetNextSkillIndex()
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                if (!Skills[i])
                    return i;
            }
            Skills.Add(null);
            return Skills.Count - 1;
        }

        public void RemoveSkill(int Index)
        {
            if (Index < 0 || Index >= Skills.Count)
                return;
            if (Skills[Index])
                Destroy(Skills[Index].gameObject, 1f);
            Skills.RemoveAt(Index);
        }

        public void RemoveSkill(Mark_Skill M)
        {
            if (!M || !Skills.Contains(M))
                return;
            RemoveSkill(Skills.IndexOf(M));
        }

        public void RemoveSkill(string ID)
        {
            Mark_Skill M = GetSkill(ID, out int Index);
            if (!M)
                return;
            RemoveSkill(Index);
        }

        public bool HasSkill(string ID, out int Index)
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i] && Skills[i].GetID() == ID)
                {
                    Index = i;
                    return true;
                }
            }
            Index = -1;
            return false;
        }

        public bool HasSkill(Mark_Skill Skill, out int Index)
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i] == Skill)
                {
                    Index = i;
                    return true;
                }
            }
            Index = -1;
            return false;
        }

        public Mark_Skill GetSpell()
        {
            return Spell;
        }

        public bool HasMark(string ID)
        {
            if (HasSkill(ID, out _))
                return true;
            if (GetStatus(ID))
                return true;
            return false;
        }

        public Mark_Status GetStatus(int Index)
        {
            if (Index < 0 || Index >= RenderStatus.Count)
                return null;
            return RenderStatus[Index];
        }

        public Mark_Status GetStatus(string ID)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
            {
                if (Status[i] && Status[i].GetID() == ID)
                    return Status[i];
            }
            return null;
        }

        public Mark_Status AddStatus(Mark_Status M, Card Caster)
        {
            return AddStatus(M, new List<string>(), Caster);
        }

        public Mark_Status AddStatus(Mark_Status M, List<string> AddKeys, Card Caster)
        {
            GameObject G = Instantiate(M.gameObject);
            G.transform.parent = transform;
            Mark_Status S = G.GetComponent<Mark_Status>();
            S.Source = this;
            S.Caster = Caster;
            foreach (string s in AddKeys)
                S.SetKey(KeyBase.Translate(s, out float v), v);
            if (S.GetInfo() && S.HasKey("MaxDuplicateCount"))
            {
                int DuplicateCount = 0;
                for (int i = Status.Count - 1; i >= 0; i--)
                {
                    if (!Status[i].GetInfo())
                        continue;
                    if (Status[i].GetID() == S.GetID())
                        DuplicateCount++;
                }
                if (DuplicateCount >= S.GetKey("MaxDuplicateCount"))
                {
                    Destroy(S.gameObject);
                    return null;
                }
            }
            if (S.GetInfo() && S.GetKey("IgnoreStack") == 0)
            {
                for (int i = Status.Count - 1; i >= 0; i--)
                {
                    if (!Status[i].GetInfo())
                        continue;
                    if (S.GetKey("CasterStack") == 1 && S.Caster != Status[i].Caster)
                        continue;
                    if (Status[i].GetID() == S.GetID())
                    {
                        Status[i].Stack(S);
                        Destroy(S.gameObject);
                        return null;
                    }

                }
            }
            Status.Add(S);
            S.OnAssign();
            return S;
        }

        public void RemoveStatus(Mark_Status M)
        {
            if (!Status.Contains(M))
                return;
            Status.Remove(M);
            Destroy(M.gameObject);
        }

        public void RemoveStatus(string ID)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
            {
                if (Status[i].GetID() == ID)
                {
                    Mark_Status S = Status[i];
                    RemoveStatus(S);
                }
            }
        }

        public CardInfo GetInfo()
        {
            if (!Info)
                Info = GetComponent<CardInfo>();
            return Info;
        }

        public string GetName()
        {
            return GetInfo().GetName();
        }

        public string GetID()
        {
            return GetInfo().GetID();
        }

        public string GetGenerationKey()
        {
            return GetInfo().GetGenerationKey();
        }

        public string GetDescription()
        {
            return GetInfo().GetDescription();
        }

        //-------------------------------------------------------------------

        public bool HasKey(string Key)
        {
            if (!KeyMark)
                return false;
            return KeyMark.HasKey(Key);
        }

        public float GetKey(string Key)
        {
            if (!KeyMark)
                return 0;
            return KeyMark.GetKey(Key);
        }

        public string GetKey(string Key, bool StringMode)
        {
            if (!KeyMark)
                return "";
            return KeyMark.GetKey(Key, StringMode);
        }

        public float ChangeKey(string Key, float Value)
        {
            if (!KeyMark)
                return 0;
            return KeyMark.ChangeKey(Key, Value);
        }

        public void SetKey(string Key, float Value)
        {
            if (!KeyMark)
                return;
            KeyMark.SetKey(Key, Value);
        }

        public void SetKey(string Key, string Value)
        {
            if (!KeyMark)
                return;
            KeyMark.SetKey(Key, Value);
        }
    }
}