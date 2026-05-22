namespace BankingSystem.Domain.Enums
{
    public enum TransferStatus
    {
        Pending=0,
        Processing=1,
        Completed=2,
        Failed=3,
        Reversed=4,
        Cancelled=5
    }
}