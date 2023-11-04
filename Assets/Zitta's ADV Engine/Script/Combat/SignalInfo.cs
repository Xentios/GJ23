using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class SignalInfo : TextInfo {
        public bool SubRender;
        [TextArea] public string Message;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string GetMessage()
        {
            return Message;
        }
    }
}