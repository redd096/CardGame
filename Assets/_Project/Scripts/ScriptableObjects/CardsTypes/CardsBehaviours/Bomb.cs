using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// TODO
    /// </summary>
    [System.Serializable]
    public class Bomb : BaseCardBehaviour
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