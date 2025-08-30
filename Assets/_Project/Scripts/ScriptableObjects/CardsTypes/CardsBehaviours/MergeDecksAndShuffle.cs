using System.Collections;

namespace cg
{
    /// <summary>
    /// Put discarded cards (DiscardsDeck) inside the Deck and Shuffle
    /// </summary>
    [System.Serializable]
    public class MergeDeckAndShuffle : BaseCardBehaviour
    {
        public override IEnumerator PlayerExecute(BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override IEnumerator AdversaryExecute(BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }
    }
}