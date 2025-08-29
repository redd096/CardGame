using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Logic for the players
    /// </summary>
    [System.Serializable]
    public class PlayerLogic
    {
        /// <summary>
        /// Current cards in hands
        /// </summary>
        public List<BaseCard> CardsInHands = new List<BaseCard>();

        //Last turn blackboard

        /// <summary>
        /// Selected players index in this turn
        /// </summary>
        public List<int> LastSelectedPlayers { get; set; } = new List<int>();
        /// <summary>
        /// e.g. DrawDeckCardsFromPreviousRange -> Discards X cards and Draw the same amount of cards
        /// </summary>
        public int LastRange { get; set; }

        //Bonus

        /// <summary>
        /// This user can't receive Stop for X turns
        /// </summary>
        public int BlockStopsForTurns;
    }
}