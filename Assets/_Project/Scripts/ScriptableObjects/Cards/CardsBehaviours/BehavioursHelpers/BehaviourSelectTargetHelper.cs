using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    public class BehaviourSelectTargetHelper
    {
        //public
        public List<int> selectedPlayers = new List<int>();

        //private
        private BaseCardBehaviour behaviour;
        private EGenericTarget target => behaviour.GetGenericTargetCard();
        private bool canSelectSelf => target == EGenericTarget.Self || target == EGenericTarget.EveryPlayer || target == EGenericTarget.EveryPlayerExceptPreviouslyChoosedOne;

        public BehaviourSelectTargetHelper(BaseCardBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        /// <summary>
        /// Based on Target check who attack
        /// </summary>
        /// <param name="isRealPlayer">makes the player select targets or is an AI and select random?</param>
        /// <returns></returns>
        public IEnumerator SelectPlayersToAttack(bool isRealPlayer)
        {
            switch (target)
            {
                //attack none
                case EGenericTarget.None:
                    break;

                //attack self
                case EGenericTarget.Self:
                    SelectSelfTarget();
                    break;

                //if this is the player, wait for him to select a player
                //for adversary, just select one player random
                case EGenericTarget.ChooseOnePlayer:
                    if (isRealPlayer)
                        yield return WaitPlayerSelectTarget();
                    else
                        SelectRandomTarget();

                    //set selection in current player list
                    PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();
                    currentPlayer.LastSelectedPlayers.Add(selectedPlayers[0]);
                    break;

                //attack every player
                case EGenericTarget.EveryPlayer:
                case EGenericTarget.EveryOtherPlayer:
                    SelectEveryTarget(canSelectLastSelection: true);
                    break;

                //attack every player not already selected in previous behaviours
                case EGenericTarget.EveryPlayerExceptPreviouslyChoosedOne:
                case EGenericTarget.EveryOtherPlayerExceptPreviouslyChoosedOne:
                    SelectEveryTarget(canSelectLastSelection: false);
                    break;
            }
        }

        #region select players

        private void SelectSelfTarget()
        {
            //select self
            int currentPlayerIndex = CardGameManager.instance.currentPlayer;
            selectedPlayers.Add(currentPlayerIndex);
        }

        private IEnumerator WaitPlayerSelectTarget()
        {
            //update infos
            CardGameUIManager.instance.UpdateInfoLabel("Select one player to attack...");

            int selectedPlayerIndex = -1;

            while (true)
            {
                foreach (var keypair in CardGameUIManager.instance.playersInScene)
                    keypair.Value.onClickSelect += OnSelectPlayer;

                //wait user to select one target and show his cards
                selectedPlayerIndex = -1;
                yield return new WaitUntil(() => selectedPlayerIndex > -1);
                ShowPlayerCards(selectedPlayerIndex);

                foreach (var keypair in CardGameUIManager.instance.playersInScene)
                    keypair.Value.onClickSelect -= OnSelectPlayer;

                //wait user to click on popup
                byte popupSelection = 0;
                CardGameUIManager.instance.ShowPopup(true, onClickYes: () => popupSelection = 1, onClickNo: () => popupSelection = 2);
                yield return new WaitUntil(() => popupSelection > 0);

                //if pressed yes, complete coroutine
                if (popupSelection == 1)
                {
                    selectedPlayers.Add(selectedPlayerIndex);
                    break;
                }
            }

            void OnSelectPlayer(PlayerUI playerUI, int clickedPlayerIndex)
            {
                //be sure player can be selected
                int currentPlayerIndex = CardGameManager.instance.currentPlayer;
                if (CanSelectPlayer(currentPlayerIndex, clickedPlayerIndex) == false)
                {
                    CardGameUIManager.instance.UpdateInfoLabel("Can't select this player. Please, select another player to attack...");
                    return;
                }

                //set selected player
                selectedPlayerIndex = clickedPlayerIndex;
            }
        }

        private void SelectRandomTarget()
        {
            int currentPlayerIndex = CardGameManager.instance.currentPlayer;
            List<PlayerLogic> players = CardGameManager.instance.Players;

            //add every possible target
            List<int> possiblePlayers = new List<int>();
            for (int i = 0; i < players.Count; i++)
            {
                if (CanSelectPlayer(currentPlayerIndex, i))
                    possiblePlayers.Add(i);
            }

            //select one random
            int selectedPlayerIndex = possiblePlayers[Random.Range(0, possiblePlayers.Count)];
            selectedPlayers.Add(selectedPlayerIndex);
        }

        private void SelectEveryTarget(bool canSelectLastSelection)
        {
            int currentPlayerIndex = CardGameManager.instance.currentPlayer;
            List<PlayerLogic> players = CardGameManager.instance.Players;
            PlayerLogic currentPlayer = CardGameManager.instance.GetCurrentPlayer();

            //select every possible target
            for (int i = 0; i < players.Count; i++)
            {
                //check if can select also LastSelectedTarget
                bool checkLastSelection = canSelectLastSelection || currentPlayer.LastSelectedPlayers.Contains(i) == false;

                if (CanSelectPlayer(currentPlayerIndex, i) && checkLastSelection)
                    selectedPlayers.Add(i);
            }
        }

        #endregion

        #region private utils

        private bool CanSelectPlayer(int currentPlayerIndex, int playerIndexToSelect)
        {
            //be sure isn't self and is still alive
            PlayerLogic playerToSelect = CardGameManager.instance.Players[playerIndexToSelect];
            bool checkSelf = canSelectSelf || playerIndexToSelect != currentPlayerIndex;    //check if could be self
            if (checkSelf && playerToSelect.IsAlive())
                return true;
            return false;
        }

        private void ShowPlayerCards(int attackedPlayerIndex)
        {
            //show selected player cards and bonus
            PlayerLogic attackedPlayer = CardGameManager.instance.Players[attackedPlayerIndex];
            bool attackedIsRealPlayer = CardGameManager.instance.IsRealPlayer(attackedPlayerIndex);
            CardGameUIManager.instance.SetCards(attackedIsRealPlayer, attackedPlayer.CardsInHands.ToArray(), showFront: attackedIsRealPlayer);
            CardGameUIManager.instance.SetBonus(attackedIsRealPlayer, attackedPlayer.ActiveBonus.ToArray());
            CardGameUIManager.instance.ShowAdversaryCardsAndBonus(true);
            CardGameUIManager.instance.UpdatePlayers(attackedPlayerIndex);
        }

        #endregion
    }
}