using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace ADV
{
    public class StaticAssign : MonoBehaviour {
        public static StaticAssign Main;
        public CombatControl CC;
        public UIControl UIC;
        public Cursor MainCursor;
        [Space]
        public GameObject EffectLine;
        public string SceneName;

        public void Awake()
        {
            Main = this;

            if (!CombatControl.Main)
                CombatControl.Main = CC;
            else if (CC)
                Destroy(CC.gameObject);

            if (!UIControl.Main)
                UIControl.Main = UIC;
            else if (UIC)
                Destroy(UIC.gameObject);

            if (!Cursor.Main)
                Cursor.Main = MainCursor;
            else if (MainCursor)
                Destroy(MainCursor.gameObject);
        }

        public static StaticAssign GetMain()
        {
            return Camera.main.GetComponent<StaticAssign>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Retry()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneName);
        }

        public static float RoundUp(float Ori)
        {
            if (Ori % 1f == 0)
                return Ori;
            return (int)Ori + 1f;
        }

        public static string ParseTime(float Value)
        {
            int FullNumber = (int)Value;
            int SmallNumber = (int)((Value * 10f) % 10f);
            return FullNumber + "." + SmallNumber;
        }

        public static string CoinSign(float Value)
        {
            /*if (Value >= 0)
                return "<#D35FB6>" + Value + " Faith</color>";
            else
                return "<#D35FB6>+" + -Value + " Faith</color>";*/
            if (Value >= 0)
                return "-" + Value + " Faith";
            else
                return "+" + -Value + " Faith";
        }

        public static string GetLockKey()
        {
            return "General_Status_Lock";
        }

        public static string GetColorCode(string Key)
        {
            if (Key == "DarkGrey")
                return "<#141414>";
            if (Key == "Grey")
                return "<#595959>";
            else if (Key == "White")
                return "<#D9D9D9>";
            else if (Key == "Red")
                return "<#D94B36>";
            else if (Key == "Orange")
                return "<#D98236>";
            else if (Key == "Yellow")
                return "<#D9B836>";
            else if (Key == "Blue")
                return "<#36BED9>";
            else if (Key == "EC" || Key == "CE")
                return "</color>";
            else
                return "";
        }

        public static string GetBoldColorCode(string Key)
        {
            if (Key == "EC" || Key == "CE")
                return GetColorCode(Key) + "</b>";
            else
                return "<b>" + GetColorCode(Key);
        }

        public static Color GetRealColor(string Key)
        {
            ColorUtility.TryParseHtmlString(GetColorCode(Key).Substring(1, 7), out Color Co);
            return Co;
        }

        public static float LineDistance(Vector2 Point, Vector2 LineStart, Vector2 LineEnd)
        {
            float x1 = LineStart.x;
            float y1 = LineStart.y;
            float x2 = LineEnd.x;
            float y2 = LineEnd.y;
            float x0 = Point.x;
            float y0 = Point.y;
            return Mathf.Abs((x2 - x1) * (y1 - y0) - (x1 - x0) * (y2 - y1)) / Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public static string NumberToHex(int Decimal)
        {
            int Scale = Decimal / 34;
            int Remainder = Decimal % 34;
            string Hax = "";
            while (Scale > 0)
            {
                Hax = SingleNumberToHex(Remainder) + Hax;
                Remainder = Scale % 34;
                Scale = Scale / 34;
            }
            Hax = SingleNumberToHex(Remainder) + Hax;
            return Hax;
        }

        public static int HexToNumber(string Hex)
        {
            if (Hex.Length <= 0)
                return 0;
            string hex = Hex;
            string Single;
            int Number = 0;
            int Power = 0;
            while (hex.Length > 0)
            {
                Single = hex.Substring(hex.Length - 1, 1);
                int Add = SingleHexToNumber(Single) * (int)Mathf.Pow(34, Power);
                //print("Addition: " + Add);
                Number += Add;
                hex = hex.Substring(0, hex.Length - 1);
                Power++;
            }
            return Number;
        }

        public static string SingleNumberToHex(int Decimal)
        {
            if (Decimal == 0)
                return "0";
            if (Decimal == 1)
                return "1";
            if (Decimal == 2)
                return "2";
            if (Decimal == 3)
                return "3";
            if (Decimal == 4)
                return "4";
            if (Decimal == 5)
                return "5";
            if (Decimal == 6)
                return "6";
            if (Decimal == 7)
                return "7";
            if (Decimal == 8)
                return "8";
            if (Decimal == 9)
                return "9";
            if (Decimal == 10)
                return "A";
            if (Decimal == 11)
                return "B";
            if (Decimal == 12)
                return "C";
            if (Decimal == 13)
                return "D";
            if (Decimal == 14)
                return "E";
            if (Decimal == 15)
                return "F";
            if (Decimal == 16)
                return "G";
            if (Decimal == 17)
                return "H";
            if (Decimal == 18)
                return "J";
            if (Decimal == 19)
                return "K";
            if (Decimal == 20)
                return "L";
            if (Decimal == 21)
                return "M";
            if (Decimal == 22)
                return "N";
            if (Decimal == 23)
                return "P";
            if (Decimal == 24)
                return "Q";
            if (Decimal == 25)
                return "R";
            if (Decimal == 26)
                return "S";
            if (Decimal == 27)
                return "T";
            if (Decimal == 28)
                return "U";
            if (Decimal == 29)
                return "V";
            if (Decimal == 30)
                return "W";
            if (Decimal == 31)
                return "X";
            if (Decimal == 32)
                return "Y";
            if (Decimal == 33)
                return "Z";
            return "";
        }

        public static int SingleHexToNumber(string Hax)
        {
            if (Hax == "0")
                return 0;
            if (Hax == "1")
                return 1;
            if (Hax == "2")
                return 2;
            if (Hax == "3")
                return 3;
            if (Hax == "4")
                return 4;
            if (Hax == "5")
                return 5;
            if (Hax == "6")
                return 6;
            if (Hax == "7")
                return 7;
            if (Hax == "8")
                return 8;
            if (Hax == "9")
                return 9;
            if (Hax == "A")
                return 10;
            if (Hax == "B")
                return 11;
            if (Hax == "C")
                return 12;
            if (Hax == "D")
                return 13;
            if (Hax == "E")
                return 14;
            if (Hax == "F")
                return 15;
            if (Hax == "G")
                return 16;
            if (Hax == "H")
                return 17;
            if (Hax == "J")
                return 18;
            if (Hax == "K")
                return 19;
            if (Hax == "L")
                return 20;
            if (Hax == "M")
                return 21;
            if (Hax == "N")
                return 22;
            if (Hax == "P")
                return 23;
            if (Hax == "Q")
                return 24;
            if (Hax == "R")
                return 25;
            if (Hax == "S")
                return 26;
            if (Hax == "T")
                return 27;
            if (Hax == "U")
                return 28;
            if (Hax == "V")
                return 29;
            if (Hax == "W")
                return 30;
            if (Hax == "X")
                return 31;
            if (Hax == "Y")
                return 32;
            if (Hax == "Z")
                return 33;
            return 0;
        }

        public static void Color()
        {
            // [Grey]: #B2B2B2
            // [DarkGrey]: #4C4C4C
            // [Pink]: #D35FB6
            // [Yellow]: #EBC452
            // [Yellow_Alter]: #D9A40B
            // [Green]: #57D95A
            // [Green_Alter]: #26BF29
            // [Red]: #EC4C3E
            // [DarkPurple]: #7756C0
            // [Purple]: #9068E8
            // [DarkBlue]: #3078BF
            // [Blue]: #4E9BE6
        }
    }
}