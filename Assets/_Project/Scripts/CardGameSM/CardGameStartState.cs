using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// The first state of the StateMachine, where every player draw initial cards
    /// </summary>
    [System.Serializable]
    public class CardGameStartState : IState<CardGameSM>
    {
        [SerializeField] private GameObject panel;

        public CardGameSM StateMachine { get; set; }

        public void Enter()
        {
            panel.SetActive(true);
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
            panel.SetActive(false);
        }
    }
}