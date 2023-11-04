using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

namespace ADV
{
    public class KeyBase : MonoBehaviour {
        public static KeyBase Main;
        public List<string> Keys;

        public void Ini()
        {
            Keys = new List<string>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddKey(string Key)
        {
            Keys.Add(Key + "[0");
        }

        public void AddKeys(KeyBase KB)
        {
            for (int i = 0; i < KB.Keys.Count; i++)
            {
                ChangeKey(Translate(KB.Keys[i], out float V), V);
            }
        }

        public bool HasKey(string Key)
        {
            foreach (string s in Keys)
            {
                if (Translate(s) == Key)
                    return true;
            }
            return false;
        }

        public float GetKey(string Key)
        {
            float Scale = 1;
            float Add = 0;
            if (Key.IndexOf("{") != -1 && Key.IndexOf("}") != -1)
            {
                string Mod = Key.Substring(Key.IndexOf("{") + 1, 1);
                string ValueText = Key.Substring(Key.IndexOf("{") + 2, Key.LastIndexOf("}") - Key.IndexOf("{") - 2);
                if (!float.TryParse(ValueText, NumberStyles.Float, CultureInfo.InvariantCulture, out float Value))
                    Value = GetKey(ValueText);
                if (Mod == "x")
                    Scale = Value;
                else if (Mod == "+")
                    Add = Value;
                else if (Mod == "-")
                    Add = -Value;
                Key = Key.Substring(Key.LastIndexOf("}") + 1);
            }
            foreach (string s in Keys)
            {
                if (Translate(s, out float V) == Key)
                    return Scale * V + Add;
            }
            if (float.TryParse(Key, NumberStyles.Float, CultureInfo.InvariantCulture, out float V2))
                return Scale * V2 + Add;
            return Add;
        }

        public string GetKey(string Key, bool StringMode)
        {
            foreach (string s in Keys)
            {
                if (Translate(s, out string V) == Key)
                    return V;
            }
            return "";
        }

        public float ChangeKey(string Key, float Value)
        {
            if (!HasKey(Key))
                AddKey(Key);
            float a = GetKey(Key);
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Translate(Keys[i]) == Key)
                    Keys[i] = Key + "[" + (a + Value);
            }
            return GetKey(Key);
        }

        public void SetKey(string Key, float Value)
        {
            ChangeKey(Key, Value - GetKey(Key));
        }

        public void SetKey(string Key, string Value)
        {
            if (!HasKey(Key))
                AddKey(Key);
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Translate(Keys[i]) == Key)
                    Keys[i] = Key + "[" + Value;
            }
        }

        public static string Translate(string OriKey, out float Value)
        {
            if (OriKey == "")
            {
                Value = 0;
                return "";
            }
            float.TryParse(OriKey.Substring(OriKey.IndexOf("[") + 1), NumberStyles.Float, CultureInfo.InvariantCulture, out Value);
            return OriKey.Substring(0, OriKey.IndexOf("["));
        }

        public static string Translate(string OriKey, out string Value)
        {
            if (OriKey == "")
            {
                Value = "";
                return "";
            }
            Value = OriKey.Substring(OriKey.IndexOf("[") + 1);
            return OriKey.Substring(0, OriKey.IndexOf("["));
        }

        public static string Translate(string OriKey)
        {
            return Translate(OriKey, out float V);
        }

        public static string Compose(string Key, float Value)
        {
            return Key + "[" + Value;
        }

        public static string Compose(string Key, string Value)
        {
            return Key + "[" + Value;
        }
    }
}