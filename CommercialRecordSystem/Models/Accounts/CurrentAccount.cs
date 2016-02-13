
namespace CommercialRecordSystem.Models.Accounts
{
    class CurrentAccount : AccountBase
    {
        public const int
            TYPE_DEBT = 0,
            TYPE_RECEIVABLE = 1,
            TYPE_CHECK = 2;

        public int ActorId { get; set; }
    }
}
