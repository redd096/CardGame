using System.Collections;
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

            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();

            //if destroyed correct card type, steal instead of destroy
            if (selectedCard != null && selectedCard.CardType == TypeCardsToStealInsteadOfDestroy)
            {
                BaseCard destroyedCard = CardGameManager.instance.DiscardsDeck.Pop();
                currentPlayer.CardsInHands.Add(destroyedCard);
                CardGameUIManager.instance.SetCards(currentIsRealPlayer, currentPlayer.CardsInHands.ToArray(), showFront: currentIsRealPlayer);
            }
            else if (selectedBonus != null && selectedBonus.CardType == TypeCardsToStealInsteadOfDestroy)
            {
                currentPlayer.AddBonus(selectedBonus);
                CardGameUIManager.instance.SetBonus(currentIsRealPlayer, currentPlayer.ActiveBonus.ToArray());
            }
        }
    }
}