using MN_Azure.Models;

namespace MN_Azure.Abstractions;

public interface IBlobServices
{
    public Task<Blobinfo> GetBlobAsync(string blobName);

    public Task<IEnumerable<string>> ListBlobAsync();

    public Task UploadFileBlobAsync(string filePath, string fileName);

    public Task UploadContentBlobAsync(string content, string fileName);

    public Task DeleteBlobAsync(string blobName);
}