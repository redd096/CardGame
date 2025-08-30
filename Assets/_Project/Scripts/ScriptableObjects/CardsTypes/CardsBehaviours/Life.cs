using System.Collections;
using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Life card. When user lose all of them, he lost the game
    /// </summary>
    [System.Serializable]
    public class Life : BaseCardBehaviour
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
            return EGenericTarget.None;
        }
    }
}