using System.ComponentModel.DataAnnotations;
namespace KbApi.Dtos;

public record DocumentDto(
    Guid Id,
    Guid KnowledgeBaseId,
    string Title,
    string Content,
    DateTime CreatedAt)
;

public record CreateDocumentDto(
    [Required, MaxLength(200)] string Title,
    [Required] string Content
);