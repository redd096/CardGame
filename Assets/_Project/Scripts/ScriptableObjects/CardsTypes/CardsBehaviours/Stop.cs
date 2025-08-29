using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Stop card. User can use it anytime to stop another player card attack
    /// </summary>
    [System.Serializable]
    public class Stop : BaseCardBehaviour
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