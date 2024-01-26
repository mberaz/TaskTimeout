
using System.Diagnostics;
using TaskTimeout;

Console.WriteLine("Hello, World!");

var emailSend = new EmailSendInfo
{
    Body = "This is an email",
    CustomerId = 1,
    EmailSentId = 1,
    Emails = new()
    {
         "1@gmail.com",
         "2@gmail.com",
         "3@gmail.com",
         "4@gmail.com",
         "5@gmail.com",
         "6@gmail.com",
         "7@gmail.com",
         "7@gmail.com",
         "9@gmail.com",
     }
};

const int batchSize = 3;

var cts = new CancellationTokenSource();
cts.CancelAfter( TimeSpan.FromSeconds(5));

var stopWatch = new Stopwatch();
stopWatch.Start();

await TimeoutHandler.ExecuteInChunksAsync(emailSend.Emails, (b, ct) =>
    b.Select(email =>
    {
        if (ct.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }

        //send email
        return EmailSender.SendEmail(emailSend, email);
    }), cts.Token, batchSize);

stopWatch.Stop();

Console.WriteLine($"END, time elapsed : {stopWatch.Elapsed.TotalSeconds}");
Console.ReadLine();

