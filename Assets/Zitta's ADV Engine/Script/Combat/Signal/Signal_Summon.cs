using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Summon : Signal {
        public string Key;

        public override void EndEffect()
        {
            GameObject G = Instantiate(Library.Main.GetCard(Key));
            Card C = G.GetComponent<Card>();
            C.Summoner = Source;
            if (GetKey("LinkSource") == 1)
                C.Source = Source;

            if (HasKey("OverrideLife") && GetKey("OverrideLife") > 0)
            {
                C.SetMaxLife(GetKey("OverrideLife"));
                C.SetLife(GetKey("OverrideLife"));
            }

            G.transform.position = new Vector3(Position.x, Position.y, G.transform.position.z);
            C.SetPosition(Position);
            CombatControl.Main.OnCardCreate(C);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "OverrideLife": Override max life of the summon
            // "LinkSource": Whether to link source to the summon
            base.CommonKeys();
        }
    }
}