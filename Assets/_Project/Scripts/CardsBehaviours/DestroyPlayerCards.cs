using UnityEngine;

namespace cg
{
    [System.Serializable]
    public class DestroyPlayerCards : BaseCardBehaviour
    {
        [Min(1)] public int NumberOfCards = 1;
        public ETargetCard TargetCard;
    }
}