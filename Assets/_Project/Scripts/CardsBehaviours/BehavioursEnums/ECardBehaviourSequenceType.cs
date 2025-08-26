
namespace cg
{
    public enum ECardBehaviourSequenceType
    {
        // The default or unlinked state.
        // This behavior is not connected to the previous one in the list.
        // (probably this is the first or the only one in the list)
        None,

        // This behavior is linked to the previous one and should execute
        // without interfering with each other
        // (e.g. draw X cards AND steal X cards).
        And,

        // This behavior is a direct follow-up to the previous one,
        // and they should execute sequentially.
        // So if the previous behaviour can't succedee, this behaviour can't start
        // (e.g. discard X cards THEN steal X cards).
        Then
    }
}