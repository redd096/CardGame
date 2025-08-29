using System.Collections;
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
            CardGameUIManager.instance.UpdateInfoLabel($"Player {CardGameManager.instance.currentPlayer} is drawing cards...");
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
            bool isPlayer = playerIndex == 0;

            //draw X cards
            for (int cardIndex = 0; cardIndex < rules.DrawCards; cardIndex++)
            {
                //stop if reach limit in hands
                if (currentPlayer.CardsInHands.Count >= rules.MaxCardsInHand)
                    break;

                //animation delay
                if (isPlayer)
                    yield return new WaitForSeconds(DELAY_BETWEEN_PLAYER_CARDS);

                CardGameManager.instance.DrawNextCard(playerIndex);

                //update ui
                CardGameUIManager.instance.SetCards(isPlayer, CardGameManager.instance.Players[playerIndex].CardsInHands.ToArray());
            }

            //change state
            StateMachine.SetState(isPlayer ? StateMachine.PlayerTurnState : StateMachine.AdversaryTurnState);
        }
    }
}