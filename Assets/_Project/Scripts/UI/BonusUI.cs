using TMPro;
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
        [SerializeField] private TMP_Text quantityLabel;
        [SerializeField] private Button selectButton;

        public System.Action<BonusUI, BaseBonus> onClickSelect;
        private BaseBonus bonus;

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
            onClickSelect?.Invoke(this, bonus);
        }

        /// <summary>
        /// Update graphic values
        /// </summary>
        /// <param name="bonus"></param>
        public void Init(BaseBonus bonus)
        {
            this.bonus = bonus;
            bonusImage.sprite = bonus.Icon;
            quantityLabel.text = $"x{bonus.Quantity}";
        }
    }
}