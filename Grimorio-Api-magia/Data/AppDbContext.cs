using GrimorioDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<EscolaDeMagia> EscolasDeMagia { get; set; }
    public DbSet<Magia> Magias { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }
    public DbSet<Pocao> Pocoes { get; set; }
    public DbSet<PocaoIngrediente> PocaoIngredientes { get; set; }
    public DbSet<Feiticeiro> Feiticeiros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PocaoIngrediente>()
            .HasKey(pi => new { pi.PocaoId, pi.IngredienteId });

        modelBuilder.Entity<PocaoIngrediente>()
            .HasOne(pi => pi.Pocao)
            .WithMany(p => p.PocaoIngredientes)
            .HasForeignKey(pi => pi.PocaoId);

        modelBuilder.Entity<PocaoIngrediente>()
            .HasOne(pi => pi.Ingrediente)
            .WithMany(i => i.PocaoIngredientes)
            .HasForeignKey(pi => pi.IngredienteId);

        modelBuilder.Entity<Magia>()
            .HasOne(m => m.EscolaDeMagia)
            .WithMany(e => e.Magias)
            .HasForeignKey(m => m.EscolaDeMagiaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feiticeiro>()
            .HasOne(f => f.EscolaDeMagia)
            .WithMany(e => e.Feiticeiros)
            .HasForeignKey(f => f.EscolaDeMagiaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feiticeiro>()
            .HasOne(f => f.Usuario)
            .WithMany(u => u.Feiticeiros)
            .HasForeignKey(f => f.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EscolaDeMagia>().HasData(
            new EscolaDeMagia { Id = 1, Nome = "Evocação", Descricao = "Magias de invocação de elementos", Elemento = "Fogo" },
            new EscolaDeMagia { Id = 2, Nome = "Necromancia", Descricao = "Magias relacionadas à morte e não-mortos", Elemento = "Sombra" },
            new EscolaDeMagia { Id = 3, Nome = "Ilusão", Descricao = "Magias de engano e ilusão", Elemento = "Arcano" }
        );

        modelBuilder.Entity<Ingrediente>().HasData(
            new Ingrediente { Id = 1, Nome = "Raiz de Mandragora", Descricao = "Raiz mágica rara", Raridade = "Raro", Quantidade = 10 },
            new Ingrediente { Id = 2, Nome = "Pó de Osso de Dragão", Descricao = "Resíduo de dragão ancião", Raridade = "Lendario", Quantidade = 3 },
            new Ingrediente { Id = 3, Nome = "Erva do Sono", Descricao = "Erva comum com propriedades sedativas", Raridade = "Comum", Quantidade = 50 }
        );
    }
}
