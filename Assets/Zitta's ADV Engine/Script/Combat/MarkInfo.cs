using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class MarkInfo : TextInfo {
        [TextArea] public string Description;
        [HideInInspector] public Sprite Icon;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override string GetName()
        {
            return base.GetName();
        }

        public string GetDescription()
        {
            return Description;
        }

        public Sprite GetIcon()
        {
            return Icon;
        }
    }
}