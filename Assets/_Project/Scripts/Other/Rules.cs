using UnityEngine;

namespace cg
{
    /// <summary>
    /// Rules for the CardGame
    /// </summary>
    [CreateAssetMenu(menuName = "CardGame/Rules")]
    public class Rules : ScriptableObject
    {
        #region private API

        [Header("When read \"+ Number of players\" it means Number of players still alive")]
        [SerializeField] bool addOnlyStillActivePlayers = true;

        [Header("Cards when start game are: 5 + Number of players")]
        [SerializeField] int startCards = 5;
        [SerializeField] bool addPlayersToStartCards = true;

        [Header("2 of the 5 cards at the beginning are Life")]
        [SerializeField] int startLife = 2;
        [SerializeField] bool addPlayersToStartLife = false;

        [Header("When start the turn, draw 2 cards")]
        [SerializeField] int drawCards = 2;
        [SerializeField] bool addPlayersToDrawCards = false;

        [Header("At the end of the turn, discards if have more han 10 cards in hand")]
        [SerializeField] int maxCardsInHand = 10;
        [SerializeField] bool addPlayersToMaxCardsInHand = false;

        private int numberTotalPlayers;
        private int numberActivePlayers;

        private int ReturnCorrectValue(int value, bool addPlayers)
        {
            if (addPlayers == false)
                return value;

            int players = addOnlyStillActivePlayers ? numberActivePlayers : numberTotalPlayers;
            return value + players;
        }

        #endregion

        #region public API

        public int StartCards => ReturnCorrectValue(startCards, addPlayersToStartCards);
        public int StartLife => ReturnCorrectValue(startLife, addPlayersToStartLife);
        public int DrawCards => ReturnCorrectValue(drawCards, addPlayersToDrawCards);
        public int MaxCardsInHand => ReturnCorrectValue(maxCardsInHand, addPlayersToMaxCardsInHand);

        /// <summary>
        /// Update number of players in game
        /// </summary>
        /// <param name="numberTotalPlayers">Number of players, both dead and alive</param>
        /// <param name="numberActivePlayers">Number of players still active in game</param>
        public void UpdatePlayersCount(int numberTotalPlayers, int numberActivePlayers)
        {
            this.numberTotalPlayers = numberTotalPlayers;
            this.numberActivePlayers = numberActivePlayers;
        }

        /// <summary>
        /// Be sure variables are correct
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool IsCorrect(int numberOfPlayers, out string error)
        {
            //set starting players
            UpdatePlayersCount(numberOfPlayers, numberOfPlayers);

            if (StartLife > StartCards)
            {
                error = "Isn't possible to have more StartLife than StartCards";
                return false;
            }
            if (StartCards > MaxCardsInHand || DrawCards > MaxCardsInHand)
            {
                error = "Isn't possible to have more StartCards or DrawCards than MaxCardsInHand";
                return false;
            }

            error = "";
            return true;
        }

        #endregion
    }
}