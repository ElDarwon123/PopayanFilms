using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PopayanFilms.Modules.Movies.Domain.Movies;

namespace PopayanFilms.Modules.Movies.Infrastructure.Database.Configurations;

internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(m => m.Description)
            .HasMaxLength(2000);

        builder.Property(m => m.Director)
            .HasMaxLength(200);

        builder.Property(m => m.Genre)
            .HasMaxLength(100);

        builder.Property(m => m.Rating)
            .HasPrecision(3, 1);

        builder.HasIndex(m => m.Title)
            .IsUnique();
    }
}
