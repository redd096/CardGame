using System.Collections.Generic;
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

        public override void PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            PlayerLogic player = CardGameManager.instance.GetCurrentPlayer();
            player.BlockStopsForTurns = NumberOfTurns;
        }

        public override void AdversaryExecute()
        {
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.Self;
        }
    }
}