using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Put discarded cards (DiscardsDeck) inside the Deck and Shuffle
    /// </summary>
    [System.Serializable]
    public class MergeDeckAndShuffle : BaseCardBehaviour
    {
        public override void PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
        }

        public override void AdversaryExecute()
        {
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }
    }
}