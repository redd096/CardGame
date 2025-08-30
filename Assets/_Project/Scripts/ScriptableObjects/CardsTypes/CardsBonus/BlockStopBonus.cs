
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Bonus effect for BlockStop card
    /// </summary>
    public class BlockStopBonus : BaseBonus
    {
        public BlockStopBonus(Sprite icon, int quantity, bool isPlayer, int playerIndex, System.Action<int> onStartTurn) : base(icon, quantity)
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
                    CardGameUIManager.instance.SetBonus(isPlayer, player.ActiveBonus.ToArray());
                }
            };
        }
    }
}