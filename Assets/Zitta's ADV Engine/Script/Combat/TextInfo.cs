using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace ADV
{
    public class TextInfo : MonoBehaviour {
        [HideInInspector] public string Key;
        public string Name;
        public string RenderName;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual string GetName()
        {
            /*if (RenderName == "")
                return GetID();*/
            return RenderName;
        }

        public virtual string GetID()
        {
            return Name;
        }

        public static string ProcessText(string Value, Card C, Mark M)
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
                else if (Key == "Bold")
                    Cici += "<b>";
                else if (Key == "EndBold" || Key == "BoldEnd")
                    Cici += "</b>";
                else if (Key == "Italic")
                    Cici += "<i>";
                else if (Key == "EndItalic" || Key == "ItalicEnd")
                    Cici += "</i>";
                else if (M && Key == "CoolDown" && (!M.HasKey("CoolDown") || M.GetKey("CCD") <= 0))
                    Cici += "[Cool Down: " + M.GetKey("CoolDown") + " turns]";
                else if (M && Key == "CoolDown" && M.GetKey("CCD") > 0)
                    Cici += "[Cool Down: " + M.GetKey("CCD") + "/" + M.GetKey("CoolDown") + " turns]";
                else if (M && Key == "Count")
                    Cici += "[Remaining Usage: " + M.GetKey("Count") + "]";
                else if (M && Key == "Upgrade")
                    Cici += "[Upgrade after " + M.GetKey("Upgrade") + " Usage]";
                else if (M && Key == "Duration" && M.GetKey("Duration") > 1)
                    Cici += "[Duration: " + M.GetKey("Duration") + " turns]";
                else if (M && Key == "Duration")
                    Cici += "[Duration: " + M.GetKey("Duration") + " turn]";
                else if (M && M.HasKey(Key))
                    Cici += ((int)M.GetKey(Key)).ToString();
                else if (C && C.HasKey(Key))
                    Cici += ((int)C.GetKey(Key)).ToString();
                else if (C && Key.Length > 0 && Key.Substring(0, 1) == ":")
                    Cici += ((int)C.GetKey(Key.Substring(1))).ToString();
                else if (Key == "n")
                    Cici += "\n";
                else
                    Cici += Key;
            }
            Cici += S;
            return Cici;
        }
    }
}