using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Cursor : MonoBehaviour {
        public static Cursor Main;
        public Vector2 Position;
        [HideInInspector] public float SelectionDelay;
        public float CursorLock;
        public bool AutoClick;

        public void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            /*PositionUpdate();
            SelectionUpdate();
            if (CursorLock > 0)
            {
                CursorLock -= Time.deltaTime;
                return;
            }
            if (Input.GetMouseButton(0) || AutoClick)
                HoldClick();
            if (Input.GetMouseButtonDown(0))
            {
                Interact();
                AutoClick = false;
            }
            if (Input.GetMouseButtonUp(0))
                UnInteract();
            if (Input.GetMouseButtonDown(1))
                RightClick();
            if (Input.GetMouseButtonUp(1))
                RightUp();
            if (Input.GetKeyDown(KeyCode.Q))
                KeyInput_Q();
            if (Input.GetKeyDown(KeyCode.W))
                KeyInput_W();
            if (Input.GetKeyDown(KeyCode.E))
                KeyInput_E();
            if (Input.GetKeyDown(KeyCode.R))
                KeyInput_R();
            if (Input.GetKeyDown(KeyCode.D))
                KeyInput_D();
            if (Input.GetKeyDown(KeyCode.Space) && CombatControl.Main.InCombat)
                AutoClick = !AutoClick;*/
        }

        public void RightClick()
        {
            if (CombatControl.Main.InCombat && CombatControl.Main.InputProtection <= 0)
            {
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "RightClick")
                    {
                        CombatControl.Main.QueueSkill(Skill);
                        break;
                    }
                }
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "AddRightClick")
                        Skill.TryUse(GetCombatPosition());
                }
            }
        }

        public void RightUp()
        {

        }

        public void Interact()
        {

        }

        public void UnInteract()
        {

        }

        public void HoldClick()
        {
            if (CombatControl.Main.InCombat && CombatControl.Main.InputProtection <= 0)
            {
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "LeftClick")
                    {
                        CombatControl.Main.QueueSkill(Skill);
                        break;
                    }
                }
            }
        }

        public void KeyInput_Q()
        {
            if (CombatControl.Main.InCombat && CombatControl.Main.InputProtection <= 0)
            {
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "Q")
                    {
                        CombatControl.Main.QueueSkill(Skill);
                        break;
                    }
                }
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "AddQ")
                        Skill.TryUse(GetCombatPosition());
                }
            }
        }

        public void KeyInput_W()
        {
            if (CombatControl.Main.InCombat && CombatControl.Main.InputProtection <= 0)
            {
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "W")
                    {
                        CombatControl.Main.QueueSkill(Skill);
                        break;
                    }
                }
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "AddW")
                        Skill.TryUse(GetCombatPosition());
                }
            }
        }

        public void KeyInput_E()
        {
            if (CombatControl.Main.InCombat && CombatControl.Main.InputProtection <= 0)
            {
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "E")
                    {
                        CombatControl.Main.QueueSkill(Skill);
                        break;
                    }
                }
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "AddE")
                        Skill.TryUse(GetCombatPosition());
                }
            }
        }

        public void KeyInput_R()
        {
            if (CombatControl.Main.InCombat && CombatControl.Main.InputProtection <= 0)
            {
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "R")
                    {
                        CombatControl.Main.QueueSkill(Skill);
                        break;
                    }
                }
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "AddR")
                        Skill.TryUse(GetCombatPosition());
                }
            }
        }

        public void KeyInput_D()
        {
            if (CombatControl.Main.InCombat && CombatControl.Main.InputProtection <= 0)
            {
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "D")
                    {
                        CombatControl.Main.QueueSkill(Skill);
                        break;
                    }
                }
                foreach (Mark_Skill Skill in CombatControl.Main.GlobalCard.Skills)
                {
                    if (Skill.GetKey("Control", true) == "AddD")
                        Skill.TryUse(GetCombatPosition());
                }
            }
        }

        public void PositionUpdate()
        {
            Vector3 a = new Vector3(); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position = new Vector2(a.x, a.y);
            transform.position = new Vector3(a.x, a.y, transform.position.z);
        }

        public void SelectionUpdate()
        {
            if (!UIControl.Main)
                return;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(Position.x, Position.y);
        }

        public Vector2 GetCombatPosition()
        {
            if (!CombatControl.Main)
                return GetPosition();
            Vector2 Position = GetPosition();
            if (Position.x > CombatControl.CombatRangeRight)
                Position.x = CombatControl.CombatRangeRight;
            if (Position.x < CombatControl.CombatRangeLeft)
                Position.x = CombatControl.CombatRangeLeft;
            if (Position.y > CombatControl.CombatRangeUp)
                Position.y = CombatControl.CombatRangeUp;
            if (Position.y < CombatControl.CombatRangeDown)
                Position.y = CombatControl.CombatRangeDown;

            return Position;
        }
    }
}