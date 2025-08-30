using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// A Life card, but only when player consume one action to put it on the table
    /// </summary>
    [System.Serializable]
    public class LifeExtra : Life
    {
        [Space]
        [ShowAssetPreview] public Sprite bonusSprite;
    }
}