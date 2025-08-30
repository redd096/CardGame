using System.Collections;
using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Select X cards in the deck, and draw them
    /// </summary>
    [System.Serializable]
    public class DrawDeckSelectableCard : DrawDeckCards
    {
        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return base.PlayerExecute(cardBehaviours, behaviourIndex);
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return base.AdversaryExecute(cardBehaviours, behaviourIndex);
        }
    }
}