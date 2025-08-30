
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Bonus effect for LifeExtra card
    /// </summary>
    public class LifeExtraBonus : BaseBonus
    {
        public LifeExtraBonus(Sprite icon, int quantity) : base(icon, quantity)
        {
        }

        public override bool CanBeStolen()
        {
            return false;
        }

        public override bool CanBeDestroyed()
        {
            return true;
        }

        public override bool CanBeDiscarded()
        {
            return false;
        }
    }
}