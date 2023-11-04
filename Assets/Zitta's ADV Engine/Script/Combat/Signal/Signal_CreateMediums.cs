using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMediums : Signal {
        public List<GameObject> MediumPrefabs;

        public override void EndEffect()
        {
            foreach (GameObject MediumPrefab in MediumPrefabs)
            {
                GameObject G = Instantiate(MediumPrefab);
                Medium M = G.GetComponent<Medium>();
                if (HasKey("Scale") && GetKey("Scale") != 1)
                {
                    if (M.HasKey("Range"))
                        M.SetKey("Range", M.GetKey("Range") * GetKey("Scale"));
                    if (M.HasKey("MinRange"))
                        M.SetKey("MinRange", M.GetKey("MinRange") * GetKey("Scale"));
                    if (M.HasKey("VerticalPointX"))
                        M.SetKey("VerticalPointX", M.GetKey("VerticalPointX") * GetKey("Scale"));
                    if (M.HasKey("VerticalPointY"))
                        M.SetKey("VerticalPointY", M.GetKey("VerticalPointY") * GetKey("Scale"));
                    if (M.HasKey("HorizontalPointX"))
                        M.SetKey("HorizontalPointX", M.GetKey("HorizontalPointX") * GetKey("Scale"));
                    if (M.HasKey("HorizontalPointY"))
                        M.SetKey("HorizontalPointY", M.GetKey("HorizontalPointY") * GetKey("Scale"));
                }
                if (HasKey("DelayScale") && GetKey("DelayScale") != 1)
                {
                    if (M.HasKey("Delay"))
                        M.SetKey("Delay", M.GetKey("Delay") * GetKey("DelayScale"));
                }
                M.Ini(Source, Target, Position);
            }
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Scale": Scale of Medium_Explosion
            // "DelayScale": Delay scale of Mediums
            base.CommonKeys();
        }
    }
}