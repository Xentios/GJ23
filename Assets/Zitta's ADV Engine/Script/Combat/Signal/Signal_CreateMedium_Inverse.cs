using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium_Inverse : Signal {
        // Inverse source and target when creating medium
        public GameObject MediumPrefab;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            M.Ini(Target, Source, Position);
            base.EndEffect();
        }
    }
}