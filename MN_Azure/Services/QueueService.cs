using System.Text.Json;
using Azure.Storage.Queues;
using MN_Azure.Abstractions;
using MN_Azure.Models;

namespace MN_Azure.Services;

public class QueueService : IQueueServices
{
    private readonly QueueClient _queueClient;
    public QueueService(QueueClient queueClient)
    {
        _queueClient = queueClient;
    }
    public async Task PushInQueue(QueueData queueData)
    {
        var json = JsonSerializer.Serialize(queueData);
        await _queueClient.SendMessageAsync(json);
    }
    
    public async Task PopFromQueue()
    {
        var queueMessage = await _queueClient.ReceiveMessageAsync();
        if (queueMessage.Value is not null)
        {
            var jsonFromMessage = queueMessage.Value.MessageText;
            await _queueClient.DeleteMessageAsync(queueMessage.Value.MessageId, queueMessage.Value.PopReceipt);
        }
    }
}