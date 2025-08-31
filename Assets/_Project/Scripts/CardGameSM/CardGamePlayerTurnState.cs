using System.Collections;
using System.Collections.Generic;
using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Player play its cards
    /// </summary>
    [System.Serializable]
    public class CardGamePlayerTurnState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        private const bool IS_REAL_PLAYER = true;
        private const float DELAY_PLAY_CARD_INFO = 1f;
        private PlayerLogic currentPlayer;
        private Dictionary<BaseCard, CardUI> uiCards = new Dictionary<BaseCard, CardUI>();

        public void Enter()
        {
            //set player cards in ui
            currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            List<BaseCard> playerCards = currentPlayer.CardsInHands;
            uiCards = CardGameUIManager.instance.SetCards(IS_REAL_PLAYER, playerCards.ToArray());

            //and register to cards events
            foreach (var keypair in uiCards)
            {
                keypair.Value.onClickSelect += OnClickCard;
            }

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Select card to play");
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
            //unregister cards events
            foreach (var keypair in uiCards)
            {
                if (keypair.Value)
                    keypair.Value.onClickSelect -= OnClickCard;
            }

            //reset vars
            currentPlayer.LastSelectedPlayers.Clear();
            currentPlayer.LastRange = 0;

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel("");
        }

        private void OnClickCard(CardUI uiCard, BaseCard card)
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