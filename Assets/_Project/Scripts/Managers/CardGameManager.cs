using redd096;
using redd096.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Manager for shared Logic of the CardGame
    /// </summary>
    public class CardGameManager : SimpleInstance<CardGameManager>
    {
        public Rules Rules;
        [Rename("Deck")][SerializeField] private Deck deckEditor;
        [Min(2)] public int NumberOfPlayers = 2;

        public Deck Deck { get; set; }
        public List<PlayerLogic> Players { get; set; } = new List<PlayerLogic>();
        public int currentPlayer { get; private set; }
        public Stack<BaseCard> DiscardsDeck { get; set; } = new Stack<BaseCard>();

        public System.Action<int> OnStartTurn;
        public System.Action<int> OnEndTurn;

        private bool isInitialized;

        protected override void InitializeInstance()
        {
            base.InitializeInstance();

            //generate cards clone for runtime, and generate players
            Deck = deckEditor.GenerateCloneForRuntime();
            Players = new List<PlayerLogic>();
            for (int i = 0; i < NumberOfPlayers; i++)
                Players.Add(new PlayerLogic());

            isInitialized = true;
        }

        /// <summary>
        /// Check if everything is set and correct
        /// </summary>
        public bool IsCorrect(out string error)
        {
            //check vars are setted
            if (isInitialized == false)
            {
                error = "Manager still isn't initialized, wait few seconds";
                return false;
            }
            if (Rules == null)
            {
                error = "Missing Rules in CardGameManager";
                return false;
            }
            if (Deck == null)
            {
                error = "Missing Deck in CardGameManager";
                return false;
            }

            //and everything is correct
            if (Rules.IsCorrect(NumberOfPlayers, out error) == false)
                return false;
            if (Deck.IsCorrect(Rules.StartCards, Rules.StartLife, NumberOfPlayers, out error) == false)
                return false;

            return true;
        }

        /// <summary>
        /// Draw next card in the deck
        /// </summary>
        /// <param name="playerIndex">The player who draw the card</param>
        public BaseCard DrawNextCard(int playerIndex)
        {
            BaseCard card = Deck.DrawNextCard();
            Players[playerIndex].CardsInHands.Add(card);
            return card;
        }

        /// <summary>
        /// Find a card with specific behaviour and draw it from the deck
        /// </summary>
        /// <param name="playerIndex">The player who draw the card</param>
        /// <param name="behaviour"></param>
        public BaseCard DrawCardWithSpecificBehaviour(int playerIndex, System.Type behaviour)
        {
            BaseCard card = Deck.DrawCardWithSpecificBehaviour(behaviour);
            Players[playerIndex].CardsInHands.Add(card);
            return card;
        }

        /// <summary>
        /// Remove card from player hands and put in DiscardDeck
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <param name="card"></param>
        public void DiscardCard(int playerIndex, BaseCard card)
        {
            //remove card from player and add to discardsDeck
            Players[playerIndex].CardsInHands.Remove(card);
            DiscardsDeck.Push(card);
        }

        /// <summary>
        /// Start turn for next player
        /// </summary>
        public void StartNextTurn()
        {
            OnEndTurn?.Invoke(currentPlayer);   //call end turn event
            currentPlayer++;
            OnStartTurn?.Invoke(currentPlayer); //call start turn event
        }

        /// <summary>
        /// Get player playing in this turn
        /// </summary>
        public PlayerLogic GetCurrentPlayer()
        {
            return Players[currentPlayer];
        }

        /// <summary>
        /// Return if this playerIndex is the Player who is playing the game
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        public bool IsRealPlayer(int playerIndex)
        {
            return playerIndex == 0;
        }

        /// <summary>
        /// Get the player who is player the game
        /// </summary>
        /// <returns></returns>
        public PlayerLogic GetRealPlayer()
        {
            return Players[0];
        }
    }
}