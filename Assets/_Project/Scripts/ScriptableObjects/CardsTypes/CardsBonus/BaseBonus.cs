
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Base class for every card bonus
    /// </summary>
    public abstract class BaseBonus
    {
        public Sprite Icon;
        public int Quantity;

        public BaseBonus(Sprite icon, int quantity)
        {
            Icon = icon;
            Quantity = quantity;
        }
    }
}