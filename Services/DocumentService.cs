using KbApi.Data;
using KbApi.Dtos;
using KbApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KbApi.Services;

public class DocumentService : IDocumentService
{
    private readonly AppDbContext _db;

    public DocumentService(AppDbContext db)
    {
        _db = db;
    }

    private static DocumentDto ToDto(Document document) =>
        new(document.Id, document.KnowledgeBaseId, document.Title, document.Content, document.CreatedAt);

    public async Task<IEnumerable<DocumentDto>?> GetAll(Guid kbId)
    {
        var knowledgeBaseExists = await _db.KnowledgeBases
            .AnyAsync(x => x.Id == kbId);

        if (!knowledgeBaseExists) return null;

        return await _db.Documents
            .AsNoTracking()
            .Where(x => x.KnowledgeBaseId == kbId)
            .Select(x => new DocumentDto(
                x.Id,
                x.KnowledgeBaseId,
                x.Title,
                x.Content,
                x.CreatedAt))
            .ToListAsync();
    }

    public async Task<DocumentDto?> GetById(Guid kbId, Guid id)
    {
        var document = await _db.Documents
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.KnowledgeBaseId == kbId && x.Id == id);

        return document is null ? null : ToDto(document);
    }

    public async Task<DocumentDto?> Create(Guid kbId, CreateDocumentDto input)
    {
        var knowledgeBaseExists = await _db.KnowledgeBases
            .AnyAsync(x => x.Id == kbId);

        if (!knowledgeBaseExists) return null;

        var document = new Document
        {
            Title = input.Title,
            Content = input.Content,
            KnowledgeBaseId = kbId
        };

        _db.Documents.Add(document);
        await _db.SaveChangesAsync();

        return ToDto(document);
    }

    public async Task<bool> Delete(Guid kbId, Guid id)
    {
        var document = await _db.Documents
            .FirstOrDefaultAsync(x => x.KnowledgeBaseId == kbId && x.Id == id);

        if (document is null) return false;

        _db.Documents.Remove(document);
        await _db.SaveChangesAsync();
        return true;
    }
}
