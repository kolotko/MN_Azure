using MN_Azure.Abstractions;

namespace MN_Azure.BackgroundTasks;

public class QueueBackground : BackgroundService
{
    private readonly IQueueServices _queueServices;
    public QueueBackground(IQueueServices queueServices)
    {
        _queueServices = queueServices;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _queueServices.PopFromQueue();
            await Task.Delay(TimeSpan.FromSeconds(60));
        }
    }
}