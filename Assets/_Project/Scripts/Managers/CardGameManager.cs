using redd096;

namespace cg
{
    public class CardGameManager : SimpleInstance<CardGameManager>
    {
        public Rules rules;
        public Deck deck;
        
        protected override void InitializeInstance()
        {
            base.InitializeInstance();
        }
    }
}