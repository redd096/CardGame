using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public ETargetCard TargetCard;

        protected int selectedPlayerIndex;
        protected BaseCard selectedCard;
        protected BaseCard selectedBonus;

        public override IEnumerator PlayerExecute(BaseCard card, int behaviourIndex)
        {
            yield return Execute(true);
        }

        public override IEnumerator AdversaryExecute(BaseCard card, int behaviourIndex)
        {
            yield return Execute(false);
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }

        protected IEnumerator Execute(bool isRealPlayer)
        {
            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            int currentPlayerIndex = CardGameManager.instance.currentPlayer;
            List<PlayerLogic> players = CardGameManager.instance.Players;

            switch (TargetCard)
            {
                case ETargetCard.ChooseOnePlayer:
                    //if this is the player, wait for him to select a player
                    if (isRealPlayer)
                    {
                        foreach (var keypair in CardGameUIManager.instance.playersInScene)
                            keypair.Value.onClickSelect += OnSelectPlayer;

                        CardGameUIManager.instance.UpdateInfoLabel("Select one player to attack...");
                        selectedPlayerIndex = -1;

                        while (true)
                        {
                            int temp = selectedPlayerIndex;
                            yield return new WaitUntil(() => selectedPlayerIndex != temp);
                            ShowPlayerCards(selectedPlayerIndex);
                            //TODO mostrare popup e se si preme no si nasconde il popup, se si preme si fa un break e si prosegue con la coroutine
                        }
                        
                        foreach (var keypair in CardGameUIManager.instance.playersInScene)
                            keypair.Value.onClickSelect -= OnSelectPlayer;
                    }
                    //for adversary, just select one player random
                    else
                    {
                        List<int> possiblePlayers = new List<int>();
                        for (int i = 0; i < players.Count; i++)
                        {
                            if (CanSelectPlayer(currentPlayerIndex, i))
                                possiblePlayers.Add(i);
                        }
                        selectedPlayerIndex = possiblePlayers[Random.Range(0, possiblePlayers.Count)];
                    }

                    //attack selected player
                    currentPlayer.LastSelectedPlayers.Add(selectedPlayerIndex);
                    yield return AttackOnePlayer(isRealPlayer, currentPlayerIndex, selectedPlayerIndex);
                    yield return new WaitForSeconds(DELAY_AFTER_BEHAVIOUR);
                    break;
                case ETargetCard.EveryOtherPlayer:
                    //attack every player
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (CanSelectPlayer(currentPlayerIndex, i))
                        {
                            yield return AttackOnePlayer(isRealPlayer, currentPlayerIndex, i);
                            yield return new WaitForSeconds(LOW_DELAY);
                        }
                    }
                    yield return new WaitForSeconds(DELAY_AFTER_BEHAVIOUR);
                    break;
                case ETargetCard.EveryOtherPlayerExceptPreviouslyChoosedOne:
                    //attack every player not already selected in previous behaviours
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (CanSelectPlayer(currentPlayerIndex, i) && currentPlayer.LastSelectedPlayers.Contains(i) == false)
                        {
                            yield return AttackOnePlayer(isRealPlayer, currentPlayerIndex, i);
                            yield return new WaitForSeconds(LOW_DELAY);
                        }
                    }
                    yield return new WaitForSeconds(DELAY_AFTER_BEHAVIOUR);
                    break;
            }
        }

        #region private API

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

        /// <summary>
        /// When a player is selected, attack its cards or bonus
        /// </summary>
        protected IEnumerator AttackOnePlayer(bool currentIsRealPlayer, int currentPlayerIndex, int attackedPlayerIndex)
        {
            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Player {currentPlayerIndex + 1} is attacking Player {attackedPlayerIndex + 1}");

            //show selected player cards and bonus
            PlayerLogic attackedPlayer = CardGameManager.instance.Players[attackedPlayerIndex];
            bool attackedIsRealPlayer = CardGameManager.instance.IsRealPlayer(attackedPlayerIndex);
            CardGameUIManager.instance.SetCards(attackedIsRealPlayer, attackedPlayer.CardsInHands.ToArray(), showFront: attackedIsRealPlayer);
            CardGameUIManager.instance.SetBonus(attackedIsRealPlayer, attackedPlayer.ActiveBonus.ToArray());
            CardGameUIManager.instance.ShowAdversaryCardsAndBonus(true);
            CardGameUIManager.instance.UpdatePlayers(attackedPlayerIndex);

            yield return new WaitForSeconds(LOW_DELAY);

            for (int i = 0; i < NumberOfCards; i++)
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
                    break;
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

        protected void OnSelectPlayer(PlayerUI playerUI, int clickedPlayerIndex)
        {
            //be sure player can be selected
            int currentPlayerIndex = CardGameManager.instance.currentPlayer;
            if (CanSelectPlayer(currentPlayerIndex, clickedPlayerIndex) == false)
            {
                CardGameUIManager.instance.UpdateInfoLabel("Can't select this player. Please, select another player to attack...");
                return;
            }

            //set selected player
            selectedPlayerIndex = clickedPlayerIndex;
        }

        protected bool CanSelectPlayer(int currentPlayerIndex, int playerIndexToSelect)
        {
            //ignore self or dead players
            PlayerLogic playerToSelect = CardGameManager.instance.Players[playerIndexToSelect];
            if (playerIndexToSelect == currentPlayerIndex || playerToSelect.IsAlive() == false)
                return false;
            return true;
        }

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
    }
}