using KbApi.Dtos;
using KbApi.Models;
namespace KbApi.Services;

public class KnowledgeBaseService : IKnowledgeBaseService
{
    private readonly List<KnowledgeBase> _store = new();

    // 把模型转成 DTO 的小工具，几个方法都要用
    private static KnowledgeBaseDto ToDto(KnowledgeBase kb) =>
        new(kb.Id, kb.Name, kb.Description, kb.CreatedAt);
    public IEnumerable<KnowledgeBaseDto> GetAll() =>
        _store.Select(ToDto);
    public KnowledgeBaseDto? GetById(Guid id)
    {
        var kb = _store.FirstOrDefault(x => x.Id == id);
        return kb is null ? null : ToDto(kb);
    }
    public KnowledgeBaseDto Create(CreateKnowledgeBaseDto input)
    {
        var kb = new KnowledgeBase
        {
            Name = input.Name,
            Description = input.Description
        };
        _store.Add(kb);
        return ToDto(kb);
    }
    public bool Update(Guid id, UpdateKnowledgeBaseDto input)
    {
        var kb = _store.FirstOrDefault(x => x.Id == id);
        if (kb is null) return false;
        kb.Name = input.Name;
        kb.Description = input.Description;
        return true;
    }

    public bool Delete(Guid id)
    {
        var kb = _store.FirstOrDefault(x => x.Id == id);
        if (kb is null) return false;
        _store.Remove(kb);
        return true;
    }
}