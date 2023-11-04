using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class SpriteLifeBar : MonoBehaviour {
        public Card Source;
        public SpriteRenderer SR;
        public List<Sprite> Sprites;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LateUpdate()
        {
            float Scale = 1 - Source.GetLife() / Source.GetMaxLife();
            if (Scale > 1)
                Scale = 1;
            int Index = (int)(Scale * (Sprites.Count - 1));
            if (Scale < 1 && Scale > 0)
                Index++;
            if (Index < 0)
                Index = 0;
            SR.sprite = Sprites[Index];
        }
    }
}