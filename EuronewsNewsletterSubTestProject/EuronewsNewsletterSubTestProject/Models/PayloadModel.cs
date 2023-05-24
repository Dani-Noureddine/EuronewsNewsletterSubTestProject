public class Payload
{
    public string? PartId { get; set; }
    public string? MimeType { get; set; }
    public string? Filename { get; set; }
    public List<Header>? Headers { get; set; }
    public Body? Body { get; set; }
    public List<Part>? Parts { get; set; }
}