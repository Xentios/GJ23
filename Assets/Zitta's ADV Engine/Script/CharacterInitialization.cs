using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ADV
{
    public class CharacterInitialization : MonoBehaviour {
        public string TargetID;
        public Card TargetCharacter;
        public bool HasTarget;
        [Space]
        public WeaponType Weapon;
        public float DamageOutputMod;
        public float DamageTakenMod;
        public bool FireDamageActive;
        public float IgniteChance;
        public bool ColdDamageActive;
        public float ChillChance;
        public bool LightningDamageActive;
        public float ShockChance;
        public float FireDamageTakenMod;
        public float ColdDamageTakenMod;
        public float LightningDamageTakenMod;
        public float RegenerationRate;
        public float DamageAuraChance;
        [Space]
        public List<GameObject> BossWeaponSet;
        public List<GameObject> SwordWeaponSet;
        public List<GameObject> AxeWeaponSet;
        public List<GameObject> BowWeaponSet;
        public List<GameObject> StaffWeaponSet;
        public GameObject DamageBuff;
        public GameObject DamageResistance;
        public GameObject FireDamage;
        public GameObject Ignite;
        public GameObject ColdDamage;
        public GameObject Chill;
        public GameObject LightningDamage;
        public GameObject Shock;
        public GameObject FireResistance;
        public GameObject ColdResistance;
        public GameObject LightningResistance;
        public GameObject Regeneration;
        public GameObject AuraActivate;
        public GameObject AuraEffect;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!CombatControl.Main)
                HasTarget = false;
            if (!TargetCharacter && CombatControl.Main && !HasTarget)
            {
                foreach (Card C in CombatControl.Main.Cards)
                {
                    if (C.GetID() == TargetID)
                    {
                        HasTarget = true;
                        TargetCharacter = C;
                        Initialization(C);
                        break;
                    }
                }
            }
        }

        public void Initialization(Card C)
        {
            if (Weapon == WeaponType.Boss)
            {
                foreach (GameObject G in BossWeaponSet)
                    AddMark(C, G, new List<string>());
            }
            else if (Weapon == WeaponType.Sword)
            {
                foreach (GameObject G in SwordWeaponSet)
                    AddMark(C, G, new List<string>());
            }
            else if (Weapon == WeaponType.Axe)
            {
                foreach (GameObject G in AxeWeaponSet)
                    AddMark(C, G, new List<string>());
            }
            else if (Weapon == WeaponType.Bow)
            {
                foreach (GameObject G in BowWeaponSet)
                    AddMark(C, G, new List<string>());
            }
            else if (Weapon == WeaponType.Staff)
            {
                foreach (GameObject G in StaffWeaponSet)
                    AddMark(C, G, new List<string>());
            }

            if (DamageOutputMod != 1)
                AddMark(C, DamageBuff, new List<string>() { "DamageMod[" + DamageOutputMod });
            if (DamageTakenMod != 1)
                AddMark(C, DamageResistance, new List<string>() { "DamageMod[" + DamageTakenMod });
            if (FireDamageActive)
                AddMark(C, FireDamage, new List<string>());
            if (IgniteChance > 0)
                AddMark(C, Ignite, new List<string>() { "Chance[" + IgniteChance });
            if (ColdDamageActive)
                AddMark(C, ColdDamage, new List<string>());
            if (ChillChance > 0)
                AddMark(C, Chill, new List<string>() { "Chance[" + ChillChance });
            if (LightningDamageActive)
                AddMark(C, LightningDamage, new List<string>());
            if (ShockChance > 0)
                AddMark(C, Shock, new List<string>() { "Chance[" + ShockChance });
            if (FireDamageTakenMod != 1)
                AddMark(C, FireResistance, new List<string>() { "DamageMod[" + FireDamageTakenMod });
            if (ColdDamageTakenMod != 1)
                AddMark(C, ColdResistance, new List<string>() { "DamageMod[" + ColdDamageTakenMod });
            if (LightningDamageTakenMod != 1)
                AddMark(C, LightningResistance, new List<string>() { "DamageMod[" + LightningDamageTakenMod });
            if (RegenerationRate > 0)
                AddMark(C, Regeneration, new List<string>() { "Heal[" + RegenerationRate });
            if (DamageAuraChance > 0)
            {
                AddMark(C, AuraActivate, new List<string>() { "Chance[" + DamageAuraChance });
                AddMark(C, AuraEffect, new List<string>());
            }
        }

        public void AddMark(Card C, GameObject Mark, List<string> AddKeys)
        {
            if (Mark.GetComponent<Mark_Status>())
            {
                Mark_Status Status = C.AddStatus(Mark.GetComponent<Mark_Status>(), C);
                foreach (string s in AddKeys)
                    Status.ChangeKey(KeyBase.Translate(s, out float Value), Value);
            }
            else if (Mark.GetComponent<Mark_Skill>())
            {
                Mark_Skill Skill = C.AddSkill(Mark.GetComponent<Mark_Skill>());
                foreach (string s in AddKeys)
                    Skill.ChangeKey(KeyBase.Translate(s, out float Value), Value);
            }
        }
    }

    public enum WeaponType
    {
        Boss,
        Sword,
        Axe,
        Bow,
        Staff,
    }
}