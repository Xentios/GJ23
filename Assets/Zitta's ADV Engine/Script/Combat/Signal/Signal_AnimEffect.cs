using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AnimEffect : Signal {
        public GameObject Prefab;

        public override void EndEffect()
        {
            GameObject G = Instantiate(Prefab);
            G.transform.position = new Vector3(Position.x, Position.y, transform.position.z);
            if (GetKey("RandomRotation") == 1)
                G.transform.eulerAngles = new Vector3(0, 0, Random.Range(0.1f, 359.9f));
            if (HasKey("Rotation"))
                G.transform.eulerAngles = new Vector3(0, 0, GetKey("Rotation"));
            if (GetKey("Follow") == 1 && Target)
            {
                if (!G.GetComponent<FollowEffect>())
                    G.AddComponent<FollowEffect>().Target = Target;
                else
                    G.GetComponent<FollowEffect>().Target = Target;
                if (HasKey("RequiredMark"))
                    G.GetComponent<FollowEffect>().RequiredMark = GetKey("RequiredMark", true);
            }
            if (GetKey("DelayTrigger") > 0)
            {
                G.AddComponent<DelayAnimTrigger>().Delay = GetKey("DelayTrigger");
            }
            if (HasKey("Scale"))
                G.transform.localScale = new Vector3(G.transform.localScale.x * GetKey("Scale"), G.transform.localScale.y * GetKey("Scale"), 1);
            if (GetKey("Follow") == 0 || !Target)
                Destroy(G, 5 + GetKey("DelayTrigger") + GetKey("AddDelay"));
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "RandomRotation": Whether to randomize effect rotation
            // "Rotation": Rotation of the effect
            // "Scale": Scale of the effect
            // "Follow": Whether the effect should follow the target
            // "RequiredMark": Set the required mark for follow effect
            // "DelayTrigger": Set up a delay animator trigger for the effect
            // "AddDelay": Additional delay time
            base.CommonKeys();
        }
    }
}