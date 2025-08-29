using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCards : BaseCardBehaviour
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