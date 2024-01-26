namespace TaskTimeout
{
    public class EmailSendInfo
    {
        public int EmailSentId { get; set; }
        public int CustomerId { get; set; }
        public string Body { get; set; }
        public List<string> Emails { get; set; }
    }
}
