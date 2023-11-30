using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ADV
{
    public class UIControl : MonoBehaviour {
        [SerializeField]
        private FloatVariable score;
        [SerializeField]
        private FloatVariable Highscore;

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
        public int EndResult;
        public float EndTime;
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
            if (End)
                EndTime += Time.deltaTime;
            if (EndTime >= 1)
            {
                InputUpdate();
            }
        }

        public void InputUpdate()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (score.Value > Highscore.Value)
                {
                    Highscore.Value = score.Value;
                }

                if (EndResult == -1)
                {
                    score.Value = 0;
                    SceneManager.LoadScene(0);
                }
                else if (EndResult == 1)
                {
                    SceneManager.LoadScene(3);//Victory
                }
                EndTime = -99f;
            }
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
            EndResult = Result;
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