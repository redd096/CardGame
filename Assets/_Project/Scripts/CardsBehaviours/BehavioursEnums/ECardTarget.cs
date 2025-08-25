
namespace cg
{
    public enum ETargetCard
    {
        /// <summary>
        /// Select one single player to attack
        /// </summary>
        ChooseOnePlayer,
        /// <summary>
        /// Do the card behaviour (e.g. steal cards) to every adversary player
        /// </summary>
        EveryPlayer,
        /// <summary>
        /// Do the card behaviour (e.g. steal cards) to every adversary player, 
        /// except the one choosed by a previous behaviour
        /// (e.g. Steal X Cards to a ChooseOnePlayer, then Destroy X Cards to EveryPlayerExceptPreviouslyChoosedOne)
        /// Note: if there isn't a previous choosed one, this is the same as EveryPlayer
        /// </summary>
        EveryPlayerExceptPreviouslyChoosedOne,
        /// <summary>
        /// Do the card behaviour to self (e.g. discard cards)
        /// </summary>
        Self,
    }
}