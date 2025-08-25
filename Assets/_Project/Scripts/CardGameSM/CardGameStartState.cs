using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    public class CardGameStartState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        public void Enter()
        {
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
        }
    }
}