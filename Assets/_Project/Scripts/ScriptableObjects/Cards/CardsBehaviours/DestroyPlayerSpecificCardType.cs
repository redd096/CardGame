using System.Collections;
using System.Collections.Generic;
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

        protected override bool HasBonusOrCardsToSelect(PlayerLogic player, out List<BaseCard> possibleBonus, out List<BaseCard> possibleCards)
        {
            // return base.HasBonusOrCardsToSelect(player, out possibleBonus, out possibleCards);

            //possible selections
            possibleBonus = new List<BaseCard>();
            foreach (var card in player.ActiveBonus)
            {
                IBonusCard bonus = card as IBonusCard;
                if (bonus.CanBeDestroyed() && card.CardType == TypeCardsToDestroy)  //only specific type
                    possibleBonus.Add(card);
            }
            possibleCards = new List<BaseCard>();
            foreach (var card in player.CardsInHands)
            {
                if (card.CardType == TypeCardsToDestroy)                            //only specific type
                    possibleCards.Add(card);
            }

            return possibleBonus.Count > 0 || possibleCards.Count > 0;
        }

        protected override IEnumerator SelectOneCardOrBonus(bool currentIsRealPlayer, PlayerLogic attackedPlayer, int attackedPlayerIndex, bool attackedIsRealPlayer)
        {
            // return base.SelectOneCardOrBonus(currentIsRealPlayer, attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer);

            //be sure there are cards or bonus to select
            if (HasBonusOrCardsToSelect(attackedPlayer, out List<BaseCard> possibleBonus, out List<BaseCard> possibleCards))
            {
                CardGameUIManager.instance.UpdateInfoLabel($"There aren't other cards or bonus to select from player {attackedPlayerIndex + 1}");
                yield return new WaitForSeconds(LOW_DELAY);
                yield break;
            }

            Dictionary<BaseCard, CardUI> attackedCards = attackedIsRealPlayer ? CardGameUIManager.instance.playerCardsInScene : CardGameUIManager.instance.adversaryCardsInScene;
            // Dictionary<BaseCard, BonusUI> attackedBonus = attackedIsRealPlayer ? CardGameUIManager.instance.playerBonusInScene : CardGameUIManager.instance.adversaryBonusInScene;

            //destroy random card also for real player. Can't choose cards because we can't see enemy cards
            if (possibleBonus.Count > 0)
                selectedBonus = possibleBonus[Random.Range(0, possibleBonus.Count)];
            else
                selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];

            yield return AttackOneCardOrBonus(attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer, attackedCards);
        }
    }
}