using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// When someone draw this card, play it automatically. 
    /// Every player get a random number, the minus lose life
    /// </summary>
    [System.Serializable]
    public class Bomb : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberLifesToRemove = 1;

        public override bool ExecuteOnDraw()
        {
            // return base.ExecuteOnDraw();
            return true;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }

        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            List<PlayerLogic> players = CardGameManager.instance.Players;
            int numberActivePlayers = players.FindAll(x => x.IsAlive()).Count;  //only active players
            int playerIndex = CardGameManager.instance.currentPlayer;
            int loserPlayerIndex = -1;

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Player {playerIndex + 1} drawed a Bomb!");
            yield return new WaitForSeconds(2f);

            //create random queue
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < numberActivePlayers; i++)
                listNumbers.Add(i + 1);
            List<int> randomQueue = new List<int>();
            for (int i = 0; i < players.Count; i++)
            {
                //ignore not active players
                if (players[i].IsAlive() == false)
                    continue;

                int randomindex = Random.Range(0, listNumbers.Count);
                int value = listNumbers[randomindex];
                randomQueue.Add(value);
                listNumbers.RemoveAt(randomindex);

                //who got the first number (the lower) is the loser
                if (randomindex == 0)
                    loserPlayerIndex = i;
            }

            //every player get a random number
            for (int i = 0; i < players.Count; i++)
            {
                //ignore not active players
                if (players[i].IsAlive() == false)
                    continue;

                //update infos
                CardGameUIManager.instance.UpdateInfoLabel($"Player {i + 1} threw a dice and got {randomQueue[i]}");
                yield return new WaitForSeconds(1f);

            }

            //the lower, lose life
            PlayerLogic loserPlayer = players[loserPlayerIndex];
            bool loserIsRealPlayer = CardGameManager.instance.IsRealPlayer(loserPlayerIndex);

            for (int i = 0; i < NumberLifesToRemove; i++)
            {
                //if has extra lifes, remove it
                if (loserPlayer.HasBonus(typeof(LifeExtra)))
                {
                    loserPlayer.RemoveBonus(typeof(LifeExtra), 1);
                }
                //else, remove life card
                else
                {
                    BaseCard lifeCard = loserPlayer.CardsInHands.Find(x => x is GenericCard genericCard && genericCard.HasBehaviour(typeof(Life)));
                    CardGameManager.instance.DiscardCard(loserPlayerIndex, lifeCard);
                }

                //update ui
                if (loserIsRealPlayer)
                {
                    CardGameUIManager.instance.SetBonus(isRealPlayer: true, loserPlayer.ActiveBonus.ToArray());
                    CardGameUIManager.instance.SetCards(isRealPlayer: true, loserPlayer.CardsInHands.ToArray());
                    yield return new WaitForSeconds(1f);
                }

                //if dead, doesn't need to continue remove lifes
                if (loserPlayer.IsAlive() == false)
                    break;
            }

            //update ui
            CardGameUIManager.instance.UpdatePlayers(loserPlayerIndex);

            //update infos
            CardGameUIManager.instance.UpdateInfoLabel($"Player {loserPlayerIndex + 1} lost {NumberLifesToRemove} lifes");
            if (loserPlayer.IsAlive() == false)
            {
                yield return new WaitForSeconds(LOW_DELAY);
                CardGameUIManager.instance.UpdateInfoLabel($"Player {loserPlayerIndex + 1} is dead");
            }
            yield return new WaitForSeconds(DELAY_AFTER_BEHAVIOUR);
        }
    }
}