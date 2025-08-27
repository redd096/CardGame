using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Every player draw initial cards, then start first player turn
    /// </summary>
    [System.Serializable]
    public class CardGameDrawStartingCards : IState<CardGameSM>
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