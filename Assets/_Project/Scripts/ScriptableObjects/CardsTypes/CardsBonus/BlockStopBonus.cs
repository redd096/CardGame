
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Bonus effect for BlockStop card
    /// </summary>
    public class BlockStopBonus : BaseBonus
    {
        public BlockStopBonus(Sprite icon, int quantity, ECardType bonusCardType, bool isRealPlayer, int playerIndex, System.Action<int> onStartTurn) : base(icon, quantity, bonusCardType)
        {
            onStartTurn += (index) =>
            {
                //on start this player turn
                if (index == playerIndex)
                {
                    //decrease bonus
                    CardGameManager.instance.Players[playerIndex].DecreaseBonus(typeof(BlockStopBonus), quantity: 1);

                    //and update ui
                    PlayerLogic player = CardGameManager.instance.Players[playerIndex];
                    CardGameUIManager.instance.SetBonus(isRealPlayer, player.ActiveBonus.ToArray());
                }
            };
        }

        public override bool CanBeStolen()
        {
            return false;
        }

        public override bool CanBeDestroyed()
        {
            return false;
        }

        public override bool CanBeDiscarded()
        {
            return false;
        }
    }
}