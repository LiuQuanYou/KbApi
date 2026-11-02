using KbApi.Dtos;
namespace KbApi.Services;

public interface IKnowledgeBaseService
{
    Task<List<KnowledgeBaseDto>> GetAll();
    Task<KnowledgeBaseDto?> GetById(Guid id);
    Task<KnowledgeBaseDto> Create(CreateKnowledgeBaseDto input);
    Task<bool> Update(Guid id, UpdateKnowledgeBaseDto input);
    Task<bool> Delete(Guid id);
}