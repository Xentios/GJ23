using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ADV
{
    public class HealthBar : MonoBehaviour {
        public Card Source;
        public Image Bar;
        public Animator Anim;
        public float LastHealth = -1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Source)
                return;
            if (Source.GetLife() <= 0 || UIControl.Main.End)
            {
                Destroy(gameObject);
                return;
            }
            if (LastHealth == -1)
                LastHealth = Source.GetLife();
            Bar.fillAmount = Source.GetLife() / Source.GetMaxLife();
            transform.position = Camera.main.WorldToScreenPoint(Source.GetAnim().GetPivotPosition());
            if (Source.GetLife() < LastHealth && LastHealth - Source.GetLife() >= 2)
                Anim.SetTrigger("Effect");
            LastHealth = Source.GetLife();
        }
    }
}