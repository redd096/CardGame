using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard how many cards he want
    /// </summary>
    [System.Serializable]
    public class DiscardCardsWithoutLimit : BaseCardBehaviour
    {
        [Space]
        public EDiscardTarget TargetCard;
        
        public override void PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {

        }

        public override void AdversaryExecute()
        {
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }
    }
}