using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CardInfo : TextInfo {
        [HideInInspector] public string GenerationKey;
        [HideInInspector] [TextArea] public string Description;
        [HideInInspector] public string AddDescription;
        [HideInInspector] [TextArea] public string Trait;
        [HideInInspector] public List<Sprite> AddIcons;
        [HideInInspector] public List<string> AddCards;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string GetGenerationKey()
        {
            if (GenerationKey == "")
                return GetID();
            return GenerationKey;
        }

        public string GetDescription()
        {
            string s = Description;
            if (AddDescription != "")
            {
                if (s != "")
                    s += "\n" + AddDescription;
                else
                    s += AddDescription;
            }
            return s;
        }

        public List<Sprite> GetAddIcons()
        {
            return AddIcons;
        }
    }
}