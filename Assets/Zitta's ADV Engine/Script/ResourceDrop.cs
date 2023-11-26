using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class ResourceDrop : MonoBehaviour {
        [HideInInspector] public Card Source;
        public float DropPoint;
        public Vector2 DropCount;
        public List<GameObject> Drops;
        [HideInInspector] public bool AlreadyTriggered;

    
        // Update is called once per frame
        void Update()
        {
            if (!Source)
                Source = GetComponent<Card>();
            if (!AlreadyTriggered && Source && Source.GetLife() <= DropPoint)
            {
                AlreadyTriggered = true;
                int Count = Mathf.RoundToInt(Random.Range(DropCount.x, DropCount.y));
                for (int i = 0; i < Count; i++)
                {
                    GameObject G = Instantiate(Drops[Random.Range(0, Drops.Count)]);
                    G.transform.position = Source.gameObject.transform.position;
                    ResourcePickup RP = G.GetComponent<ResourcePickup>();
                    RP.StartPosition = Source.GetPosition();
                    RP.Ini();
                }
            }
        }
    }
}