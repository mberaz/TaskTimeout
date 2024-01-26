namespace TaskTimeout
{
    public class EmailSender
    {
        public static Task SendEmail(EmailSendInfo emailSendInfo, string email)
        {
            Console.WriteLine($"sending email {email}");
            return Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}
