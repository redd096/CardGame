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
        public List<PlayerLogic> Players { get; set; }

        private bool isInitialized;

        protected override void InitializeInstance()
        {
            base.InitializeInstance();

            //generate cards clone for runtime, and generate players
            Deck = deckEditor.GenerateCloneForRuntime();
            Players = new List<PlayerLogic>();
            for (int i = 0; i < NumberOfPlayers; i++) Players.Add(new PlayerLogic());

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
        public void DrawNextCard(int playerIndex)
        {
            BaseCard card = Deck.DrawNextCard();
            Players[playerIndex].Cards.Add(card);
        }

        /// <summary>
        /// Find a card with specific behaviour and draw it from the deck
        /// </summary>
        /// <param name="playerIndex">The player who draw the card</param>
        /// <param name="behaviour"></param>
        public void DrawCardWithSpecificBehaviour(int playerIndex, System.Type behaviour)
        {
            BaseCard card = Deck.DrawCardWithSpecificBehaviour(behaviour);
            Players[playerIndex].Cards.Add(card);
        }
    }
}