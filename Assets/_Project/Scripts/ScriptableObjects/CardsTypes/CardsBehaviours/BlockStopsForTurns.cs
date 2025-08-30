using System.Collections;
using System.Collections.Generic;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players can't stop this user for X turns
    /// </summary>
    [System.Serializable]
    public class BlockStopsForTurns : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfTurns = 1;
        [ShowAssetPreview] public Sprite bonusSprite;

        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return Execute(true);
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return Execute(false);
        }

        private IEnumerator Execute(bool isRealPlayer)
        {
            //set block for x turns
            int playerIndex = CardGameManager.instance.currentPlayer;
            PlayerLogic player = CardGameManager.instance.GetCurrentPlayer();
            player.AddBonus(new BlockStopBonus(bonusSprite, NumberOfTurns, isRealPlayer, playerIndex, CardGameManager.instance.OnStartTurn));

            //update ui
            CardGameUIManager.instance.SetBonus(isRealPlayer, player.ActiveBonus.ToArray());

            //update infos
            BaseBonus bonus = player.GetBonus(typeof(BlockStopBonus));
            CardGameUIManager.instance.UpdateInfoLabel($"Player {CardGameManager.instance.currentPlayer + 1} added {NumberOfTurns} turns. Now will block the Stops for {bonus.Quantity} turns");
            yield return new WaitForSeconds(DELAY_AFTER_BEHAVIOUR);
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.Self;
        }
    }
}