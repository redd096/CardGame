
namespace cg
{
    public enum EDiscardTarget
    {
        /// <summary>
        /// Do the card behaviour to self (e.g. discard cards)
        /// </summary>
        Self,
        /// <summary>
        /// Do the card behaviour to every adversary player and to self (everyone must discard cards)
        /// </summary>
        EveryPlayer,
        /// <summary>
        /// Do the card behaviour to every adversary player and to self (everyone must discard cards), 
        /// except the one choosed by a previous behaviour
        /// (e.g. Steal X Cards to a ChooseOnePlayer, then Discard X Cards to EveryPlayerExceptPreviouslyChoosedOne)
        /// Note: if there isn't a previous choosed one, this is the same as EveryPlayer
        /// </summary>
        EveryPlayerExceptPreviouslyChoosedOne,

        //Next are the same as ECardTarget

        /// <summary>
        /// Select one single player to attack (e.g. steal card to him)
        /// </summary>
        ChooseOnePlayer,
        /// <summary>
        /// Do the card behaviour to every adversary player NOT SELF (e.g. steal cards to every adversary player)
        /// </summary>
        EveryOtherPlayer,
        /// <summary>
        /// Do the card behaviour to every adversary player NOT SELF (e.g. steal cards to every adversary player), 
        /// except the one choosed by a previous behaviour
        /// (e.g. Steal X Cards to a ChooseOnePlayer, then Destroy X Cards to EveryPlayerExceptPreviouslyChoosedOne)
        /// Note: if there isn't a previous choosed one, this is the same as EveryPlayer
        /// </summary>
        EveryOtherPlayerExceptPreviouslyChoosedOne,
    }
}