using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class FixedPosition : MonoBehaviour {
        public bool FixedX;
        public bool FixedY;
        public Vector2 Position;

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
            if (FixedX)
                transform.position = new Vector3(Position.x, transform.position.y, transform.position.z);
            if (FixedY)
                transform.position = new Vector3(transform.position.x, Position.y, transform.position.z);
        }
    }
}