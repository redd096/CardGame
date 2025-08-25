using UnityEngine;

namespace cg
{
    [System.Serializable]
    public class LookDeckCards : BaseCardBehaviour
    {
        [Min(1)] public int NumberOfCards = 1;
    }
}