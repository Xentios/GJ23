using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class CardAnim : MonoBehaviour {
        public Card Source;
        public Animator Animator;
        public GameObject AnimBase;
        public GameObject AnimPivot;
        public TextMeshPro LifeText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            LifeUpdate();
        }

        public void LifeUpdate()
        {
            if (LifeText)
                LifeText.text = StaticAssign.RoundUp(Source.GetLife()).ToString();
        }

        public Animator GetAnimator()
        {
            return Animator;
        }

        public void OnDeath()
        {
            Animator.SetTrigger("Death");
        }

        public Vector3 GetPivotPosition()
        {
            if (!AnimPivot)
                return transform.position;
            return AnimPivot.transform.position;
        }
    }
}