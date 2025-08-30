
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

        /// <summary>
        /// When player has to select cards to steal, can steal this bonus from the playing field?
        /// </summary>
        /// <returns></returns>
        public abstract bool CanBeStolen();
        /// <summary>
        /// When player has to select cards to destroy, can destroy this bonus from the playing field?
        /// </summary>
        /// <returns></returns>
        public abstract bool CanBeDestroyed();
        /// <summary>
        /// When player has to discard cards, can instead destroy this bonus from the playing field?
        /// </summary>
        /// <returns></returns>
        public abstract bool CanBeDiscarded();
    }
}