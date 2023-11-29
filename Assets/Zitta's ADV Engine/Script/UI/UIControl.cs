using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

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
        public Animator EndRenderer;
        public TextMeshPro ResultText;
        public TextMeshPro PointText;
        public bool End;
        public float EndTime;
        [Space]
        public bool Mute;


        private bool reallyCallOnce;
        public void Update()
        {
            if (End)
                EndTime += Time.deltaTime;
            if (EndTime >= 1)
            {
                InputSystem.onAnyButtonPress.CallOnce(control => InputUpdate(control));

            }
        }

        private void InputUpdate(InputControl control)
        {
            if (reallyCallOnce == true) return;
            reallyCallOnce = true;

            SceneManager.LoadScene(0);
            EndTime = -99f;
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

        public void OnCombatEnd(int Result)
        {
            End = true;
            EndRenderer.SetTrigger("Effect");
            if (Result == -1)
                ResultText.text = "Defeat!";
            else if (Result == 1)
                ResultText.text = "Victory!";
            PointText.text = CombatControl.Main.GlobalCard.GetKey("Point").ToString();
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