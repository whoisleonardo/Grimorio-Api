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
            new EscolaDeMagia { Id = 1, Nome = "Evocação", Descricao = "Escola de magia focada na invocação de seres e elementos poderosos do plano astral", Elemento = "Fogo" },
            new EscolaDeMagia { Id = 2, Nome = "Necromancia", Descricao = "Escola que manipula as forças da morte e controla criaturas não-mortas", Elemento = "Sombra" },
            new EscolaDeMagia { Id = 3, Nome = "Ilusão", Descricao = "Arte de enganar os sentidos e criar ilusões para confundir inimigos", Elemento = "Arcano" },
            new EscolaDeMagia { Id = 4, Nome = "Transmutação", Descricao = "Transformação de matéria e mudança de propriedades dos objetos", Elemento = "Terra" },
            new EscolaDeMagia { Id = 5, Nome = "Adivinhação", Descricao = "Capacidade de prever o futuro e desvendar mistérios ocultos", Elemento = "Éter" },
            new EscolaDeMagia { Id = 6, Nome = "Abjuração", Descricao = "Proteção através de barreiras mágicas e repulsão de forças negativas", Elemento = "Luz" }
        );

        modelBuilder.Entity<Ingrediente>().HasData(
            new Ingrediente { Id = 1, Nome = "Raiz de Mandragora", Descricao = "Raiz de uma planta mágica ancestral com poderes de invocação", Raridade = "Raro", Quantidade = 15 },
            new Ingrediente { Id = 2, Nome = "Pó de Osso de Dragão", Descricao = "Resíduo pulverizado de ossos de dragão ancião com grande poder mágico", Raridade = "Lendario", Quantidade = 5 },
            new Ingrediente { Id = 3, Nome = "Erva do Sono", Descricao = "Erva comum com propriedades sedativas naturais", Raridade = "Comum", Quantidade = 100 },
            new Ingrediente { Id = 4, Nome = "Cristal de Quartzo Azul", Descricao = "Cristal raro que amplifica a magia de adivinhação", Raridade = "Raro", Quantidade = 8 },
            new Ingrediente { Id = 5, Nome = "Tinta de Kraken", Descricao = "Tinta coletada de um kraken antigo, elemento raro para transmutação", Raridade = "Lendario", Quantidade = 2 },
            new Ingrediente { Id = 6, Nome = "Pétalas de Flor Noturna", Descricao = "Pétalas que só florescem à noite com propósitos de abjuração", Raridade = "Incomum", Quantidade = 30 }
        );
    }
}
