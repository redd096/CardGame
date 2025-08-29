
using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Select X cards in the deck, and draw them
    /// </summary>
    [System.Serializable]
    public class DrawDeckSelectableCard : DrawDeckCards
    {
        public override void PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            base.PlayerExecute(cardBehaviours, behaviourIndex);
        }

        public override void AdversaryExecute()
        {
            base.AdversaryExecute();
        }
    }
}