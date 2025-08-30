using System.Collections.Generic;
using redd096;
using TMPro;
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
        [SerializeField] private BonusUI bonusPrefab;
        [SerializeField] private Transform playerCardsContainer;
        [SerializeField] private Transform adversaryCardsContainer;
        [SerializeField] private GameObject adversaryCardsObj;
        [SerializeField] private TMP_Text infoLabel;
        [Space]
        [SerializeField] private CardTypeColors colorCardTypes;

        private Dictionary<int, PlayerUI> playersInScene = new Dictionary<int, PlayerUI>();
        private Dictionary<BaseCard, CardUI> playerCardsInScene = new Dictionary<BaseCard, CardUI>();
        private Dictionary<BaseCard, CardUI> adversaryCardsInScene = new Dictionary<BaseCard, CardUI>();
        private Dictionary<BaseBonus, BonusUI> playerBonusInScene = new Dictionary<BaseBonus, BonusUI>();
        private Dictionary<BaseBonus, BonusUI> adversaryBonusInScene = new Dictionary<BaseBonus, BonusUI>();

        protected override void InitializeInstance()
        {
            base.InitializeInstance();

            //remove placeholders
            for (int i = playerCardsContainer.childCount - 1; i >= 0; i--)
                Destroy(playerCardsContainer.GetChild(i).gameObject);
            for (int i = adversaryCardsContainer.childCount - 1; i >= 0; i--)
                Destroy(adversaryCardsContainer.GetChild(i).gameObject);

            //reset ui
            CreatePlayers(CardGameManager.instance.NumberOfPlayers);
            SetCards(true, null);
            SetBonus(true, null);
            SetCards(false, null);
            SetBonus(false, null);
            ShowAdversaryCards(false);
            UpdateInfoLabel("");
        }

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
        /// Update players UI (playerTurn, players alive, etc...)
        /// </summary>
        public void UpdatePlayers(int selectedPlayerIndex = -1)
        {
            List<PlayerLogic> players = CardGameManager.instance.Players;
            int playerTurn = CardGameManager.instance.currentPlayer;

            foreach (var keypair in playersInScene)
            {
                int playerIndex = keypair.Key;
                PlayerLogic player = players[playerIndex];
                PlayerUI playerUI = keypair.Value;

                playerUI.SetIsActive(player.IsAlive());
                playerUI.SetIsPlayerTurn(playerTurn == playerIndex);
                playerUI.SetSelected(selectedPlayerIndex == playerIndex);
            }
        }

        /// <summary>
        /// Set cards in UI for player or adversary
        /// </summary>
        public Dictionary<BaseCard, CardUI> SetCards(bool isRealPlayer, BaseCard[] cards)
        {
            Transform container = isRealPlayer ? playerCardsContainer : adversaryCardsContainer;
            Dictionary<BaseCard, CardUI> dict = isRealPlayer ? playerCardsInScene : adversaryCardsInScene;

            //destroy previous
            foreach (var keypair in dict)
                Destroy(keypair.Value.gameObject);
            dict.Clear();

            //be sure there are cards
            if (cards == null)
                return dict;

            //and create new ones
            for (int i = 0; i < cards.Length; i++)
            {
                BaseCard card = cards[i];
                Color color = colorCardTypes.GetColor(card.CardType);

                CardUI cardUI = Instantiate(cardPrefab, container);
                cardUI.Init(card, color);

                dict.Add(card, cardUI);
            }

            return dict;
        }

        /// <summary>
        /// Set bonus in UI for player or adversary
        /// </summary>
        public Dictionary<BaseBonus, BonusUI> SetBonus(bool isRealPlayer, BaseBonus[] bonusList)
        {
            Transform container = isRealPlayer ? playerCardsContainer : adversaryCardsContainer;
            Dictionary<BaseBonus, BonusUI> dict = isRealPlayer ? playerBonusInScene : adversaryBonusInScene;

            //destroy previous
            foreach (var keypair in dict)
                Destroy(keypair.Value.gameObject);
            dict.Clear();

            //be sure there are bonus
            if (bonusList == null)
                return dict;

            //and create new ones
            for (int i = 0; i < bonusList.Length; i++)
            {
                BaseBonus bonus = bonusList[i];

                BonusUI bonusUI = Instantiate(bonusPrefab, container);
                bonusUI.Init(bonus);

                dict.Add(bonus, bonusUI);
            }

            return dict;
        }

        /// <summary>
        /// Show or hide adversary cards
        /// </summary>
        public void ShowAdversaryCards(bool show)
        {
            adversaryCardsObj.SetActive(show);
        }

        /// <summary>
        /// Show info for player on what's happening or what to do
        /// </summary>
        /// <param name="text"></param>
        public void UpdateInfoLabel(string text)
        {
            infoLabel.text = text;
        }
    }
}