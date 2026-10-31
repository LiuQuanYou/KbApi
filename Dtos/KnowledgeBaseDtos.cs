namespace KbApi.Dtos;


// 创建时前端传的数据
public record CreateKnowledgeBaseDto(string Name, string? Description);
// 更新时前端传的数据
public record UpdateKnowledgeBaseDto(string Name, string? Description);
// 返回给前端的数据
public record KnowledgeBaseDto(Guid Id, string Name, string? Description, DateTime CreatedAt);