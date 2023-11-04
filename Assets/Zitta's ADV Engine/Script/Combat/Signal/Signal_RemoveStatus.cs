using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_RemoveStatus : Signal {
        public List<string> StatusKeys;
        public List<string> TriggerKeys;
        [HideInInspector] public List<Mark_Status> RemovingStatus;

        public override void InputMark(Mark M)
        {
            if (TriggerKeys.Count > 0)
            {
                bool Trigger = true;
                foreach (string s in TriggerKeys)
                    if (!M.HasKey(s))
                        Trigger = false;
                if (Trigger)
                    RemovingStatus.Add(M.GetComponent<Mark_Status>());
            }

            if (StatusKeys.Contains(M.GetID()))
                RemovingStatus.Add(M.GetComponent<Mark_Status>());
            base.InputMark(M);
        }

        public override void EndEffect()
        {
            if (!HasKey("RemoveCount"))
                SetKey("RemoveCount", 999);
            for (int i = RemovingStatus.Count - 1; i >= 0 && GetKey("RemoveCount") > 0; i--)
            {
                ChangeKey("RemoveCount", -1);
                RemovingStatus[i].Source.RemoveStatus(RemovingStatus[i]);
            }
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "RemoveCount": Maximum status to remove
            base.CommonKeys();
        }
    }
}