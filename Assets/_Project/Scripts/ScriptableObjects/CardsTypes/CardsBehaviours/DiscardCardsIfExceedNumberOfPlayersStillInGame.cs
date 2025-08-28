using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard X cards, only if there are more than X players still in game
    /// </summary>
    [System.Serializable]
    public class DiscardCardsIfExceedNumberOfPlayersStillInGame : DiscardCards
    {
        [Space]
        [Min(2)] public int ActivateWhenExceedThisNumberOfPlayerStillInGame = 2;
    }
}