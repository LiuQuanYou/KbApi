using KbApi.Dtos;
namespace KbApi.Services;

public interface IDocumentService
{
    Task<IEnumerable<DocumentDto>?> GetAll(Guid kbId);        // 知识库不存在返回 null
    Task<DocumentDto?> GetById(Guid kbId, Guid id);
    Task<DocumentDto?> Create(Guid kbId, CreateDocumentDto input);
    Task<bool> Delete(Guid kbId, Guid id);
}
