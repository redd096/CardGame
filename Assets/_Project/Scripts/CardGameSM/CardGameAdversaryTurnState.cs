using System.Collections;
using System.Collections.Generic;
using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Adversary play its cards
    /// </summary>
    [System.Serializable]
    public class CardGameAdversaryTurnState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        private const bool IS_REAL_PLAYER = false;
        private const float DELAY_PLAY_CARD_INFO = 1f;
        private PlayerLogic currentPlayer;

        public void Enter()
        {
            //set player cards in ui
            currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            List<BaseCard> playerCards = currentPlayer.CardsInHands;
            CardGameUIManager.instance.SetCards(IS_REAL_PLAYER, playerCards.ToArray());

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Player {CardGameManager.instance.currentPlayer + 1} is selecting card to play");

            //adversary select a card to play
            SelectCardToPlay();
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
            //reset vars
            currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            currentPlayer.LastSelectedPlayers.Clear();
            currentPlayer.LastRange = 0;

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel("");
        }

        private void SelectCardToPlay()
        {
            //find card to play by type
            BaseCard cardToPlay = currentPlayer.CardsInHands.Find(x => x.CardType == ECardType.Steal);
            if (cardToPlay == null)
                cardToPlay = currentPlayer.CardsInHands.Find(x => x.CardType == ECardType.Destroy);
            if (cardToPlay == null)
                cardToPlay = currentPlayer.CardsInHands.Find(x => x.CardType == ECardType.Normal);
            if (cardToPlay == null)
                cardToPlay = currentPlayer.CardsInHands.Find(x => x.CardType == ECardType.Discard);

            if (cardToPlay == null)
            {
                Debug.LogError($"Error: for some reason Player {CardGameManager.instance.currentPlayer + 1} doesn't have cards to play");
                //change turn
                CardGameManager.instance.StartNextTurn();
                StateMachine.SetState(StateMachine.DrawTurnCardsState);
                return;
            }

            //play found card
            OnClickCard(cardToPlay);
        }

        private void OnClickCard(BaseCard card)
        {
            //start coroutine
            StateMachine.StartCoroutine(PlayCardBehaviours());

            IEnumerator PlayCardBehaviours()
            {
                //discard card and update ui
                CardGameManager.instance.DiscardCard(CardGameManager.instance.currentPlayer, card);
                CardGameUIManager.instance.SetCards(IS_REAL_PLAYER, currentPlayer.CardsInHands.ToArray());

                //cycle card behaviours
                if (card is GenericCard genericCard)
                {
                    //update ui
                    CardGameUIManager.instance.UpdateInfoLabel($"Player {CardGameManager.instance.currentPlayer + 1} played card: {card.CardName}.");
                    yield return new WaitForSeconds(DELAY_PLAY_CARD_INFO);

                    for (int i = 0; i < genericCard.CardsBehaviours.Count; i++)
                    {
                        BaseCardBehaviour cardBehaviour = genericCard.CardsBehaviours[i];
                        yield return cardBehaviour.Execute(IS_REAL_PLAYER, genericCard, i);
                    }
                }

                //change turn
                CardGameManager.instance.StartNextTurn();
                StateMachine.SetState(StateMachine.DrawTurnCardsState);
            }
        }
    }
}