using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards of a specified type to other players
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerSpecificCardType : DestroyPlayerCards
    {
        [Space]
        public ECardType TypeCardsToDestroy;
    }
}