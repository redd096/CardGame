using System.Collections;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players, but if hit a specified type, then destroy another card to the same player
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCardsButDestroyAgainIfSpecificCardType : DestroyPlayerCards
    {
        [Space]
        public ECardType TypeCardsToDestroyAgain;

        protected override IEnumerator SelectOneCardOrBonus(bool currentIsRealPlayer, PlayerLogic attackedPlayer, int attackedPlayerIndex, bool attackedIsRealPlayer)
        {
            yield return base.SelectOneCardOrBonus(currentIsRealPlayer, attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer);

            //if destroyed correct card type, destroy again one card
            if (selectedCard != null && selectedCard.CardType == TypeCardsToDestroyAgain
                || selectedBonus != null && selectedBonus.CardType == TypeCardsToDestroyAgain)
            {
                yield return base.SelectOneCardOrBonus(currentIsRealPlayer, attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer);
            }
        }
    }
}