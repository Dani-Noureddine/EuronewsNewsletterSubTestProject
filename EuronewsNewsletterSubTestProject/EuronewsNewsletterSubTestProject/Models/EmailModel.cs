public class Email { 
    public string? Id { get; set; }
    public string? ThreadId { get; set; }
    public List<string>? LabelIds { get; set; }
    public string? Snippet { get; set; }
    public Payload? Payload { get; set; }
    public int? SizeEstimate { get; set; }
    public string? HistoryId { get; set; }
    public string? InternalDate { get; set; }
}