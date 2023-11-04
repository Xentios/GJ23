using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ADV
{
    public class UIControl : MonoBehaviour {
        public static UIControl Main;
        public GameObject CardBase;
        [Space]
        public Card SelectingCard;
        public Card LastSelectingCard;
        public float SelectingDelay;
        public float SelectingTime;
        [Space]
        public bool Mute;

        public void Awake()
        {

        }

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }

        public void LateUpdate()
        {
            SelectingUpdate();
        }

        public void SelectingUpdate()
        {
            if (!LastSelectingCard || SelectingCard != LastSelectingCard)
            {
                SelectingTime = 0f;
                LastSelectingCard = SelectingCard;
            }
            else
                SelectingTime += Time.deltaTime;
        }

        public void OnCombatStart()
        {

        }

        public void OnCombatEnd()
        {

        }

        public void NumberEffect(string Key, string text, Card Source)
        {

        }
    }

    public enum UIPanel
    {
        Combat,
        Skill
    }
}