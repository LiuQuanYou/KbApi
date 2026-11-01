using KbApi.Dtos;
using KbApi.Models;
namespace KbApi.Services;

public class DocumentService : IDocumentService
{
    private readonly List<Document> _store = new();
    private readonly IKnowledgeBaseService _kbService;

    public DocumentService(IKnowledgeBaseService kbService)
    {
        _kbService = kbService;
    }

    private static DocumentDto ToDto(Document d) =>
    new(d.Id, d.KnowledgeBaseId, d.Title, d.Content, d.CreatedAt);

    private bool KbExists(Guid kbId) => _kbService.GetById(kbId) is not null;

    public IEnumerable<DocumentDto>? GetAll(Guid kbId)
    {
        // 1. 知识库不存在，返回 null，让 Controller 回 404
        if (!KbExists(kbId)) return null;

        // 2. 筛出属于这个知识库的文档，再转成 DTO
        return _store
            .Where(x => x.KnowledgeBaseId == kbId)
            .Select(ToDto);
    }

    public DocumentDto? GetById(Guid kbId, Guid id)
    {
        if (!KbExists(kbId)) return null;

        var d = _store.FirstOrDefault(x => x.KnowledgeBaseId == kbId && x.Id == id);
        return d is null ? null : ToDto(d);
    }

    public DocumentDto? Create(Guid kbId, CreateDocumentDto input)
    {
        if (!KbExists(kbId)) return null;

        var d = new Document
        {
            Title = input.Title,
            Content = input.Content,
            KnowledgeBaseId = kbId
        };

        _store.Add(d);
        return ToDto(d);
    }

    public bool Delete(Guid kbId, Guid id)
    {
        var d = _store.FirstOrDefault(x => x.KnowledgeBaseId == kbId && x.Id == id);
         if (!KbExists(kbId) || d is null) return false;
         _store.Remove(d);
         return true;
    }
}