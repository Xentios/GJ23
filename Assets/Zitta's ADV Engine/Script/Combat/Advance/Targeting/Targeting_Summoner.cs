using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Summoner : Targeting {

        public override Card FindTarget(Card Source, Vector2 Position)
        {
            return Source.Summoner;
        }
    }
}