
namespace cg
{
    public class BaseBehaviourHelper
    {
        protected BaseCardBehaviour behaviour;
        protected EGenericTarget target => behaviour.GetGenericTargetCard();

        public BaseBehaviourHelper(BaseCardBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        protected void ShowPlayerCards(int attackedPlayerIndex)
        {
            //show selected player cards and bonus
            PlayerLogic attackedPlayer = CardGameManager.instance.Players[attackedPlayerIndex];
            bool attackedIsRealPlayer = CardGameManager.instance.IsRealPlayer(attackedPlayerIndex);
            CardGameUIManager.instance.SetCards(attackedIsRealPlayer, attackedPlayer.CardsInHands.ToArray(), showFront: attackedIsRealPlayer);
            CardGameUIManager.instance.SetBonus(attackedIsRealPlayer, attackedPlayer.ActiveBonus.ToArray());
            CardGameUIManager.instance.ShowAdversaryCardsAndBonus(attackedIsRealPlayer == false);
            CardGameUIManager.instance.UpdatePlayers(attackedPlayerIndex);
        }
    }
}