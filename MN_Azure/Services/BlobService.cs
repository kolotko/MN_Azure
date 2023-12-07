using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MN_Azure.Abstractions;
using MN_Azure.Models;

namespace MN_Azure.Services;

public class BlobService : IBlobServices
{
    private readonly BlobServiceClient _blob;
    private readonly string _blobContainerName;

    public BlobService(BlobServiceClient blob)
    {
        _blob = blob;
        _blobContainerName = "example";
    }
    public async Task<Blobinfo> GetBlobAsync(string blobName)
    {
        var containerClient = _blob.GetBlobContainerClient(_blobContainerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        var blobDownloadInfo =  await blobClient.DownloadAsync();
        return new Blobinfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
    }

    public async Task<IEnumerable<string>> ListBlobAsync()
    {
        var containerClient = _blob.GetBlobContainerClient(_blobContainerName);
        var items = new List<string>();

        await foreach (var blobItem in containerClient.GetBlobsAsync())
            items.Add(blobItem.Name);

        return items;
    }

    public async Task UploadFileBlobAsync(string filePath, string fileName)
    {
        var containerClient = _blob.GetBlobContainerClient(_blobContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(filePath, new BlobHttpHeaders() {ContentType = "image/png"});
    }

    public async Task UploadContentBlobAsync(string content, string fileName)
    {
        var containerClient = _blob.GetBlobContainerClient(_blobContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        var bytes = Encoding.UTF8.GetBytes(content);
        using var memoryStream = new MemoryStream(bytes);
        await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders() {ContentType = "image/png"});
    }

    public async Task DeleteBlobAsync(string blobName)
    {
        var containerClient = _blob.GetBlobContainerClient(_blobContainerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.DeleteIfExistsAsync();
    }
}