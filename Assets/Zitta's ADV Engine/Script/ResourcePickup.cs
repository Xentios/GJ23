using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class ResourcePickup : MonoBehaviour {
        public string ResourceType;
        [Space]
        public Animator Anim;
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public float Position;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(StartPosition.x + (EndPosition.x - StartPosition.x) * Position,
                StartPosition.y + (EndPosition.y - StartPosition.y) * Position, 0);
        }

        public void Ini()
        {
            EndPosition = StartPosition + new Vector2(Random.Range(1f, 8f), Random.Range(-8f, -2.5f));
        }
    }
}