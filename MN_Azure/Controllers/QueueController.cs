using Microsoft.AspNetCore.Mvc;
using MN_Azure.Abstractions;
using MN_Azure.Models;

namespace MN_Azure.Controllers;

[ApiController]
[Route("[controller]")]
public class QueueController : ControllerBase
{
    private readonly IQueueServices _queueServices;
    public QueueController(IQueueServices queueServices)
    {
        _queueServices = queueServices;
    }
    [HttpPost("put")]
    public async Task<IActionResult> Put(QueueData QueueData)
    {
        _queueServices.PushInQueue(QueueData);
        return Ok();
    }
}