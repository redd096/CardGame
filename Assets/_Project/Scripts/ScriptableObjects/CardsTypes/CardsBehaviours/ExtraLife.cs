using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// A Life card, but only when player consume one action to put it on the table
    /// </summary>
    [System.Serializable]
    public class ExtraLife : Life
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