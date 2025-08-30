using UnityEngine;

namespace cg
{
    /// <summary>
    /// Interface to add to BaseCard for activate bonus in scene (e.g. LifeExtra to put an extra life in scene when played)
    /// </summary>
    public interface IBonusCard
    {
        Sprite Icon { get; }

        /// <summary>
        /// Called when activate the bonus (when play the card)
        /// </summary>
        /// <param name="playerIndex">The player who played this card</param>
        void OnActivateBonus(int playerIndex);
        /// <summary>
        /// When player has to select cards to steal, can steal this bonus from the playing field?
        /// </summary>
        /// <returns></returns>
        bool CanBeStolen();
        /// <summary>
        /// When player has to select cards to destroy, can destroy this bonus from the playing field?
        /// </summary>
        /// <returns></returns>
        bool CanBeDestroyed();
        /// <summary>
        /// When player has to discard cards, can instead destroy this bonus from the playing field?
        /// </summary>
        /// <returns></returns>
        bool CanBeDiscarded();
    }
}