using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public ETargetCard TargetCard;

        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            int playerIndex = CardGameManager.instance.currentPlayer;
            PlayerLogic player = CardGameManager.instance.GetCurrentPlayer();

            switch (TargetCard)
            {
                case ETargetCard.ChooseOnePlayer:
                    //update infos
                    CardGameUIManager.instance.UpdateInfoLabel("Select one player to attack");

                    //register to buttons events and wait player to click it
                    
                    break;
                case ETargetCard.EveryOtherPlayer:
                    break;
                case ETargetCard.EveryOtherPlayerExceptPreviouslyChoosedOne:
                    break;
            }
            yield return null;
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }
    }
}