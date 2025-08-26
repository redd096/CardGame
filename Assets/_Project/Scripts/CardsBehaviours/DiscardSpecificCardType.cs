using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard X cards of a specified type
    /// </summary>
    [System.Serializable]
    public class DiscardSpecificCardType : DiscardCards
    {
        [Space]
        public ECardType TypeCardsToDiscard;
    }
}