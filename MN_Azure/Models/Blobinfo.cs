namespace MN_Azure.Models;

public class Blobinfo
{
    public Stream Content { get; set; }
    public string ContentType { get; set; }
    public Blobinfo(Stream content, string contentType)
    {
        Content = content;
        ContentType = contentType;
    }
}