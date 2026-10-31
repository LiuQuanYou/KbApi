using System.ComponentModel.DataAnnotations;
namespace KbApi.Dtos;

public static class KnowledgeBaseRules
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 500;
}


// 创建时前端传的数据
public record CreateKnowledgeBaseDto(
    [property: Required, MaxLength(KnowledgeBaseRules.NameMaxLength)] string Name,
    [property: MaxLength(KnowledgeBaseRules.DescriptionMaxLength)] string? Description
);
// 更新时前端传的数据
public record UpdateKnowledgeBaseDto(
    [property: Required, MaxLength(KnowledgeBaseRules.NameMaxLength)] string Name,
    [property: MaxLength(KnowledgeBaseRules.DescriptionMaxLength)] string? Description
);
// 返回给前端的数据
public record KnowledgeBaseDto(
    Guid Id,
    string Name, string? Description, DateTime CreatedAt
);