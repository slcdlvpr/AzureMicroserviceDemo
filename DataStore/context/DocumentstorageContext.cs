using Microsoft.EntityFrameworkCore;

namespace DataStore.context
{
    public partial class DocumentstorageContext : DbContext
    {
        private readonly string _connectionString;
        public DocumentstorageContext(string connection)
        {
            _connectionString = connection;
        }
        public DocumentstorageContext(DbContextOptions<DocumentstorageContext> options)
            : base(options)
        {
        }
        public virtual DbSet<MemberDocumentStorage> MemberDocumentStorage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(_connectionString, x => x.UseNetTopologySuite());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberDocumentStorage>(entity =>
            {
                entity.Property(e => e.FileUri)
                    .IsRequired()
                    .HasColumnName("FileURI")
                    .HasMaxLength(500);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
