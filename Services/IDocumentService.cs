using KbApi.Dtos;
namespace KbApi.Services;

public interface IDocumentService
{
    IEnumerable<DocumentDto>? GetAll(Guid kbId);        // 知识库不存在返回 null
    DocumentDto? GetById(Guid kbId, Guid id);
    DocumentDto? Create(Guid kbId, CreateDocumentDto input);
    bool Delete(Guid kbId, Guid id);
}