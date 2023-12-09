using MN_Azure.Models;

namespace MN_Azure.Abstractions;

public interface IQueueServices
{
    public Task PushInQueue(QueueData queueData);
    public Task PopFromQueue();
}