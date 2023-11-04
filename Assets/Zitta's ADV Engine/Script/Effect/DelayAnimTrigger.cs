using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class DelayAnimTrigger : MonoBehaviour {
        public float Delay;
        public bool Triggered;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CombatControl.Main.InCombat)
                Delay -= CombatControl.Main.CombatTime();
            else
                Delay -= Time.deltaTime;
            if (Delay <= 0 && !Triggered)
            {
                Triggered = true;
                GetComponent<Animator>().SetTrigger("Next");
            }
        }
    }
}