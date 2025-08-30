using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cg
{
    /// <summary>
    /// Script to show on UI a Card
    /// </summary>
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private GameObject backCard;
        [SerializeField] private GameObject frontCard;
        [Space]
        [SerializeField] private Image cardImage;
        [SerializeField] private TMP_Text cardNameLabel;
        [SerializeField] private TMP_Text cardDescriptionLabel;
        [SerializeField] private Image colorTypeImage;
        [SerializeField] private Button selectButton;

        public System.Action<CardUI, BaseCard> onClickSelect;
        private BaseCard card;

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
            onClickSelect?.Invoke(this, card);
        }

        /// <summary>
        /// Set graphic values
        /// </summary>
        public void Init(BaseCard card, Color color)
        {
            this.card = card;

            cardImage.sprite = card.CardSprite;
            cardNameLabel.text = card.CardName;
            cardDescriptionLabel.text = card.Description;
            colorTypeImage.color = color;
        }

        /// <summary>
        /// Show front or the back of the card
        /// </summary>
        /// <param name="showFront"></param>
        public void ShowFrontOrBack(bool showFront)
        {
            frontCard.SetActive(showFront);
            backCard.SetActive(showFront == false);
        }
    }
}