using KbApi.Dtos;
namespace KbApi.Services;

public interface IKnowledgeBaseService
{
    IEnumerable<KnowledgeBaseDto> GetAll();
    KnowledgeBaseDto? GetById(Guid id);
    KnowledgeBaseDto Create(CreateKnowledgeBaseDto input);
    bool Update(Guid id, UpdateKnowledgeBaseDto input);
    bool Delete(Guid id);
}