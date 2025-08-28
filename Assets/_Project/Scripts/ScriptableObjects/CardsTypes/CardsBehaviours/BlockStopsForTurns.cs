using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players can't stop this user for X turns
    /// </summary>
    [System.Serializable]
    public class BlockStopsForTurns : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfTurns = 1;
    }
}