using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// A Life card, but only when player consume one action to put it on the table
    /// </summary>
    [System.Serializable]
    public class LifeExtra : Life, IBonusCard
    {
        [Space]
        [ShowAssetPreview] public Sprite bonusSprite;

        #region bonus

        public Sprite Icon => bonusSprite;

        public void OnActivateBonus(int playerIndex)
        {
        }

        public bool CanBeStolen()
        {
            return true;
        }

        public bool CanBeDestroyed()
        {
            return true;
        }

        public bool CanBeDiscarded()
        {
            return true;
        }

        #endregion
    }
}