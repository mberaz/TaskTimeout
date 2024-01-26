
using System.Diagnostics;
using TaskTimeout;

Console.WriteLine("Hello, World!");

var emailSend = new EmailSendInfo
{
    Body = "This is an email",
    CustomerId = 1,
    EmailSentId = 1,
    Emails = new List<string>
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

var maxTimeSpan = TimeSpan.FromSeconds(5);
var batchSize = 3;

var cts = new CancellationTokenSource();
cts.CancelAfter(maxTimeSpan);

var stopWatch = new Stopwatch();
stopWatch.Start();

var tasks = TimeoutHandler.ExecuteInChunksAsync(emailSend.Emails, (b, ct) =>
    b.Select(email =>
    {
        if (ct.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }
        //send email
        return EmailSender.SendEmail(emailSend, email);
    }), cts.Token, batchSize);

await Task.WhenAll(tasks);


stopWatch.Stop();

Console.WriteLine($"END, time elapsed : {stopWatch.Elapsed.TotalSeconds}");

Console.ReadLine();

