using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Azure;
using MN_Azure.Abstractions;
using MN_Azure.BackgroundTasks;
using MN_Azure.Models;
using MN_Azure.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSingleton<IQueueServices, QueueService>();
builder.Services.AddSingleton<IBlobServices, BlobService>();
// do extensions method
var enableBackgroundTask = builder.Configuration.GetValue<bool>("EnableBackgroundTask");
if (enableBackgroundTask)
{
    builder.Services.AddHostedService<QueueBackground>();
}

builder.Services.AddAzureClients(azureBuilder =>
{
    var storageAccountConnectionString = builder.Configuration.GetValue<string>("StorageAccountConnectionString");
    var queueName = builder.Configuration.GetValue<string>("QueueName");
    azureBuilder.AddClient<QueueClient, QueueClientOptions>((_, _, _) =>
    {
        return new QueueClient(storageAccountConnectionString,queueName);
    });

    azureBuilder.AddQueueServiceClient(storageAccountConnectionString);
    azureBuilder.AddBlobServiceClient(storageAccountConnectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.MapHealthChecks("/_health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseAuthorization();

app.MapControllers();

app.Run();
