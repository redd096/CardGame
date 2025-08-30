using System.Collections;
using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Base class for Card Behaviours
    /// </summary>
    [System.Serializable]
    public abstract class BaseCardBehaviour
    {
        public const float DELAY_AFTER_BEHAVIOUR = 2f;
        public const float LOW_DELAY = 1f;
        public const float BIG_DELAY = 2f;

        public ECardBehaviourSequenceType SequenceType;

        public abstract IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex);
        public abstract IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex);
        public abstract EGenericTarget GetGenericTargetCard();

        /// <summary>
        /// Execute on Draw instead of when player/adversary select it
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteOnDraw()
        {
            return false;
        }

        /// <summary>
        /// Does this behaviour have to be merged with previous behaviour? 
        /// (e.g. GiveCardsToPlayer is merged -> Steal 1 card and Give 1 card, then the same to another player, and so on...)
        /// </summary>
        public bool MergeWithPreviousBehaviour(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            //get previous behaviour
            BaseCardBehaviour otherBehaviour = null;
            int index = behaviourIndex - 1;
            if (index >= 0 && cardBehaviours.Count > index)
                otherBehaviour = cardBehaviours[index];

            return MergeWithPreviousBehaviour(cardBehaviours, behaviourIndex, otherBehaviour);
        }
        /// <summary>
        /// Does this behaviour have to be merged with previous behaviour? 
        /// (e.g. GiveCardsToPlayer is merged -> Steal 1 card and Give 1 card, then the same to another player, and so on...)
        /// </summary>
        protected virtual bool MergeWithPreviousBehaviour(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex, BaseCardBehaviour previousBehaviour)
        {
            return false;
        }

        
        /// <summary>
        /// Does this behaviour have to be merged with next behaviour? 
        /// (e.g. LookPlayerCards is merged -> Look 2 cards and Steal 1 card, then the same to another player, and so on...)
        /// </summary>
        public bool MergeWithNextBehaviour(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            //get next behaviour
            BaseCardBehaviour otherBehaviour = null;
            int index = behaviourIndex + 1;
            if (index >= 0 && cardBehaviours.Count > index)
                otherBehaviour = cardBehaviours[index];

            return MergeWithNextBehaviour(cardBehaviours, behaviourIndex, otherBehaviour);
        }
        /// <summary>
        /// Does this behaviour have to be merged with next behaviour? 
        /// (e.g. LookPlayerCards is merged -> Look 2 cards and Steal 1 card, then the same to another player, and so on...)
        /// </summary>
        protected virtual bool MergeWithNextBehaviour(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex, BaseCardBehaviour nextBehaviour)
        {
            return false;
        }
    }
}