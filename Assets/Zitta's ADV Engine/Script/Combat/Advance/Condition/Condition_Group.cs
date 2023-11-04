using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Group : Condition {
        public List<Condition> Required;
        public List<Condition> Avoided;

        public override bool Pass(Card C)
        {
            foreach (Condition Con in Required)
            {
                if (!Con.Pass(C))
                    return false;
            }
            foreach (Condition Con in Avoided)
            {
                if (Con.Pass(C))
                    return false;
            }
            return true;
        }
    }
}