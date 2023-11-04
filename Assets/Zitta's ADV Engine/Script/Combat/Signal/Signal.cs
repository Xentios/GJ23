using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal : MonoBehaviour {
        public KeyBase KB;
        [HideInInspector] public SignalInfo Info;
        [HideInInspector] public Card Source;
        [HideInInspector] public Card Target;
        [HideInInspector] public Vector2 Position;

        public void Awake()
        {
            Info = null;
        }

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual void Ini()
        {
            if (GetKey("RandomPosition") == 1)
            {
                if (HasKey("RandomRange"))
                    Position += new Vector2(Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f)).normalized * Random.Range(0, GetKey("RandomRange"));
                else
                    Position = new Vector2(Random.Range(CombatControl.CombatRangeLeft, CombatControl.CombatRangeRight),
                        Random.Range(CombatControl.CombatRangeDown, CombatControl.CombatRangeUp));
            }
            if (GetKey("TargetSnap") == 1 && Target)
                Position = Target.GetPosition();
            if (GetKey("CursorPosition") == 1)
                Position = Cursor.Main.GetCombatPosition();
            if (HasKey("AddPositionX"))
                Position += new Vector2(GetKey("AddPositionX"), 0);
            if (HasKey("AddPositionY"))
                Position += new Vector2(0, GetKey("AddPositionY"));
        }

        // Modify Marks
        public virtual void InputMark(Mark M)
        {

        }

        public virtual void StartEffect()
        {

        }

        public virtual void EndEffect()
        {

        }

        // For PassSignal()
        public virtual string ReturnKey(out float Value)
        {
            Value = 0;
            return "Empty";
        }

        // Process AddKeys
        public virtual void IniAddKeys(List<string> Keys)
        {
            foreach (string s in Keys)
                SetKey(KeyBase.Translate(s, out float v), v);
        }

        public void OutputMessage()
        {
            /*if (GetMessage(out string M, out bool SR))
                CombatControl.Main.RecieveMessage(M, SR);*/
        }

        public KeyBase GKB()
        {
            return KB;
        }

        public void AddKey(string Key)
        {
            GKB().AddKey(Key);
        }

        public void AddKeys(KeyBase KB)
        {
            GKB().AddKeys(KB);
        }

        public bool HasKey(string Key)
        {
            return GKB().HasKey(Key);
        }

        public float GetKey(string Key)
        {
            return GKB().GetKey(Key);
        }

        public string GetKey(string Key, bool StringMode)
        {
            return GKB().GetKey(Key, StringMode);
        }

        public float ChangeKey(string Key, float Value)
        {
            return GKB().ChangeKey(Key, Value);
        }

        public void SetKey(string Key, float Value)
        {
            GKB().SetKey(Key, Value);
        }

        public void SetKey(string Key, string Value)
        {
            GKB().SetKey(Key, Value);
        }

        public SignalInfo GetInfo()
        {
            if (!Info)
                Info = GetComponent<SignalInfo>();
            return Info;
        }

        public bool GetMessage(out string Message, out bool SubRender)
        {
            if (!GetInfo() || GetInfo().GetMessage() == "")
            {
                Message = "";
                SubRender = false;
                return false;
            }

            Message = ProcessMessage(GetInfo().GetMessage());
            SubRender = GetInfo().SubRender;
            return true;
        }

        public string ProcessMessage(string Value)
        {
            string Cici = "";
            string S = Value;
            while (S.IndexOf("*") != -1/* && S.IndexOf("*") != S.Length - 1*/)
            {
                Cici += S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (S.IndexOf("*") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (Key == "Source")
                    Cici += "[" + Source.GetName() + "]";
                else if (Key == "Target")
                    Cici += "[" + Target.GetName() + "]";
                else if (Key == "Its")
                    Cici += "Its";
                else if (Key == "its")
                    Cici += "its";
                else if (Key == "Itself")
                    Cici += "Itself";
                else if (Key == "itself")
                    Cici += "itself";
                else if (Key == "(it")
                    Cici += "it";
                else if (Key == "(It")
                    Cici += "It";
                else if (Key == "it")
                    Cici += "it";
                else if (Key == "It")
                    Cici += "It";
                else
                    Cici += GetKey(Key);
            }
            Cici += S;
            return Cici;
        }

        public virtual void CommonKeys()
        {
            // "Target": Whether the signal should be send to the opponent
            // "TargetSnap": Whether the signal should snap to the target's position
            // "RandomPosition": Whether to randomize position
            // "CursorPosition": Whether to set position to cursor's position
            // "SourcePosition": Which position the signal is targeting related to the source position
            // "AllPosition": Whether the signal is targeting all positions
            // "ReverseSource": Whether the signal should be send from the opponent
            // "IgnoreSource": Whether OutputSignal should be ignored
            // "ItemCount": The count of the source skill (item)
            // "Delay": The delay before the signal is resolved
            // "RealSource": If the signal should always be send by the real source
            // "RequireTarget": Whether to only send signal if it has a target
            // "AddPositionX": Additional signal position
            // "AddPositionY": Additional signal position
        }
    }
}