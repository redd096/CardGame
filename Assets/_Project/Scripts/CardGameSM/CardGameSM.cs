using System.Collections.Generic;
using redd096.StateMachine;

namespace cg
{
    /// <summary>
    /// StateMachine for the card game
    /// </summary>
    public class CardGameSM : StateMachine<CardGameSM>
    {
        //states
        public CardGameStartState StartState;
        public CardGameDrawStartingCardsState DrawStartingCardsState;
        public CardGameDrawTurnCardsState DrawTurnCardsState;
        public CardGameAutomaticallyPlayCardsOnDrawState AutomaticallyPlayCardsOnDrawState;
        public CardGamePlayerTurnState PlayerTurnState;
        public CardGameAdversaryTurnState AdversaryTurnState;

        //blackboard
        public List<BaseCard> CardsToPlayOnDraw { get; set; } = new List<BaseCard>();

        protected override void Awake()
        {
            base.Awake();

            SetState(StartState);
        }
    }
}