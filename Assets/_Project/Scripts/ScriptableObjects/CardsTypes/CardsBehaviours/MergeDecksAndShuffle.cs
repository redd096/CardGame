using System.Collections;
using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Put discarded cards (DiscardsDeck) inside the Deck and Shuffle
    /// </summary>
    [System.Serializable]
    public class MergeDeckAndShuffle : BaseCardBehaviour
    {
        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return null;
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }
    }
}