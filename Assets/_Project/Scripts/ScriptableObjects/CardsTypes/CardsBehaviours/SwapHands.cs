using System.Collections;

namespace cg
{
    /// <summary>
    /// Select one player and swap cards with him
    /// </summary>
    [System.Serializable]
    public class SwapHands : BaseCardBehaviour
    {
        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.ChooseOnePlayer;
        }
    }
}