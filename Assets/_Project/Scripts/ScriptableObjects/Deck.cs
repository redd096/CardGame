using redd096.Attributes;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace cg
{
    /// <summary>
    /// Deck of cards
    /// </summary>
    [CreateAssetMenu(menuName = "CardGame/Deck")]
    public class Deck : ScriptableObject
    {
        public List<BaseCard> Cards = new List<BaseCard>();
        public bool ShuffleOnStartGame;

        /// <summary>
        /// Be sure there are at least cards to start the game
        /// </summary>
        public bool IsCorrect(int startCardsForPlayer, int startLifeCardsForPlayer, int numberOfPlayers, out string error)
        {
            if (startCardsForPlayer * numberOfPlayers > Cards.Count)
            {
                error = "There aren't enough cards in deck";
                return false;
            }
            int lifeCount = Cards.FindAll(x => x is GenericCard genericCard && genericCard.HasBehaviour(typeof(Life))).Count();
            if (startLifeCardsForPlayer * numberOfPlayers > lifeCount)
            {
                error = "There aren't enough Life cards in deck";
                return false;
            }

            error = "";
            return true;
        }

        /// <summary>
        /// Generate a copy of the Deck and of every Card, to avoid edit scriptable objects at runtime
        /// </summary>
        public Deck GenerateCloneForRuntime()
        {
            Deck clone = Instantiate(this);
            for (int i = 0; i < Cards.Count; i++)
            {
                clone.Cards[i] = Instantiate(Cards[i]);
            }
            return clone;
        }

        /// <summary>
        /// Shuffle cards inside the deck
        /// </summary>
        public void ShuffleDeck()
        {
            // Use a simple and effective Fisher-Yates shuffle algorithm
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                BaseCard temp = Cards[i];
                Cards[i] = Cards[randomIndex];
                Cards[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Draw next card in the deck
        /// </summary>
        /// <returns></returns>
        public BaseCard DrawNextCard()
        {
            BaseCard card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Find a card with specific behaviour and draw it from the deck
        /// </summary>
        /// <param name="behaviour"></param>
        /// <returns></returns>
        public BaseCard DrawCardWithSpecificBehaviour(System.Type behaviour)
        {
            int index = Cards.FindIndex(x => x is GenericCard genericCard && genericCard.HasBehaviour(behaviour));
            BaseCard card = Cards[index];
            Cards.RemoveAt(index);
            return card;
        }

        #region editor
#if UNITY_EDITOR

        [Space]
        [ReadOnly][SerializeField] private int numberOfStealCards;
        [ReadOnly][SerializeField] private int numberOfDestroyCards;
        [ReadOnly][SerializeField] private int numberOfDiscardCards;
        [ReadOnly][SerializeField] private int numberOfNormalCards;
        [ReadOnly][SerializeField] private int numberOfLifeCards;
        [ReadOnly][SerializeField] private int numberOfStopCards;
        [Space]
        [ReadOnly][SerializeField] private int specificLifeCards;
        [ReadOnly][SerializeField] private int specificBombCards;
        [ReadOnly][SerializeField] private int specificSwapHandsCards;

        private void OnValidate()
        {
            if (Cards != null && Cards.Count > 0)
            {
                numberOfStealCards = Cards.FindAll(x => x.CardType == ECardType.Steal).Count();
                numberOfDestroyCards = Cards.FindAll(x => x.CardType == ECardType.Destroy).Count();
                numberOfDiscardCards = Cards.FindAll(x => x.CardType == ECardType.Discard).Count();
                numberOfNormalCards = Cards.FindAll(x => x.CardType == ECardType.Normal).Count();
                numberOfLifeCards = Cards.FindAll(x => x.CardType == ECardType.Life).Count();
                numberOfStopCards = Cards.FindAll(x => x.CardType == ECardType.Stop).Count();

                specificLifeCards = Cards.FindAll(x => x is GenericCard genericCard && genericCard.HasBehaviour(typeof(Life))).Count();
                specificBombCards = Cards.FindAll(x => x is GenericCard genericCard && genericCard.HasBehaviour(typeof(Bomb))).Count();
                specificSwapHandsCards = Cards.FindAll(x => x is GenericCard genericCard && genericCard.HasBehaviour(typeof(SwapHands))).Count();
            }
        }

        [Space]
        [SerializeField] private FGenerateDeckDebug[] generateDeckCards;

        [System.Serializable]
        public struct FGenerateDeckDebug
        {
            public BaseCard Card;
            public int Quantity;
        }

        [Button("Generate Deck")]
        void GenerateDeckEditor()
        {
            //int length = 0;
            //foreach (var v in generateDeckCards)
            //    length += v.Quantity;
            //Cards = new BaseCard[length];

            Cards = new List<BaseCard>();
            //foreach card
            foreach (var v in generateDeckCards)
            {
                //add x quantity
                for (int i = 0; i < v.Quantity; i++)
                {
                    Cards.Add(v.Card);
                }
            }

            // Mark the object as dirty so Unity saves the changes
            OnValidate();
            EditorUtility.SetDirty(this);
        }

        [Button("Shuffle Deck")]
        void ShuffleDeckEditor()
        {
            //shuffle
            ShuffleDeck();

            // Mark the object as dirty so Unity saves the changes
            OnValidate();
            EditorUtility.SetDirty(this);
        }

#endif
        #endregion
    }
}