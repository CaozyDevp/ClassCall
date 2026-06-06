namespace ClassCall.Core.Messages
{
    public class ConfirmMessage : MessageBase<ConfirmMessage>
    {
        public long TimeStamp { get; set; }

        public int ReceivedMessageId { get; set; }

        public string Classroom { get; set; }
    }
}