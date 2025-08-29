using redd096.StateMachine;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Adversary play its cards
    /// </summary>
    [System.Serializable]
    public class CardGameAdversaryTurnState : IState<CardGameSM>
    {
        public CardGameSM StateMachine { get; set; }

        public void Enter()
        {

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Player {CardGameManager.instance.currentPlayer} is selecting card to play");
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel("");
        }
    }
}