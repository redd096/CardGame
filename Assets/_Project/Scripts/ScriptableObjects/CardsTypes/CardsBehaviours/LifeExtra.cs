using System.Collections;
using System.Collections.Generic;
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
        
        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return base.PlayerExecute(cardBehaviours, behaviourIndex);
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return base.AdversaryExecute(cardBehaviours, behaviourIndex);
        }
    }
}