using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Invoke_Random : Signal {
        public List<float> Channels;

        public override void StartEffect()
        {
            SetKey("Channel", Channels[Random.Range(0, Channels.Count)]);
            base.StartEffect();
        }

        public override void EndEffect()
        {
            Target.InvokeSkill(GetKey("Channel"), Position);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Channel": Channel to invoke
            base.CommonKeys();
        }
    }
}