using KbApi.Dtos;
using KbApi.Models;
using KbApi.Data;
using Microsoft.EntityFrameworkCore;

namespace KbApi.Services;

public class KnowledgeBaseService : IKnowledgeBaseService
{
    private readonly AppDbContext _db;
    public KnowledgeBaseService(AppDbContext db)
    {
        _db = db;
    }


    // 把模型转成 DTO 的小工具，几个方法都要用
    private static KnowledgeBaseDto ToDto(KnowledgeBase kb) =>
        new(kb.Id, kb.Name, kb.Description, kb.CreatedAt);
    public async Task<List<KnowledgeBaseDto>> GetAll() =>
    await _db.KnowledgeBases
        .AsNoTracking()
        .Select(kb => new KnowledgeBaseDto(kb.Id, kb.Name, kb.Description, kb.CreatedAt))
        .ToListAsync();
    public async Task<KnowledgeBaseDto?> GetById(Guid id)
    {
        var kb = await _db.KnowledgeBases.FindAsync(id);
        return kb is null ? null : ToDto(kb);
    }
    public async Task<KnowledgeBaseDto> Create(CreateKnowledgeBaseDto input)
    {
        var kb = new KnowledgeBase
        {
            Name = input.Name,
            Description = input.Description
        };
        _db.KnowledgeBases.Add(kb);
        await _db.SaveChangesAsync();
        return ToDto(kb);
    }
    public async Task<bool> Update(Guid id, UpdateKnowledgeBaseDto input)
    {
        var kb = await _db.KnowledgeBases.FindAsync(id);
        if (kb is null) return false;
        kb.Name = input.Name;
        kb.Description = input.Description;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var kb = await _db.KnowledgeBases.FindAsync(id);
        if (kb is null) return false;
        _db.Remove(kb);
        await _db.SaveChangesAsync();
        return true;
    }
}