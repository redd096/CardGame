using UnityEngine;

namespace cg
{
    /// <summary>
    /// Steal X cards of a specified type to other players
    /// </summary>
    [System.Serializable]
    public class StealPlayerSpecificCardType : StealPlayerCards
    {
        [Space]
        public ECardType TypeCardsToSteal;
    }
}