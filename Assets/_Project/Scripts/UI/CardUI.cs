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
        [SerializeField] private Image cardImage;
        [SerializeField] private TMP_Text cardNameLabel;
        [SerializeField] private TMP_Text cardDescriptionLabel;
        [SerializeField] private Image colorTypeImage;

        private BaseCard card;

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
    }
}