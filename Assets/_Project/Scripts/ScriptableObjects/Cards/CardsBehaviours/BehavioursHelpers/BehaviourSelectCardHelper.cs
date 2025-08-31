using UnityEngine;

namespace cg
{
    public class BehaviourSelectCardHelper : BaseBehaviourHelper
    {
        public BehaviourSelectCardHelper(BaseCardBehaviour behaviour) : base(behaviour)
        {
            //TODO copy from DestroyPlayerCards to select cards to attack
            //NB we must add also Self attack for discard

            //as for SelectTargetHelper we must only select cards/bonus, then the card behaviour will decide what to do
            //(destroy, discard, steal)

            //NB we need to know which type of cards can select, so look to the DestroyCards overrides
            //NB we must give access to easy API -> for example for Destroy but if specific card, destroy again
            //so we want to give a list of selected card, but the possibility to call again to select other cards ???
        }
    }
}