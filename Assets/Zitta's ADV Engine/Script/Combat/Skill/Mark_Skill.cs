using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Skill : Mark {
        public List<GameObject> MainSignals;
        public List<GameObject> Targetings;
        public List<GameObject> Conditions;
        [HideInInspector] public List<Targeting> Tars;
        [HideInInspector] public List<Condition> Cons;

        public void TryUse(Vector2 Position)
        {
            Card T = null;
            if (!CanUse())
                return;
            if (Tars.Count <= 0)
            {
                foreach (GameObject G in Targetings)
                {
                    Targeting Tar = G.GetComponent<Targeting>();
                    Tars.Add(Tar);
                }
            }
            foreach (Targeting Tar in Tars)
            {
                T = Tar.FindTarget(Source, Position);
                if (T)
                    break;
            }
            Use(T, Position);
        }

        public virtual void Use(Card Target, Vector2 Position)
        {
            if (!Target && GetKey("RequireTarget") == 1)
                return;
            OnUse(Target, Position);
            List<string> AddKeys = new List<string>();
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, Target, Position);
        }

        public virtual void OnUse(Card Target, Vector2 Position)
        {
            if (HasKey("CoolDown") && GetKey("CDMR") == 0)
            {
                if (HasKey("RCD"))
                    SetKey("CCD", GetKey("CoolDown") + Random.Range(-GetKey("RCD"), GetKey("RCD")));
                else
                    SetKey("CCD", GetKey("CoolDown"));
            }
            if (HasKey("AnimLock") && Source.AnimLock < GetKey("AnimLock"))
                Source.AnimLock = GetKey("AnimLock");
            foreach (Mark_Skill S in Source.Skills)
            {
                if (S.HasKey("SkillMod") && S.GetKey("SkillMod", true) == GetID())
                    S.TryUse(Position);
            }
        }

        public virtual void ForceUse(Card Target, Vector2 Position)
        {
            List<string> AddKeys = new List<string>();
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, Target, Position);
            // Do not trigger OnUse effects
        }

        public virtual void IntrinsicForceUse(Card Target, Vector2 Position)
        {
            if (!IntrinsicPass())
                return;
            // Ignore targeting & condition
            if (!Target && GetKey("RequireTarget") == 1)
                return;
            OnUse(Target, Position);
            List<string> AddKeys = new List<string>();
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, Target, Position);
        }

        public override void StartOfCombat()
        {
            if (HasKey("CoolDown") || HasKey("CCD"))
                SetKey("CCD", 0);
            if (GetKey("StartOfCombat") == 1)
                TryUse(new Vector2());
            if (HasKey("StartCoolDown"))
                SetKey("CCD", GetKey("StartCoolDown"));
            base.StartOfCombat();
        }

        public override void EndOfCombat()
        {
            if (HasKey("CoolDown") || HasKey("CCD"))
                SetKey("CCD", 0);
            if (GetKey("Temporary") == 1 && Source)
                Source.RemoveSkill(this);
            if (HasKey("CombatDuration") && Source)
            {
                ChangeKey("CombatDuration", -1);
                if (GetKey("CombatDuration") <= 0)
                    Source.RemoveSkill(this);
            }
            if (GetKey("SpellLock") == 1)
                SetKey("SpellLock", 0);
            base.EndOfCombat();
        }

        public override void Death()
        {
            if (HasKey("CoolDown"))
                SetKey("CCD", 0);
            if (GetKey("Temporary") == 1 && Source)
                Source.RemoveSkill(this);
            base.Death();
        }

        public virtual bool CanUse()
        {
            return (/*GetKey("CoolDown") <= 0 || */GetKey("CCD") <= 0)
                && GetKey("Passive") == 0 && Source && ConditionPass() && GetKey("Disable") == 0
                && (GetKey("IgnoreAnimLock") == 1 || Source.AnimLock <= 0);
        }

        public virtual bool IntrinsicPass()
        {
            return (GetKey("CCD") <= 0) && GetKey("Passive") == 0 && Source && GetKey("Disable") == 0 && (GetKey("IgnoreAnimLock") == 1 || Source.AnimLock <= 0);
        }

        public virtual bool ConditionPass()
        {
            if (Cons.Count <= 0)
            {
                foreach (GameObject G in Conditions)
                {
                    Condition C = G.GetComponent<Condition>();
                    Cons.Add(C);
                }
            }
            foreach (Condition C in Cons)
            {
                if (!C.Pass(Source))
                    return false;
            }
            return true;
        }

        public bool CanItemStack(Mark_Skill S)
        {
            return HasKey("Count") && S.HasKey("Count");
        }

        public void ItemStack(Mark_Skill S)
        {
            if (!CanItemStack(S))
                return;
            ChangeKey("Count", S.GetKey("Count"));
        }

        public override void TimePassed(float Value)
        {
            if (HasKey("CoolDown") || HasKey("CCD"))
            {
                if (GetKey("ASCD") != 0)
                    ChangeKey("CCD", -Value);
                else
                    ChangeKey("CCD", -Value);
                if (GetKey("CCD") < 0)
                    SetKey("CCD", 0);
            }
            if (HasKey("CurrentAutoDelay"))
            {
                ChangeKey("CurrentAutoDelay", -Value);
            }
            base.TimePassed(Value);
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Mod" && GetKey("Mod") == 1)
                return 1;
            return base.PassValue(Key, Value);
        }

        public override bool Active()
        {
            return CanUse();
        }

        public override void CommonKeys()
        {
            // "CoolDown": Original cool down
            // "CCD": Current cool down
            // "RCD": Random cool down range
            // "CDMR": Whether cool down should be resetted by external signal
            // "StartCoolDown": Cool down to start the combat with
            // "AnimLock": Source AnimLock Duration
            // "Passive": Whether the skill is passive
            // "Disable": Whether the skill is disabled
            // "Permanent": Whether the skill cannot be replaced
            // "Temporary": Whether the skill should be removed after combat
            // "CombatDuration": Current combat duration (skill removed at 0)
            // "Auto": Whether the skill will automatically be used
            // "Auto_Death": Whether the skill will automatically be used in death
            // "AutoPriority": Priority when choosing skills
            // "AutoDelay": Delay for trying to auto cast
            // "CurrentAutoDelay": Current auto delay
            // "Duel": Whether the skill is a dueling skill
            // "DuelPriority": Priority when choosing dueling skills
            // "ManaCost": Mana cost (0 ~ 1)
            // "FateCost": Global Fate cost
            // "FateCost_All": Render Global Fate cost as 'all'
            // "PreCombat": Whether the skill should be used before combat
            // "PreCombat_Summon": Whether the pre combat skill should be used before other (for summoning)
            // "PreCombat_Early": Whether the pre combat skill should be used early (for global passive)
            // "PostCombat": Whether the skill should be used after combat
            // "PostCombat_Death": Whether the skill should be used after combat while dead
            // "OnDeath": Whether the skill should be used on death
            // "EndOfTurn": Whether the skill should be used at the end of a turn
            // "EndOfTurn_Death": Whether the skill should be used at the end of a turn in death
            // "StartOfCombat": Whether the skill should be used at the start of combat
            // "EndOfCombat": Whether the skill should be used at the end of combat
            // "OnStressUp": Whether the skill should be used when level up
            // "OnStressDown": Whether the skill should be used when level down
            // "InvokeChannel": Whether the skill can be invoked with channel value
            // "OutOfCombat": Whether the skill should be used when the source is not in combat
            // "OutOfCombat_Early": Whether the OutOfCombat skill should be used early
            // "StartOfLoop": Whether the skill should be used at the start of each loop
            // "Spell": Whether the skill is the active skill of the card
            // "SpellLock": Whether the spell is used
            // "Mod": Whether the skill is a mod
            // "EventSkill": Whether the skill is an event skill
            // "SkillMod": Whether the skill should be used after another skill
            // "RequireTarget": Whether to only use the skill when there's a target
            // "CursorPosition": (For Auto) Whether to use on the cursor position when auto-use
            base.CommonKeys();
        }
    }
}