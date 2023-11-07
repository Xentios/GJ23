using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace ADV
{
    public class CombatControl : MonoBehaviour {
        public static CombatControl Main;
        public float GameID;
        public float TimeScale = -1;
        public Card GlobalCard;
        [Space]
        public float TimeLimit;
        [Space]
        public Mark_Skill QueuingSkill;
        public float QueueTime;
        public float InputProtection;
        [Space]
        public string RandomizeCode;
        public int CurrentCodeIndex;
        public List<string> UsedRandomizeCodes;
        public int StartCode;
        [Space]
        public bool InCombat;
        public int EnemyCount;
        [HideInInspector] public List<Card> AllCards;
        public List<Card> Cards;
        public List<Medium> Mediums;
        [HideInInspector] public List<string> DeathCards;

        public void Awake()
        {
            StartCode = UnityEngine.Random.Range(1000, 10000);
            GameID = UnityEngine.Random.Range(0.001f, 999999.99f);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!InCombat)
                return;

            QueueUpdate();

            InputProtection -= Time.deltaTime;
        }

        public void LateUpdate()
        {

        }

        public void StartCombat()
        {
            InCombat = true;
            TimeLimit = 15f;

            InputProtection = 0.25f;

            ForceResetMedium();
            GlobalCard.StartOfCombat();
            foreach (Card C in Cards)
                C.StartOfCombat();

            UIControl.Main.OnCombatStart();
        }

        public void EndCombat()
        {
            ForceResetMedium();
            for (int i = Cards.Count - 1; i >= 0; i--)
                Cards[i].Destroy();
            GlobalCard.EndOfCombat();

            InCombat = false;
            TimeLimit = 0f;

            QueuingSkill = null;
            QueueTime = 0f;

            UIControl.Main.OnCombatEnd();
        }

        public void QueueSkill(Mark_Skill Skill)
        {
            if (!InCombat)
                return;
            QueueTime = 0.1f;
            QueuingSkill = Skill;
        }

        public void QueueUpdate()
        {
            if (QueueTime <= 0 || !QueuingSkill)
                return;
            QueuingSkill.TryUse(Cursor.Main.GetCombatPosition());
            if (!Input.GetMouseButton(0))
                QueueTime -= CombatTime();
            if (QueueTime < 0)
                QueuingSkill = null;
        }

        public int GetRandomizedIndex(int ListCount)
        {
            if (ListCount <= 0)
                return 0;
            float Rate = int.Parse(RandomizeCode.ToString().Substring(CurrentCodeIndex, 2), CultureInfo.InvariantCulture) / 100f;
            return (int)((ListCount - 0.001f) * Rate);
        }

        public void NextRandomizeIndex()
        {
            CurrentCodeIndex += 2;
            while (CurrentCodeIndex >= RandomizeCode.ToString().Length)
                CurrentCodeIndex -= RandomizeCode.ToString().Length;
        }

        //---------------------------------------------------------- Combat Utility ----------------------------------------------------------

        public bool CardInCombat(Card C)
        {
            return Cards.Contains(C) || C == GlobalCard;
        }

        public List<Card> GetCards()
        {
            return Cards;
        }

        public List<Card> GetCards(int Side, int Active)
        {
            List<Card> Temp = new List<Card>();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Active == 1 && !Cards[i].CombatActive())
                    continue;
                if (Active == 0 && Cards[i].CombatActive())
                    continue;
                if (Cards[i].GetSide() != Side)
                    continue;
                Temp.Add(Cards[i]);
            }
            return Temp;
        }

        public void AddMedium(Medium M)
        {
            Mediums.Add(M);
        }

        public void RemoveMedium(Medium M)
        {
            if (Mediums.Contains(M))
                Mediums.Remove(M);
        }

        public void ResetMedium()
        {
            for (int i = Mediums.Count - 1; i >= 0; i--)
                Mediums[i].EndEffect();
        }

        public void ForceResetMedium()
        {
            for (int i = Mediums.Count - 1; i >= 0; i--)
            {
                Destroy(Mediums[i].gameObject);
                Mediums.RemoveAt(i);
            }
        }

        public bool MediumInCombat(Medium M)
        {
            return Mediums.Contains(M);
        }

        public void OnCardCreate(Card C)
        {
            if (!Cards.Contains(C))
                Cards.Add(C);
            if (C.GetSide() == 1)
                EnemyCount++;
        }

        public void OnCardDestroy(Card C)
        {
            if (AllCards.Contains(C))
                AllCards.Remove(C);
            if (Cards.Contains(C))
                Cards.Remove(C);
            if (C.GetSide() == 1)
                EnemyCount--;
        }

        public void OnCardDeath(Card C)
        {
            if (Cards.Contains(C))
                Cards.Remove(C);
            StartCoroutine(DelayCardDestroy(C));
        }

        public IEnumerator DelayCardDestroy(Card C)
        {
            float Delay = 5;
            while (Delay > 0)
            {
                yield return 0;
                Delay -= Time.deltaTime;
                if (!InCombat)
                    yield break;
            }
            C.Destroy();
        }

        public float CombatTime()
        {
            return Time.deltaTime * TimeScale;
        }

        public const float CombatRangeLeft = -40f;
        public const float CombatRangeRight = 40f + 170f;
        public const float CombatRangeDown = -20f;
        public const float CombatRangeUp = 20f;
    }
}