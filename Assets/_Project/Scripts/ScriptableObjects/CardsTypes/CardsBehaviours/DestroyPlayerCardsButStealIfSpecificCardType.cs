using System.Collections;
using System.Collections.Generic;
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

        protected override IEnumerator AttackOneCardOrBonus(bool currentIsRealPlayer, PlayerLogic attackedPlayer, int attackedPlayerIndex, bool attackedIsRealPlayer)
        {
            yield return base.AttackOneCardOrBonus(currentIsRealPlayer, attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer);

            //if destroyed correct card type, destroy again one card
            if (selectedCard != null && selectedCard.CardType == TypeCardsToStealInsteadOfDestroy
                || selectedBonus != null && selectedBonus.BonusCardType == TypeCardsToStealInsteadOfDestroy)
            {
                Debug.LogError("TODO - INSTEAD OF CALL DISCARD, SHOULD STEAL THE SELECTED CARD OR BONUS");
            }
        }
    }
}