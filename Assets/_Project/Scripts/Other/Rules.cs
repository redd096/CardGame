using UnityEngine;

namespace cg
{
    /// <summary>
    /// Rules for the CardGame
    /// </summary>
    [CreateAssetMenu(menuName = "CardGame/Rules")]
    public class Rules : ScriptableObject
    {
        [Header("When read \"+ Number of players\" it means Number of players still alive")]
        public bool AddOnlyStillActivePlayers = true;

        [Header("Cards when start game are: 5 + Number of players")]
        public int StartCards = 5;
        public bool AddPlayersToStartCards = true;

        [Header("2 of the 5 cards at the beginning are Life")]
        public int StartLife = 2;
        public bool AddPlayersToStartLife = false;

        [Header("When start the turn, draw 2 cards")]
        public int DrawCards = 2;
        public bool AddPlayersToDrawCards = false;

        [Header("At the end of the turn, discards if have more han 10 cards in hand")]
        public int MaxCardsInHand = 10;
        public bool AddPlayersToMaxCardsInHand = false;
    }
}