using UnityEngine;

namespace cg
{
    [System.Serializable]
    public class DrawDeckSpecificCard : BaseCardBehaviour
    {
        [Min(1)] public int NumberOfCards = 1;
    }
}