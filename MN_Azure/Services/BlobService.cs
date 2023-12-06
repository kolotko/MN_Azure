using MN_Azure.Abstractions;
using MN_Azure.Models;

namespace MN_Azure.Services;

public class BlobService : IBlobServices
{
    public Task<Blobinfo> GetBlobAsync(string blobName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> ListBlobAsync()
    {
        throw new NotImplementedException();
    }

    public Task UploadFileBlobAsync(string filePath, string fileName)
    {
        throw new NotImplementedException();
    }

    public Task UploadContentBlobAsync(string content, string fileName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBlobAsync(string blobName)
    {
        throw new NotImplementedException();
    }
}