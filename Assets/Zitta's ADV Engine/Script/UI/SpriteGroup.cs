using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class SpriteGroup : MonoBehaviour {
        public List<SpriteRenderer> Sprites;
        public List<TextMeshPro> Texts;
        public float Alpha;

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
            Render();
        }

        public void Render()
        {
            foreach (SpriteRenderer SR in Sprites)
            {
                if (SR)
                    SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Alpha);
            }
            foreach (TextMeshPro Text in Texts)
            {
                if (Text)
                    Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, Alpha);
            }
        }
    }
}