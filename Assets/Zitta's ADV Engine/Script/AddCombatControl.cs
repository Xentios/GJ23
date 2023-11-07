using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AddCombatControl : MonoBehaviour {
        public Animator CameraMovement;
        public List<Card> FriendlyCards;
        public List<Card> Bosses;
        public int CurrentIndex;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Bosses.Count > CurrentIndex)
            {
                if ((!Bosses[CurrentIndex] || !Bosses[CurrentIndex].CombatActive()) && GetMaxPosition() >= 85f * CurrentIndex + 40f)
                {
                    CurrentIndex++;
                    CameraMovement.SetInteger("State", CurrentIndex);
                }
            }
        }

        public float GetMaxPosition()
        {
            float MaxPosition = CombatControl.CombatRangeLeft;
            foreach (Card C in FriendlyCards)
            {
                if (!C)
                    continue;
                if (C.GetPosition().x > MaxPosition)
                    MaxPosition = C.GetPosition().x;
            }
            return MaxPosition;
        }
    }
}