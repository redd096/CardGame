using System.Collections;

namespace cg
{
    /// <summary>
    /// Stop card. User can use it anytime to stop another player card attack
    /// </summary>
    [System.Serializable]
    public class Stop : BaseCardBehaviour
    {
        public override IEnumerator PlayerExecute(BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override IEnumerator AdversaryExecute(BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }
    }
}