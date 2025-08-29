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
        public CardGamePlayerTurnState PlayerTurnState;
        public CardGameAdversaryTurnState AdversaryTurnState;

        protected override void Awake()
        {
            base.Awake();

            SetState(StartState);
        }
    }
}