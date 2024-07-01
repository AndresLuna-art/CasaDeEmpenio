using Microsoft.EntityFrameworkCore;
using CasaDeEmpeño.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>()
            .Property(p => p.FechaIngreso)
            .HasColumnType("datetime2");

        modelBuilder.Entity<Producto>()
            .Property(p => p.FechaVencimientoDevolucion)
            .HasColumnType("datetime2");
                
        modelBuilder.Entity<Oferta>()
            .Property(o => o.FechaOferta)
            .HasColumnType("datetime2");


        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Oferta> Ofertas { get; set; }
    public DbSet<Devolucion> Devoluciones { get; set; }
}
