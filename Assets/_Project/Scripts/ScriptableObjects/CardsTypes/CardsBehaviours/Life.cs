using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Life card. When user lose all of them, he lost the game
    /// </summary>
    [System.Serializable]
    public class Life : BaseCardBehaviour
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