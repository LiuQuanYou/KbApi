namespace KbApi.Models;

public class KnowledgeBase
{
    public List<Document> Documents { get; set; } = new();
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}