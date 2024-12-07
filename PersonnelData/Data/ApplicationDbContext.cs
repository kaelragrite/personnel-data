using Microsoft.EntityFrameworkCore;
using PersonnelData.Models;

namespace PersonnelData.Data;

public class ApplicationDbContext : DbContext
{
#pragma warning disable CS8618
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
#pragma warning restore CS8618

    public DbSet<Person> Persons { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<PersonRelation> PersonRelations { get; set; }
    public DbSet<Phone> Phones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Person>()
            .HasMany(x => x.Phones)
            .WithOne(x => x.Person)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Person>()
            .HasMany(x => x.Relations)
            .WithOne(x => x.RelatedPerson)
            .HasForeignKey(x => x.RelatedPersonId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<PersonRelation>().HasKey(x => new { x.PersonId, x.RelatedPersonId });
    }
}