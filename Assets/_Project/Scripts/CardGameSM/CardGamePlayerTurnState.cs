using System.Collections.Generic;
using redd096.StateMachine;

namespace cg
{
    public class CardGamePlayerTurnState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        private Dictionary<BaseCard, CardUI> uiCards = new Dictionary<BaseCard, CardUI>();

        public void Enter()
        {
            //set player cards in ui
            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            List<BaseCard> playerCards = currentPlayer.Cards;
            uiCards = CardGameUIManager.instance.SetCards(isPlayer: true, playerCards.ToArray());

            //and register to cards events
            foreach (var keypair in uiCards)
            {
                keypair.Value.onClickSelect += OnClickCard;
            }
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