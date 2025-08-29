using redd096.StateMachine;
using System.Collections;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Every player draw initial cards, then start first player turn
    /// </summary>
    [System.Serializable]
    public class CardGameDrawStartingCardsState : IState<CardGameSM>
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
            CardGameUIManager.instance.UpdateInfoLabel("Drawing start cards...");
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
            Deck deck = CardGameManager.instance.Deck;
            int numberOfPlayers = CardGameManager.instance.NumberOfPlayers;

            //shuffle deck if necessary
            if (deck.ShuffleOnStartGame)
                deck.ShuffleDeck();

            //foreach player
            for (int playerIndex = 0; playerIndex < numberOfPlayers; playerIndex++)
            {
                bool isPlayer = playerIndex == 0;

                for (int cardIndex = 0; cardIndex < rules.StartCards; cardIndex++)
                {
                    //animation delay
                    if (isPlayer)
                        yield return new WaitForSeconds(DELAY_BETWEEN_PLAYER_CARDS);

                    //draw life cards, then any cards
                    if (cardIndex < rules.StartLife)
                        CardGameManager.instance.DrawCardWithSpecificBehaviour(playerIndex, typeof(Life));
                    else
                        CardGameManager.instance.DrawNextCard(playerIndex);

                    //update ui
                    CardGameUIManager.instance.SetCards(isPlayer, CardGameManager.instance.Players[playerIndex].CardsInHands.ToArray());
                }
            }

            //change state
            StateMachine.SetState(StateMachine.DrawTurnCardsState);
        }
    }
}