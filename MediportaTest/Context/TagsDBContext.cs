using MediportaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MediportaTest.Context;

public partial class TagsDBContext : DbContext
{
    public TagsDBContext()
    {
    }

    public TagsDBContext(DbContextOptions<TagsDBContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=tags.db");
    
    
    public DbSet<Tag> Tags { get; set; } = null!;
}
