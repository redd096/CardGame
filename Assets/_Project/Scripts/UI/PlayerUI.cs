using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cg
{
    /// <summary>
    /// Script to show Players on UI
    /// </summary>
    public class PlayerUI : MonoBehaviour
    {
        //show active obj (with selection and so on...), or deactive obj (dark background)
        [SerializeField] GameObject activeObj;
        [SerializeField] GameObject deactiveObj;
        [SerializeField] TMP_Text[] playerNameLabels;

        [Header("Active")]
        //when active show if this is its turn or its selected from another player
        [SerializeField] Button selectButton;
        [SerializeField] GameObject playerTurnObj;
        [SerializeField] GameObject selectedObj;

        public System.Action<PlayerUI, int> onClickSelect;
        private int playerIndex;

        void Awake()
        {
            selectButton.onClick.AddListener(OnClickSelect);
        }

        void OnDestroy()
        {
            selectButton.onClick.RemoveListener(OnClickSelect);
        }

        private void OnClickSelect()
        {
            onClickSelect?.Invoke(this, playerIndex);
        }

        /// <summary>
        /// Set name and default graphic. And set player index for game logics
        /// </summary>
        public void Init(int playerIndex, string playerName)
        {
            this.playerIndex = playerIndex;

            foreach (var playerNameLabel in playerNameLabels)
                playerNameLabel.text = playerName;

            SetSelected(false);
            SetIsPlayerTurn(false);
            SetIsActive(true);
        }

        public void SetSelected(bool selected)
        {
            selectedObj.SetActive(selected);
        }

        public void SetIsPlayerTurn(bool isPlayerTurn)
        {
            playerTurnObj.SetActive(isPlayerTurn);
        }

        public void SetIsActive(bool isActive)
        {
            activeObj.SetActive(isActive);
            deactiveObj.SetActive(isActive == false);
        }
    }
}