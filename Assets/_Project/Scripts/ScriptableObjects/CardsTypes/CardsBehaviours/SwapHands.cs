using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Select one player and swap cards with him
    /// </summary>
    [System.Serializable]
    public class SwapHands : BaseCardBehaviour
    {
        public override void PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            
        }

        public override void AdversaryExecute()
        {
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.ChooseOnePlayer;
        }
    }
}