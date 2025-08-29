using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Steal X cards to other players
    /// </summary>
    [System.Serializable]
    public class StealPlayerCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public ETargetCard TargetCard;

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