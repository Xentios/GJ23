using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark : MonoBehaviour {
        public KeyBase KB;
        [HideInInspector] public MarkInfo Info;
        [HideInInspector] public Card Source;

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        // Called when assigned to source
        public virtual void OnAssign()
        {

        }

        // Modify Signals sent
        public virtual void OutputSignal(Signal S)
        {

        }

        // Modify Signals recieved
        public virtual void InputSignal(Signal S)
        {

        }

        // After the signal's effect
        public virtual void ReturnSignal(Signal S)
        {

        }

        // After the signal's effect
        public virtual void ConfirmSignal(Signal S)
        {

        }

        // Passive stat modifier
        public virtual float PassValue(string Key, float Value)
        {
            return Value;
        }

        public virtual void TimePassed(float Value)
        {

        }

        public virtual void StartOfCombat()
        {

        }

        public virtual void EndOfCombat()
        {

        }

        public virtual void Death()
        {

        }

        public virtual void SendSignal(GameObject Prefab)
        {
            SendSignal(Prefab, new List<string>());
        }

        public virtual void SendSignal(GameObject Prefab, List<string> LS)
        {

        }

        public virtual bool Active()
        {
            return true;
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

        public MarkInfo GetInfo()
        {
            if (!Info)
                Info = GetComponent<MarkInfo>();
            return Info;
        }

        public virtual string GetName()
        {
            if (!GetInfo())
                return "";
            return GetInfo().GetName();
        }

        public virtual string GetID()
        {
            if (!GetInfo())
                return "";
            return GetInfo().GetID();
        }

        public virtual string GetDescription()
        {
            if (!GetInfo())
                return "";
            return TextInfo.ProcessText(GetInfo().GetDescription(), Source, this);
        }

        public virtual Sprite GetIcon()
        {
            if (!GetInfo())
                return null;
            return GetInfo().GetIcon();
        }

        /*public string ProcessDescription(string Value)
        {
            string Cici = "";
            string S = Value;
            while (S.IndexOf("*") != -1)
            {
                Cici += S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (S.IndexOf("*") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (Key == "CoolDown" && (!HasKey("CoolDown") || GetKey("CCD") <= 0))
                    Cici += "[Cool Down: " + GetKey("CoolDown") + " turns]";
                else if (Key == "CoolDown" && GetKey("CCD") > 0)
                    Cici += "[Cool Down: " + GetKey("CCD") + "/" + GetKey("CoolDown") + " turns]";
                else if (Key == "Count")
                    Cici += "[Remaining Usage: " + GetKey("Count") + "]";
                else if (Key == "Upgrade")
                    Cici += "[Upgrade after " + GetKey("Upgrade") + " Usage]";
                else if (Key == "Duration" && GetKey("Duration") > 1)
                    Cici += "[Duration: " + GetKey("Duration") + " turns]";
                else if (Key == "Duration")
                    Cici += "[Duration: " + GetKey("Duration") + " turn]";
                else if (Key == "Grey" || Key == "DarkGrey"
                    || Key == "Pink" || Key == "Yellow" || Key == "Green" || Key == "Red" || Key == "Purple" || Key == "Blue" || Key == "BlueGreen"
                    || Key == "CE" || Key == "EC")
                    Cici += StaticAssign.GetColorCode(Key);
                else if (Source && Key.Length >= 11 && Key.Substring(0, 11) == "StatScaling")
                {
                    KeyBase.Translate(Key, out float a);
                    Cici += a * Source.GetGlobalStatMod();
                }
                else if (Source && Key == "StatMod")
                {
                    float Stat = (int)((CombatControl.Main.GetFriendlyStatMod() - 1) * 100f);
                    Cici += "+" + Stat + "%";
                }
                else if (HasKey(Key))
                    Cici += ((int)GetKey(Key)).ToString();
                else if (Source && Source.HasKey(Key))
                    Cici += ((int)Source.GetKey(Key)).ToString();
                else if (Source && Key.Length > 0 && Key.Substring(0, 1) == "`")
                    Cici += ((int)Source.GetKey(Key.Substring(1))).ToString();
                else
                    Cici += GetKey(Key);
            }
            Cici += S;
            return Cici;
        }

        public static string ProcessDescription_Static(string Value, Card Source)
        {
            string Cici = "";
            string S = Value;
            while (S.IndexOf("*") != -1)
            {
                Cici += S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (S.IndexOf("*") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (Key == "Grey" || Key == "DarkGrey"
                    || Key == "Pink" || Key == "Yellow" || Key == "Green" || Key == "Red" || Key == "Purple" || Key == "Blue" || Key == "BlueGreen"
                    || Key == "CE" || Key == "EC")
                    Cici += StaticAssign.GetColorCode(Key);
                else
                    Cici += Key;
            }
            Cici += S;
            return Cici;
        }*/

        public virtual void CommonKeys()
        {

        }
    }
}