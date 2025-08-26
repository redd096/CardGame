using redd096.Attributes;
using UnityEngine;

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

        #region editor
#if UNITY_EDITOR

        [Space]
        [SerializeField] FGenerateDeckDebug[] generateDeckCards;

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