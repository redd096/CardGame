using UnityEngine;

namespace cg
{
    /// <summary>
    /// Select X cards of the specified type in the deck, and draw them
    /// </summary>
    [System.Serializable]
    public class DrawDeckSpecificCardType : DrawDeckCards
    {
        [Space]
        public ECardType TypeCardsToDraw;
    }
}