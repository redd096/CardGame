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
        public List<BaseBonus> ActiveBonus = new List<BaseBonus>();

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
                || HasBonus(typeof(LifeExtraBonus), out _);
        }

        /// <summary>
        /// Check if this player has a specific bonus active
        /// </summary>
        /// <param name="type">Bonus type</param>
        /// <returns></returns>
        public bool HasBonus(System.Type type, out int bonusIndex)
        {
            bonusIndex = -1;
            if (ActiveBonus != null && ActiveBonus.Count > 0)
                bonusIndex = ActiveBonus.FindIndex(x => x.GetType() == type);

            return bonusIndex > -1;
        }

        /// <summary>
        /// Return bonus from the player list
        /// </summary>
        /// <param name="type">Bonus type</param>
        /// <returns></returns>
        public BaseBonus GetBonus(System.Type type)
        {
            if (HasBonus(type, out int bonusIndex))
                return ActiveBonus[bonusIndex];
            return null;
        }

        /// <summary>
        /// Add bonus to player list. 
        /// If player has already this bonus, just add its quantity
        /// </summary>
        /// <param name="bonus"></param>
        public void AddBonus(BaseBonus bonus)
        {
            if (HasBonus(bonus.GetType(), out int bonusIndex))
                ActiveBonus[bonusIndex].Quantity += bonus.Quantity;
            else
                ActiveBonus.Add(bonus);
        }

        /// <summary>
        /// Decrease quantity from bonus in player list. 
        /// If the bonus reach 0, remove from the list
        /// </summary>
        /// <param name="type">Bonus type</param>
        /// <param name="quantity"></param>
        public void DecreaseBonus(System.Type type, int quantity)
        {
            if (HasBonus(type, out int bonusIndex))
            {
                ActiveBonus[bonusIndex].Quantity -= quantity;

                if (ActiveBonus[bonusIndex].Quantity <= 0)
                    ActiveBonus.RemoveAt(bonusIndex);
            }
        }
    }
}