using System.Collections;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players can't stop this user for X turns
    /// </summary>
    [System.Serializable]
    public class BlockStopsForTurns : BaseCardBehaviour, IBonusCard
    {
        [Space]
        [Min(1)] public int NumberOfTurns = 1;
        [ShowAssetPreview] public Sprite bonusSprite;

        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            int playerIndex = CardGameManager.instance.currentPlayer;
            PlayerLogic player = CardGameManager.instance.GetCurrentPlayer();

            //instead of put card in DiscardDecks, put in player bonus
            CardGameManager.instance.DiscardsDeck.Pop();
            player.AddBonus(card);

            //update ui
            CardGameUIManager.instance.SetBonus(isRealPlayer, player.ActiveBonus.ToArray());

            //update infos
            int quantity = player.GetBonusQuantity(typeof(BlockStopsForTurns));
            CardGameUIManager.instance.UpdateInfoLabel($"Player {playerIndex + 1} added {NumberOfTurns} turns. Now will block the Stops for {quantity} turns");
            yield return new WaitForSeconds(DELAY_AFTER_BEHAVIOUR);
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.Self;
        }

        #region bonus

        public Sprite Icon => bonusSprite;

        public void OnActivateBonus(int playerIndex)
        {
            CardGameManager.instance.OnStartTurn += (index) =>
            {
                //on start this player turn
                if (index == playerIndex)
                {
                    //decrease bonus
                    CardGameManager.instance.Players[playerIndex].RemoveBonus(typeof(BlockStopsForTurns), quantity: 1);

                    //and update ui
                    PlayerLogic player = CardGameManager.instance.Players[playerIndex];
                    bool isRealPlayer = CardGameManager.instance.IsRealPlayer(playerIndex);
                    CardGameUIManager.instance.SetBonus(isRealPlayer, player.ActiveBonus.ToArray());
                }
            };
        }

        public bool CanBeStolen()
        {
            return false;
        }

        public bool CanBeDestroyed()
        {
            return false;
        }

        public bool CanBeDiscarded()
        {
            return false;
        }

        #endregion
    }
}