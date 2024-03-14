using GoodReads.Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodReads.Books.Infra.Persistence.Mappings
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("Title");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("Description");

            builder.Property(c => c.ISBN)
                .IsRequired()
                .HasColumnType("varchar(13)")
                .HasColumnName("ISBN");

            builder.Property(c => c.Author)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("Author");

            builder.Property(c => c.Publisher)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("Publisher");

            builder.Property(c=> c.Gender)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Gender");

            builder.Property(c => c.Year)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Year");

            builder.Property(c => c.NumberOfPages)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("NumberOfPages");

            builder.Property(c => c.AverageGrade)
                .IsRequired()
                .HasColumnType("decimal(15,2)")
                .HasColumnName("AverageGrade");

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("CreatedAt");
        }
    }
}
