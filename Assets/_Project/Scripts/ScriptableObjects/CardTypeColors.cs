using System.Linq;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// used to create colors for card types
    /// </summary>
    [CreateAssetMenu(menuName = "CardGame/CardTypeColors")]
    public class CardTypeColors : ScriptableObject
    {
        [SerializeField] private FColorCardType[] colorCardTypes;

        public Color GetColor(ECardType cardType)
        {
            return colorCardTypes.FirstOrDefault(x => x.cardType == cardType).color;
        }

        [System.Serializable]
        private struct FColorCardType
        {
            public ECardType cardType;
            public Color color;
        }
    }
}