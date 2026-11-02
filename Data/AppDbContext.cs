using Microsoft.EntityFrameworkCore;
using KbApi.Models;
using KbApi.Dtos;
namespace KbApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<KnowledgeBase> KnowledgeBases => Set<KnowledgeBase>();
    public DbSet<Document> Documents => Set<Document>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KnowledgeBase>(e =>
        {
            // TODO: Name 必填，最大长度 100（或者你 DTO 里定的长度）
            e.Property(x => x.Name)
             .IsRequired()
             .HasMaxLength(KnowledgeBaseRules.NameMaxLength);
        });
        modelBuilder.Entity<Document>(e =>
        {
            // TODO: Title 必填，最大长度 200
            // TODO: 配置一对多：一个 KnowledgeBase 有多个 Documents，
            //       外键是 KnowledgeBaseId，删除知识库时级联删除文档
            e.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            e.HasOne(x => x.KnowledgeBase)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.KnowledgeBaseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}