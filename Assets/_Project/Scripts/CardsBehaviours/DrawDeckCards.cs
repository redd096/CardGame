using UnityEngine;

namespace cg
{
    [System.Serializable]
    public class DrawDeckCards : BaseCardBehaviour
    {
        [Min(1)] public int NumberOfCards = 1;
    }
}