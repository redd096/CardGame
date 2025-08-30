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
            CardGameUIManager.instance.UpdateInfoLabel($"Player {CardGameManager.instance.currentPlayer + 1} is selecting card to play");
        }

        public void UpdateState()
        {
        }

        public void Exit()
        {
            //reset vars
            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();
            currentPlayer.LastSelectedPlayers.Clear();
            currentPlayer.LastRange = 0;

            //change turn
            CardGameManager.instance.StartNextTurn();

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel("");
        }
    }
}