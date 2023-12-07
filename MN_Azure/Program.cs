using Azure.Storage.Blobs;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MN_Azure.Abstractions;
using MN_Azure.Models;
using MN_Azure.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton(x => 
    new BlobServiceClient(builder.Configuration.GetValue<string>("BlobConnectionString")));

builder.Services.AddSingleton<IBlobServices, BlobService>();

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
