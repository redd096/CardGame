using System.Collections;

namespace cg
{
    /// <summary>
    /// Stop card. User can use it anytime to stop another player card attack
    /// </summary>
    [System.Serializable]
    public class Stop : BaseCardBehaviour
    {
        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }
    }
}