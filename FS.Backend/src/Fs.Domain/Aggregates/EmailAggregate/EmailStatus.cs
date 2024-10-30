namespace Fs.Domain.Aggregates.EmailAggregate
{
    public enum EmailStatus
    {
        New = 1,
        ReadyToSend = 2,
        Sent = 3,
        Failed = 4
    }
}
