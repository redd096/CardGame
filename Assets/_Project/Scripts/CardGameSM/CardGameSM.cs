using redd096.StateMachine;

namespace cg
{
    /// <summary>
    /// StateMachine for the card game
    /// </summary>
    public class CardGameSM : StateMachine<CardGameSM>
    {
        public CardGameStartState StartState;
        public CardGameDrawStartingCardsState DrawStartingCardsState;
        public CardGamePlayerTurnState PlayerTurnState;

        protected override void Awake()
        {
            base.Awake();

            SetState(StartState);
        }
    }
}