using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players, but if hit a specified type, then steal that card instead of destroy it
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCardsButStealIfSpecificCardType : DestroyPlayerCards
    {
        [Space]
        public ECardType TypeCardsToStealInsteadOfDestroy;
    }
}