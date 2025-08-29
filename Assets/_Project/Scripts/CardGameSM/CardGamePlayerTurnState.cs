using System.Collections.Generic;
using redd096.StateMachine;

namespace cg
{
    /// <summary>
    /// Player play its cards
    /// </summary>
    [System.Serializable]
    public class CardGamePlayerTurnState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        private Dictionary<BaseCard, CardUI> uiCards = new Dictionary<BaseCard, CardUI>();

        public void Enter()
        {
            //set player cards in ui
            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            List<BaseCard> playerCards = currentPlayer.CardsInHands;
            uiCards = CardGameUIManager.instance.SetCards(isPlayer: true, playerCards.ToArray());

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
                keypair.Value.onClickSelect -= OnClickCard;
            }

            //change turn
            CardGameManager.instance.StartNextTurn();

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel("");
        }

        private void OnClickCard(CardUI uiCard, BaseCard card)
        {
            if (card is GenericCard genericCard)
            {
                //cycle cards behaviours
                for (int i = 0; i < genericCard.CardsBehaviours.Count; i++)
                {
                    BaseCardBehaviour cardBehaviour = genericCard.CardsBehaviours[i];

                }
            }
        }
    }
}