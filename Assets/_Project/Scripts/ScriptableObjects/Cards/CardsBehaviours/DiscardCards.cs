using System.Collections;
using System.Collections.Generic;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard X cards
    /// </summary>
    [System.Serializable]
    public class DiscardCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public EDiscardTarget TargetCard;
        [Tooltip("Numbers are calculated for a game with 2 players. When number of players increments, increment also numbers of cards")] public bool IncreaseWithNumberOfPlayers;
        [EnableIf(nameof(IncreaseWithNumberOfPlayers))][Min(1)] public int NumberIncrement = 1;
        public int CorrectNumberOfCards => NumberOfCards + (IncreaseWithNumberOfPlayers ? NumberIncrement * Mathf.Max(CardGameManager.instance.StillActivePlayers.Count - 2, 0) : 0);

        protected int selectedPlayerIndex;
        protected BaseCard selectedCard;
        protected BaseCard selectedBonus;

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }

        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            //select targets
            var selectTargetHelper = new BehaviourSelectTargetHelper(this);
            yield return selectTargetHelper.SelectPlayersToAttack(isRealPlayer);

            //attack every target
            int currentPlayerIndex = CardGameManager.instance.currentPlayer;
            for (int i = 0; i < selectTargetHelper.selectedPlayers.Count; i++)
            {
                int targetIndex = selectTargetHelper.selectedPlayers[i];
                yield return AttackOnePlayer(isRealPlayer, currentPlayerIndex, targetIndex);

                //delay between targets (not if this is the last one)
                 if (i < selectTargetHelper.selectedPlayers.Count - 1)
                    yield return new WaitForSeconds(LOW_DELAY);
            }

            yield return new WaitForSeconds(DELAY_AFTER_BEHAVIOUR);
        }

        #region utils

        private void ShowPlayerCards(int attackedPlayerIndex)
        {
            //show selected player cards and bonus
            PlayerLogic attackedPlayer = CardGameManager.instance.Players[attackedPlayerIndex];
            bool attackedIsRealPlayer = CardGameManager.instance.IsRealPlayer(attackedPlayerIndex);
            CardGameUIManager.instance.SetCards(attackedIsRealPlayer, attackedPlayer.CardsInHands.ToArray(), showFront: attackedIsRealPlayer);
            CardGameUIManager.instance.SetBonus(attackedIsRealPlayer, attackedPlayer.ActiveBonus.ToArray());
            CardGameUIManager.instance.ShowAdversaryCardsAndBonus(true);
            CardGameUIManager.instance.UpdatePlayers(attackedPlayerIndex);
        }

        protected virtual bool HasBonusOrCardsToSelect(PlayerLogic player, out List<BaseCard> possibleBonus, out List<BaseCard> possibleCards)
        {
            //possible selections
            possibleBonus = new List<BaseCard>();
            foreach (var card in player.ActiveBonus)
            {
                IBonusCard bonus = card as IBonusCard;
                if (bonus.CanBeDestroyed())
                    possibleBonus.Add(card);
            }
            possibleCards = new List<BaseCard>();
            foreach (var card in player.CardsInHands)
                possibleCards.Add(card);

            return possibleBonus.Count > 0 || possibleCards.Count > 0;
        }

        #endregion

        #region events

        protected void OnSelectCardToDestroy(CardUI cardUI, BaseCard card)
        {
            //set selected card
            selectedCard = card;
        }

        protected void OnSelectBonusToDestroy(BonusUI bonusUI, BaseCard card)
        {
            //be sure bonus can be attacked
            IBonusCard bonus = card as IBonusCard;
            if (bonus.CanBeDestroyed() == false)
            {
                CardGameUIManager.instance.UpdateInfoLabel("Can't select this bonus. Please, select another card or bonus to attack...");
                return;
            }

            //set selected bonus
            selectedBonus = card;
        }

        #endregion

        #region private API

        /// <summary>
        /// When a player is selected, attack its cards or bonus
        /// </summary>
        protected virtual IEnumerator AttackOnePlayer(bool currentIsRealPlayer, int currentPlayerIndex, int attackedPlayerIndex)
        {
            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Player {currentPlayerIndex + 1} is attacking Player {attackedPlayerIndex + 1}");

            //show selected player cards and bonus
            PlayerLogic attackedPlayer = CardGameManager.instance.Players[attackedPlayerIndex];
            bool attackedIsRealPlayer = CardGameManager.instance.IsRealPlayer(attackedPlayerIndex);
            ShowPlayerCards(attackedPlayerIndex);

            yield return new WaitForSeconds(LOW_DELAY);

            for (int i = 0; i < CorrectNumberOfCards; i++)
            {
                //be sure there are cards or bonus to select
                if (HasBonusOrCardsToSelect(attackedPlayer, out _, out _))
                {
                    CardGameUIManager.instance.UpdateInfoLabel($"There aren't other cards or bonus to select from player {attackedPlayerIndex + 1}");
                    yield return new WaitForSeconds(LOW_DELAY);
                    break;
                }

                yield return SelectOneCardOrBonus(currentIsRealPlayer, attackedPlayer, attackedPlayerIndex, attackedIsRealPlayer);

                //if player is dead, it's useless to continue destroy cards
                if (attackedPlayer.IsAlive() == false)
                {
                    CardGameManager.instance.UpdateAlivePlayers();
                    break;
                }
            }

            //update UI
            CardGameUIManager.instance.ShowAdversaryCardsAndBonus(false);
            CardGameUIManager.instance.UpdatePlayers();
            if (attackedPlayer.IsAlive() == false)
            {
                yield return new WaitForSeconds(LOW_DELAY);
                CardGameUIManager.instance.UpdateInfoLabel($"Player {attackedPlayerIndex + 1} is dead");
            }
        }

        /// <summary>
        /// Select one card or bonus to attack
        /// </summary>
        protected virtual IEnumerator SelectOneCardOrBonus(bool currentIsRealPlayer, PlayerLogic attackedPlayer, int attackedPlayerIndex, bool attackedIsRealPlayer)
        {
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

        /// <summary>
        /// When a card is selected, apply behaviour to it
        /// </summary>
        protected IEnumerator AttackOneCardOrBonus(PlayerLogic attackedPlayer, int attackedPlayerIndex, bool attackedIsRealPlayer, Dictionary<BaseCard, CardUI> attackedCards)
        {
            //if card, show it
            if (selectedCard)
            {
                if (attackedIsRealPlayer)
                {
                    //rotate just to understand the selected card on the player
                    attackedCards[selectedCard].ShowFrontOrBack(showFront: false);
                    yield return new WaitForSeconds(LOW_DELAY);
                }
                attackedCards[selectedCard].ShowFrontOrBack(showFront: true);
                yield return new WaitForSeconds(LOW_DELAY);

                //destroy card
                CardGameManager.instance.DiscardCard(attackedPlayerIndex, selectedCard);
                CardGameUIManager.instance.SetCards(attackedIsRealPlayer, attackedPlayer.CardsInHands.ToArray(), showFront: attackedIsRealPlayer);
            }
            //else remove bonus
            else
            {
                attackedPlayer.RemoveBonus(selectedBonus.GetType(), quantity: 1);
                CardGameUIManager.instance.SetBonus(attackedIsRealPlayer, attackedPlayer.ActiveBonus.ToArray());
            }
        }

        #endregion
    }
}