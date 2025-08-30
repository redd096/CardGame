using System.Collections;
using System.Collections.Generic;
using redd096.StateMachine;

namespace cg
{
    /// <summary>
    /// If player or adversary drawed cards to execute on draw, play them automatically
    /// </summary>
    [System.Serializable]
    public class CardGameAutomaticallyPlayCardsOnDrawState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        public void Enter()
        {
            StateMachine.StartCoroutine(PlayCardsCoroutine());
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
            //reset blackboard
            StateMachine.CardsToPlayOnDraw.Clear();
        }

        private IEnumerator PlayCardsCoroutine()
        {
            List<BaseCard> cardsToPlay = StateMachine.CardsToPlayOnDraw;
            int playerIndex = CardGameManager.instance.currentPlayer;
            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            bool isRealPlayer = CardGameManager.instance.IsRealPlayer(playerIndex);

            //cycle cards that have behaviours to play on draw
            for (int i = 0; i < cardsToPlay.Count; i++)
            {
                if (cardsToPlay[i] is GenericCard genericCard
                    && genericCard.HasBehaviourToExecuteOnDraw(out List<BaseCardBehaviour> behaviours))
                {
                    //discard card and update ui
                    CardGameManager.instance.DiscardCard(CardGameManager.instance.currentPlayer, genericCard);
                    CardGameUIManager.instance.SetCards(isRealPlayer, currentPlayer.CardsInHands.ToArray());

                    //cycle behaviours
                    for (int j = 0; j < behaviours.Count; j++)
                    {
                        BaseCardBehaviour cardBehaviour = genericCard.CardsBehaviours[j];
                        yield return cardBehaviour.PlayerExecute(genericCard.CardsBehaviours, j);
                    }
                }
            }

            //move to player or adversary state
            StateMachine.SetState(isRealPlayer ? StateMachine.PlayerTurnState : StateMachine.AdversaryTurnState);
        }
    }
}