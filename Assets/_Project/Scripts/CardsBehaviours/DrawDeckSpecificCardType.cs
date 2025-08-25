using UnityEngine;

namespace cg
{
    [System.Serializable]
    public class DrawDeckSpecificCardType : BaseCardBehaviour
    {
        [Min(1)] public int NumberOfCards = 1;
        public ECardType TypeCardsToDraw;
    }
}