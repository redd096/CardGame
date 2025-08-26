using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// The first state of the StateMachine, where every player draw initial cards
    /// </summary>
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