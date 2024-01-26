namespace TaskTimeout;

public class TimeoutHandler
{
    public static async Task ExecuteInChunksAsync<TInfo>(IEnumerable<TInfo> info,
        Func<IEnumerable<TInfo>, CancellationToken, IEnumerable<Task>> action,
        CancellationToken cancellationToken,
        int batchSize = 100)
    {
        foreach (var chunk in info.Chunk(batchSize))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("CancellationToken was cancelled");
                break;
            }

            Console.WriteLine("Starting new batch");
            await Task.WhenAll(action(chunk, cancellationToken));
        }
    }
}