using System.Collections;
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

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }

        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            yield return null;
        }
    }
}