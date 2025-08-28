using redd096.Attributes;
using UnityEngine;
using System.Linq;

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
        public BaseCard[] Cards;
        public bool ShuffleOnStartGame;

        /// <summary>
        /// Be sure there are at least cards to start the game
        /// </summary>
        public bool IsCorrect(int startCardsForPlayer, int startLifeCardsForPlayer, int numberOfPlayers, out string error)
        {
            if (startCardsForPlayer * numberOfPlayers > Cards.Length)
            {
                error = "There aren't enough cards in deck";
                return false;
            }
            int lifeCount = Cards.Where(x => x is GenericCard genericCard && genericCard.CardsBehaviours.Behaviours.Find(y => y.GetType() == typeof(Life)) != null).Count();
            if (startLifeCardsForPlayer * numberOfPlayers > lifeCount)
            {
                error = "There aren't enough Life cards in deck";
                return false;
            }

            error = "";
            return true;
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
            numberOfStealCards = Cards.Where(x => x.CardType == ECardType.Steal).Count();
            numberOfDestroyCards = Cards.Where(x => x.CardType == ECardType.Destroy).Count();
            numberOfDiscardCards = Cards.Where(x => x.CardType == ECardType.Discard).Count();
            numberOfNormalCards = Cards.Where(x => x.CardType == ECardType.Normal).Count();
            numberOfLifeCards = Cards.Where(x => x.CardType == ECardType.Life).Count();
            numberOfStopCards = Cards.Where(x => x.CardType == ECardType.Stop).Count();

            specificLifeCards = Cards.Where(x => x is GenericCard genericCard && genericCard.CardsBehaviours.Behaviours.Find(y => y.GetType() == typeof(Life)) != null).Count();
            specificBombCards = Cards.Where(x => x is GenericCard genericCard && genericCard.CardsBehaviours.Behaviours.Find(y => y.GetType() == typeof(Bomb)) != null).Count();
            specificSwapHandsCards = Cards.Where(x => x is GenericCard genericCard && genericCard.CardsBehaviours.Behaviours.Find(y => y.GetType() == typeof(SwapHands)) != null).Count();
        }

        [Space]
        [SerializeField] private FGenerateDeckDebug[] generateDeckCards;

        [System.Serializable]
        public struct FGenerateDeckDebug
        {
            public BaseCard Card;
            public int Quantity;
        }

        [Button]
        void GenerateDeck()
        {
            int length = 0;
            foreach (var v in generateDeckCards)
                length += v.Quantity;

            Cards = new BaseCard[length];
            int counter = 0;
            //foreach card
            foreach (var v in generateDeckCards)
            {
                //add x quantity
                for (int i = 0; i < v.Quantity; i++)
                {
                    Cards[counter] = v.Card;
                    counter++;
                }
            }
        }

        [Button]
        void ShuffleDeck()
        {
            // Use a simple and effective Fisher-Yates shuffle algorithm
            for (int i = Cards.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                BaseCard temp = Cards[i];
                Cards[i] = Cards[randomIndex];
                Cards[randomIndex] = temp;
            }

            // Mark the object as dirty so Unity saves the changes
            EditorUtility.SetDirty(this);
        }

#endif
        #endregion
    }
}