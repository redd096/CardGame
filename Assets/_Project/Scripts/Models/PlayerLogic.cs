using System.Collections.Generic;

namespace cg
{
    /// <summary>
    /// Logic for the players
    /// </summary>
    [System.Serializable]
    public class PlayerLogic
    {
        /// <summary>
        /// Current cards in hands
        /// </summary>
        public List<BaseCard> CardsInHands = new List<BaseCard>();

        /// <summary>
        /// Player current active bonus
        /// </summary>
        public List<BaseCard> ActiveBonus = new List<BaseCard>();

        //Last turn blackboard

        /// <summary>
        /// Selected players index in this turn
        /// </summary>
        public List<int> LastSelectedPlayers = new List<int>();
        /// <summary>
        /// e.g. DrawDeckCardsFromPreviousRange -> Discards X cards and Draw the same amount of cards
        /// </summary>
        public int LastRange;

        /// <summary>
        /// Check has still a life
        /// </summary>
        public bool IsAlive()
        {
            return CardsInHands.Find(x => x is GenericCard genericCard && genericCard.HasBehaviour(typeof(Life))) != null
                || HasBonus(typeof(LifeExtra));
        }

        /// <summary>
        /// Check if this player has a specific bonus active
        /// </summary>
        /// <param name="type">Bonus type</param>
        /// <returns></returns>
        public bool HasBonus(System.Type type)
        {
            if (ActiveBonus != null && ActiveBonus.Count > 0)
                return ActiveBonus.Find(x => x.GetType() == type) != null;
            return false;
        }

        /// <summary>
        /// Return the count of this type of bonus
        /// </summary>
        /// <param name="type">Bonus type</param>
        /// <returns></returns>
        public int GetBonusQuantity(System.Type type)
        {
            if (HasBonus(type))
                return ActiveBonus.FindAll(x => x.GetType() == type).Count;
            return 0;
        }

        /// <summary>
        /// Add bonus to player list
        /// </summary>
        /// <param name="bonus"></param>
        public void AddBonus(BaseCard bonus)
        {
            ActiveBonus.Add(bonus);
        }

        /// <summary>
        /// Remove bonus from the list
        /// </summary>
        /// <param name="type">Bonus type</param>
        /// <param name="quantity"></param>
        public void RemoveBonus(System.Type type, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                if (HasBonus(type))
                {
                    int index = ActiveBonus.FindIndex(x => x.GetType() == type);
                    ActiveBonus.RemoveAt(index);
                }
            }
        }
    }
}