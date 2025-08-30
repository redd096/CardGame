using System.Collections;
using System.Collections.Generic;
using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Current player draw X cards
    /// </summary>
    [System.Serializable]
    public class CardGameDrawTurnCardsState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        private const float DELAY_BETWEEN_PLAYER_CARDS = 0.1f;
        private Coroutine coroutine;

        public void Enter()
        {
            //start coroutine
            if (coroutine != null)
                StateMachine.StopCoroutine(coroutine);
            coroutine = StateMachine.StartCoroutine(GiveCardsCoroutine());

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Player {CardGameManager.instance.currentPlayer + 1} is drawing cards...");
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
            //update infos
            CardGameUIManager.instance.UpdateInfoLabel("");
        }

        private IEnumerator GiveCardsCoroutine()
        {
            Rules rules = CardGameManager.instance.Rules;
            int playerIndex = CardGameManager.instance.currentPlayer;
            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            bool isRealPlayer = CardGameManager.instance.IsRealPlayer(playerIndex);

            //draw X cards
            for (int cardIndex = 0; cardIndex < rules.DrawCards; cardIndex++)
            {
                //stop if reach limit in hands
                if (currentPlayer.CardsInHands.Count >= rules.MaxCardsInHand)
                    break;

                //animation delay
                if (isRealPlayer)
                    yield return new WaitForSeconds(DELAY_BETWEEN_PLAYER_CARDS);

                //draw card and check if this is to play on draw
                BaseCard drawedCard = CardGameManager.instance.DrawNextCard(playerIndex);
                if (drawedCard is GenericCard genericCard && genericCard.HasBehaviourToExecuteOnDraw(out _))
                    StateMachine.CardsToPlayOnDraw.Add(drawedCard);

                //update ui
                CardGameUIManager.instance.SetCards(isRealPlayer, CardGameManager.instance.Players[playerIndex].CardsInHands.ToArray());
            }

            //change state
            //if there are cards to play automatically on draw, move to that state
            if (StateMachine.CardsToPlayOnDraw.Count > 0)
                StateMachine.SetState(StateMachine.AutomaticallyPlayCardsOnDrawState);
            //else move to player or adversary state
            StateMachine.SetState(isRealPlayer ? StateMachine.PlayerTurnState : StateMachine.AdversaryTurnState);
        }
    }
}