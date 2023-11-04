using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class FollowEffect : MonoBehaviour {
        public Card Target;
        public string RequiredMark;
        public bool Destroyed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Animator>())
            {
                GetComponent<Animator>().SetBool("TargetLost", !Target || !Target.CombatActive());
                GetComponent<Animator>().SetBool("MarkLost", RequiredMark != null && RequiredMark != "" && Target && !Target.HasMark(RequiredMark));
            }

            if (Target && Target.CombatActive())
                transform.position = new Vector3(Target.GetPosition().x, Target.GetPosition().y, transform.position.z);
            else if (!Destroyed)
            {
                Destroyed = true;
                Destroy(gameObject, 5);
            }
        }
    }
}