using UnityEngine;

namespace cg
{
    public abstract class BaseCard : ScriptableObject
    {
        public string CardName;
        public string Description;
        public ECardType CardType;
    }
}