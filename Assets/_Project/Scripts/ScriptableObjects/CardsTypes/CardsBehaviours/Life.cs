using System.Collections;

namespace cg
{
    /// <summary>
    /// Life card. When user lose all of them, he lost the game
    /// </summary>
    [System.Serializable]
    public class Life : BaseCardBehaviour
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