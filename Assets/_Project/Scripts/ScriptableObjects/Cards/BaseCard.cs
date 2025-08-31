using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Base class for every card
    /// </summary>
    public abstract class BaseCard : ScriptableObject
    {
        public string CardName;
        public string Description;
        public ECardType CardType;
        [ShowAssetPreview] public Sprite CardSprite;
    }
}