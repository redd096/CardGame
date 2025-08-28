using redd096;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Manager for shared Logic of the CardGame
    /// </summary>
    public class CardGameManager : SimpleInstance<CardGameManager>
    {
        public Rules rules;
        [SerializeField] private Deck deck;
        [Min(2)] public int numberOfPlayers = 2;

        public Deck Deck { get; set; }

        protected override void InitializeInstance()
        {
            base.InitializeInstance();

            //generate clone for runtime
            Deck = deck.GenerateCloneForRuntime();
        }

        /// <summary>
        /// Check if everything is set and correct
        /// </summary>
        public bool IsCorrect(out string error)
        {
            //check vars are setted
            if (rules == null)
            {
                error = "Missing Rules in CardGameManager";
                return false;
            }
            if (deck == null)
            {
                error = "Missing Deck in CardGameManager";
                return false;
            }
            if (Deck == null)
            {
                error = "Deck still isn't cloned for runtime, wait few seconds";
                return false;
            }

            //and everything is correct
            if (rules.IsCorrect(numberOfPlayers, out error) == false)
                return false;
            if (Deck.IsCorrect(rules.StartCards, rules.StartLife, numberOfPlayers, out error) == false)
                return false;

            return true;
        }
    }
}