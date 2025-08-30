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

        protected override IEnumerator SelectOneCardOrBonus(bool currentIsRealPlayer, PlayerLogic attackedPlayer, int attackedPlayerIndex, bool attackedIsRealPlayer)
        {
            // return base.SelectOneCardOrBonus(currentIsRealPlayer, attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer);

            //TODO non dovrebbe far selezionare, ma dovrebbe automaticamente distruggere una carta di quel tipo all'avversario

            //be sure there are cards or bonus to select
            if (HasBonusOrCardsToSelect(attackedPlayer, out List<BaseCard> possibleBonus, out List<BaseCard> possibleCards))
            {
                CardGameUIManager.instance.UpdateInfoLabel($"There aren't other cards or bonus to select from player {attackedPlayerIndex + 1}");
                yield return new WaitForSeconds(LOW_DELAY);
                yield break;
            }

            Dictionary<BaseCard, CardUI> attackedCards = attackedIsRealPlayer ? CardGameUIManager.instance.playerCardsInScene : CardGameUIManager.instance.adversaryCardsInScene;
            Dictionary<BaseCard, BonusUI> attackedBonus = attackedIsRealPlayer ? CardGameUIManager.instance.playerBonusInScene : CardGameUIManager.instance.adversaryBonusInScene;

            //if current is a player, wait for him to select card or bonus to attack
            if (currentIsRealPlayer)
            {
                foreach (var keypair in attackedCards)
                    keypair.Value.onClickSelect += OnSelectCardToDestroy;
                foreach (var keypair in attackedBonus)
                    keypair.Value.onClickSelect += OnSelectBonusToDestroy;

                CardGameUIManager.instance.UpdateInfoLabel("Select one card or bonus to attack...");
                selectedCard = null;
                selectedBonus = null;
                yield return new WaitUntil(() => selectedCard != null || selectedBonus != null);

                foreach (var keypair in attackedCards)
                    keypair.Value.onClickSelect -= OnSelectCardToDestroy;
                foreach (var keypair in attackedBonus)
                    keypair.Value.onClickSelect -= OnSelectBonusToDestroy;
            }
            //if adversary, just select one random bonus or card
            else
            {
                if (possibleBonus.Count > 0)
                    selectedBonus = possibleBonus[Random.Range(0, possibleBonus.Count)];
                else
                    selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];
            }

            yield return AttackOneCardOrBonus(attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer, attackedCards);
        }

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
    }
}