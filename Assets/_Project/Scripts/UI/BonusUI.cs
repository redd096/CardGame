using UnityEngine;
using UnityEngine.UI;

namespace cg
{
    /// <summary>
    /// Script to show bonus in UI
    /// </summary>
    public class BonusUI : MonoBehaviour
    {
        [SerializeField] private Image bonusImage;
        [SerializeField] private Button selectButton;

        public System.Action<BonusUI, BaseCard> onClickSelect;
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
        /// Update graphic values
        /// </summary>
        /// <param name="bonus"></param>
        public void Init(BaseCard card)
        {
            this.card = card;
            IBonusCard bonus = card as IBonusCard;
            bonusImage.sprite = bonus.Icon;
        }
    }
}