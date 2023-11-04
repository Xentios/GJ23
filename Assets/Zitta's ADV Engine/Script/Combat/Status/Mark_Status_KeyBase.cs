using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_KeyBase : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (HasKey(Key) && Key != "DamageOverride" && Key != "LifeOverride" && Key != "AbsLifeOverride")
                return GetKey(Key);
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "AggroIndex": Current aggro index
            // "IniIndex": Aggro index at the start of combat
            // "Rank": Current positional rank
            // "Summon": Whether the source is summoned and should be destroyed after combat
            // "Temporary": Whether the source should be destroyed after combat
            // "Revive": Whether the source should be revived after combat
            // "Hidden": Whether the source should be hidden in card slots
            // "NoDamage": Whether the source cannot deal any damage
            // "DeathTurn": Turn count when the source died
            // "Spell": Whether the source is a spell
            // "Undeployed": Whether the source cannot be deployed
            // "DamageOverride": Base damage value override (StatOverride)
            // "LifeOverride": Max life value override (StatOverride)
            // "AbsLifeOverride": Absolute life value override (StatOverride)
            // "AddCost": Additional cost
            // "FixedDamage": Whether to render the damage as blue
            // "IgnoreDamage": Whether to not render the damage value
            // "IgnoreHealth": Whether to not render the health value
            // "HighlightDamage": Whether to highlight the damage value
            // "HighlightLife": Whether to highlight the health value
            // "DoubleAttack": Whether source can double attack
            // "TripleAttack": Whether source can triple attack
            // "SkipAttack": Whether source will skip attack

            // "Event": Whether source is a event
            // "Resolved": Whether the source event is alredy resolved

            // "LastStress": The stress value when stress process begin
            // "LastStressResult": The stress change for certain stress condition
            // "LonelyCount": Adj Lonely friendly count
            // "DeathIndex": Order when the card died
            // "EncounterScaling": Whether the source should scale with encounter
            // "Artifact": Wehther the source is an artifact
            // "Construct": Whether the source is a construct
            // "Ability": Whether the source has active ability
            // "Slow": Whether the source has default slow value
            // "Choice": Whether the source is a choice
            // "Free": Whether the source is free to buy
            // "Boss": Whether the source is a boss
            // "FinalBoss": Whether the source is the final boss
            // "Sleep": Whether the boss is not active
            // "SleepTime": Time until the boss is auto awake
            // "Path": Whether the source is a path
            // "Core": Whether the source is the core
            // "IgnoreLock": Whether the source cannot be locked
            // "Locked": Whether the source is always locked
            // "IgnoreCount": Whether the source can be recruited even when the slot is full
            // "IgnoreRecruitCount": Whether the source should not be influenced by recruit count
            // "RefreshOnRecruit": Whether to refresh recruit panel on recruit
            // "GenerateOnRecruit": Whether to generate a new card on recruit
            // "RecruitTierGrowth": Cost grwoth for each recruit panel tier
            // "FriendlyDamage": Whether the damage value should be rendered as negative effect
            // "BanishCount": How many card to banish during current boss (on Boss)
            // "Sculpture": Whether the source is banished
            // "NoAttack": Whether the source has irrelevant attack damage value
            // "EmpowerCount": How many enemies to empower each turn (on Boss)
            // "NewBoss": Whether the boss is new (on Boss)
            // "Unique": Whether the card can only exist one
            // "StartRecruit": Whether to recruit at the start of encounter
            // "AddLevel": Additional stack level
            // "LockBuff": How many times the source has been buffed by locked effect (cost reduction)
            // "RequireTarget": Whether the card require a target when recruited
            // "EffectValue": Special effect value
            // "IgnoreIndexRange": Whether to ignore index range condition with targeting
            // "DefeatLock": Whether to prevent defeat in combat

            // "GlobalStress": Global stress level (on Gloabl Card)
            // "GlobalStressModChange": Global life and damage mod change per stress (on Global Card)
            // "ArtifactStress": Global stress level required to unlock the next artifact (on Global Card)
            // "ArtifactStressGrowth": Global stress level increase after each unlock (on Global Card)
            // "ArtifactStressGrowthII": ArtifactStressGrowth increase after each unlock (on Global Card)
            // "EncounterLevel": Encounter level (on Global Card)
            // "EcnounterLevelModChange": Encounter life and damage mod change per level (on Global Card)
            // "CombatDuration": Current combat duration, combat end at 0 (on Global Card)
            // "OriCombatDuration": Starting combat duration (on Global Card)
            base.CommonKeys();
        }
    }
}