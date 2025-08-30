using System.Collections;

namespace cg
{
    /// <summary>
    /// Select one player and swap cards with him
    /// </summary>
    [System.Serializable]
    public class SwapHands : BaseCardBehaviour
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
            return EGenericTarget.ChooseOnePlayer;
        }
    }
}