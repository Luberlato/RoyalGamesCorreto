using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoyalGames.Domains;

namespace RoyalGames.Contexts;

public partial class RoyalGamesContext : DbContext
{
    public RoyalGamesContext()
    {
    }

    public RoyalGamesContext(DbContextOptions<RoyalGamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoIndicativa> ClassificacaoIndicativa { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Jogo> Jogo { get; set; }

    public virtual DbSet<Log_AlteracaoJogo> Log_AlteracaoJogo { get; set; }

    public virtual DbSet<Plataforma> Plataforma { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GS4LSO7;Database=RoyalGames;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoIndicativa>(entity =>
        {
            entity.HasKey(e => e.ClassificacaoIndicativaID).HasName("PK__Classifi__892DEC6F119BF912");

            entity.Property(e => e.Faixa)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroID).HasName("PK__Genero__A99D02680CF8FB43");

            entity.Property(e => e.Nome)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.JogoID).HasName("PK__Jogo__591968553C5AB148");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_ExclusaoJogo");
                    tb.HasTrigger("trg_LogAlteracaoJogos");
                });

            entity.Property(e => e.DataLancamento).HasPrecision(0);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.ClassificacaoIndicativa).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.ClassificacaoIndicativaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jogo__Classifica__3D5E1FD2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jogo__UsuarioID__3E52440B");

            entity.HasMany(d => d.Genero).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroID")
                        .HasConstraintName("Fk_JogoGenero_Genero"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("Fk_JogoGenero_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "GeneroID").HasName("Pk_JogoGenero");
                    });

            entity.HasMany(d => d.Plataforma).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoPlataforma",
                    r => r.HasOne<Plataforma>().WithMany()
                        .HasForeignKey("PlataformaID")
                        .HasConstraintName("Fk_JogoPlataforma_Plataforma"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("Fk_JogoPlataforma_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "PlataformaID").HasName("Pk_JogoPlataforma");
                    });
        });

        modelBuilder.Entity<Log_AlteracaoJogo>(entity =>
        {
            entity.HasKey(e => e.AlteracaoID).HasName("PK__Log_Alte__8C18DF4C9ABFBE18");

            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Jogo).WithMany(p => p.Log_AlteracaoJogo)
                .HasForeignKey(d => d.JogoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Log_Alter__JogoI__44FF419A");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaID).HasName("PK__Platafor__B835678DCF0AB9C0");

            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE79879EDA1CE");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534E2061C38").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
