using System;
using System.Collections.Generic;
using redd096;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [Space]
        [SerializeField] private BonusUI bonusPrefab;
        [SerializeField] private Transform playerBonusContainer;
        [SerializeField] private Transform adversaryBonusContainer;
        [Space]
        [SerializeField] private CardUI cardPrefab;
        [SerializeField] private Transform playerCardsContainer;
        [SerializeField] private Transform adversaryCardsContainer;
        [Space]
        [SerializeField] private GameObject adversaryObj;
        [SerializeField] private TMP_Text infoLabel;
        [SerializeField] private CardTypeColors colorCardTypes;
        [Space]
        [SerializeField] private GameObject popupObj;
        [SerializeField] private Button noButton;
        [SerializeField] private Button yesButton;

        public Dictionary<int, PlayerUI> playersInScene = new Dictionary<int, PlayerUI>();
        public Dictionary<BaseCard, CardUI> playerCardsInScene = new Dictionary<BaseCard, CardUI>();
        public Dictionary<BaseCard, CardUI> adversaryCardsInScene = new Dictionary<BaseCard, CardUI>();
        public Dictionary<BaseCard, BonusUI> playerBonusInScene = new Dictionary<BaseCard, BonusUI>();
        public Dictionary<BaseCard, BonusUI> adversaryBonusInScene = new Dictionary<BaseCard, BonusUI>();

        protected override void InitializeInstance()
        {
            base.InitializeInstance();

            //reset ui
            CreatePlayers(CardGameManager.instance.NumberOfPlayers);
            SetCards(true, null);
            SetBonus(true, null);
            SetCards(false, null);
            SetBonus(false, null);
            ShowAdversaryCardsAndBonus(false);
            UpdateInfoLabel("");

            //register events
            yesButton.onClick.AddListener(OnClickYesPopup);
            noButton.onClick.AddListener(OnClickNoPopup);
        }

        private void ODestroy()
        {
            //unregister events
            yesButton.onClick.RemoveListener(OnClickYesPopup);
            noButton.onClick.RemoveListener(OnClickNoPopup);
        }

        #region events

        private void OnClickYesPopup()
        {
            //TODO fare una funzione per accendere il popup e registrare 2 eventi per sì e no
        }

        private void OnClickNoPopup()
        {
        }

        #endregion

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
        public Dictionary<BaseCard, CardUI> SetCards(bool isRealPlayer, BaseCard[] cards, bool showFront = true)
        {
            Transform container = isRealPlayer ? playerCardsContainer : adversaryCardsContainer;
            Dictionary<BaseCard, CardUI> dict = isRealPlayer ? playerCardsInScene : adversaryCardsInScene;

            //destroy previous
            for (int i = container.childCount - 1; i >= 0; i--)
                Destroy(container.GetChild(i).gameObject);
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
                cardUI.ShowFrontOrBack(showFront);

                dict.Add(card, cardUI);
            }

            return dict;
        }

        /// <summary>
        /// Set bonus in UI for player or adversary
        /// </summary>
        public Dictionary<BaseCard, BonusUI> SetBonus(bool isRealPlayer, BaseCard[] bonusList)
        {
            Transform container = isRealPlayer ? playerBonusContainer : adversaryBonusContainer;
            Dictionary<BaseCard, BonusUI> dict = isRealPlayer ? playerBonusInScene : adversaryBonusInScene;

            //destroy previous
            for (int i = container.childCount - 1; i >= 0; i--)
                Destroy(container.GetChild(i).gameObject);
            dict.Clear();

            //be sure there are bonus
            if (bonusList == null)
            {
                container.gameObject.SetActive(dict.Count > 0); //show only if there are bonus
                return dict;
            }

            //and create new ones
            for (int i = 0; i < bonusList.Length; i++)
            {
                BaseCard bonus = bonusList[i];

                BonusUI bonusUI = Instantiate(bonusPrefab, container);
                bonusUI.Init(bonus);

                dict.Add(bonus, bonusUI);
            }

            container.gameObject.SetActive(dict.Count > 0); //show only if there are bonus
            return dict;
        }

        /// <summary>
        /// Show or hide adversary cards
        /// </summary>
        public void ShowAdversaryCardsAndBonus(bool show)
        {
            adversaryObj.SetActive(show);
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