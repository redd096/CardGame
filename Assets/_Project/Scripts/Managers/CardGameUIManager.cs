using System.Collections.Generic;
using redd096;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Manager for the UI of the CardGame
    /// </summary>
    public class CardGameUIManager : SimpleInstance<CardGameUIManager>
    {
        [SerializeField] private GameObject startLoadingPanel;
        [SerializeField] private GameObject gamePanel;
        [Space]
        [SerializeField] private PlayerUI playerPrefab;
        [SerializeField] private Transform playersContainer;
        [SerializeField] private CardUI cardPrefab;
        [SerializeField] private Transform playerCardsContainer;
        [SerializeField] private Transform adversaryCardsContainer;
        [Space]
        [SerializeField] private CardTypeColors colorCardTypes;

        private Dictionary<int, PlayerUI> playersInScene = new Dictionary<int, PlayerUI>();
        private Dictionary<BaseCard, CardUI> playerCardsInScene = new Dictionary<BaseCard, CardUI>();
        private Dictionary<BaseCard, CardUI> adversaryCardsInScene = new Dictionary<BaseCard, CardUI>();

        /// <summary>
        /// Show loading or game panel
        /// </summary>
        public void ShowPanel(bool isGamePanel)
        {
            startLoadingPanel.SetActive(isGamePanel == false);
            gamePanel.SetActive(isGamePanel);
        }

        /// <summary>
        /// Create players in scene
        /// </summary>
        public void CreatePlayers(int numberOfPlayers)
        {
            //destroy previous
            for (int i = playersContainer.childCount - 1; i >= 0; i--)
                Destroy(playersContainer.GetChild(i).gameObject);
            playersInScene.Clear();

            //and create new ones
            for (int i = 0; i < numberOfPlayers; i++)
            {
                string playerName = $"Player {i + 1}";

                PlayerUI playerUI = Instantiate(playerPrefab, playersContainer);
                playerUI.Init(i, playerName);

                playersInScene.Add(i, playerUI);
            }
        }

        /// <summary>
        /// Set cards in UI for player or adversary
        /// </summary>
        public void SetCards(bool isPlayer, BaseCard[] cards)
        {
            Transform container = isPlayer ? playerCardsContainer : adversaryCardsContainer;
            Dictionary<BaseCard, CardUI> dict = isPlayer ? playerCardsInScene : adversaryCardsInScene;

            //destroy previous
            for (int i = container.childCount - 1; i >= 0; i--)
                Destroy(container.GetChild(i).gameObject);
            dict.Clear();

            //be ure there are cards
            if (cards == null)
                return;

            //and create new ones
            for (int i = 0; i < cards.Length; i++)
            {
                BaseCard card = cards[i];
                Color color = colorCardTypes.GetColor(card.CardType);

                CardUI cardUI = Instantiate(cardPrefab, container);
                cardUI.Init(card, color);

                dict.Add(card, cardUI);
            }
        }
    }
}