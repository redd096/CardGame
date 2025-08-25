using UnityEngine;

namespace cg
{
    [System.Serializable]
    public class BlockStopsForTurns : BaseCardBehaviour
    {
        [Min(1)] public int NumberOfTurns = 1;
    }
}