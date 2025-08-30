using System.Collections;
using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Select one player and swap cards with him
    /// </summary>
    [System.Serializable]
    public class SwapHands : BaseCardBehaviour
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
            return EGenericTarget.ChooseOnePlayer;
        }
    }
}