using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Raktárkezelő.Models;

public partial class RaktarContext : DbContext
{
    public RaktarContext()
    {
    }

    public RaktarContext(DbContextOptions<RaktarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beszallitok> Beszallitoks { get; set; }

    public virtual DbSet<Termekek> Termekeks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=127.0.0.1;PORT=3306;DATABASE=Raktar;USER=root;PASSWORD=termekek;SSL Mode=Disabled;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beszallitok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("beszallitok");

            entity.HasIndex(e => e.Email, "Email_UNIQUE").IsUnique();

            entity.Property(e => e.Cim).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(32);
            entity.Property(e => e.Nev).HasMaxLength(64);
            entity.Property(e => e.Telefon).HasMaxLength(32);
        });

        modelBuilder.Entity<Termekek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("termekek");

            entity.HasIndex(e => e.BeszallitoId, "BeszallitoID");

            entity.Property(e => e.BeszallitoId).HasColumnName("BeszallitoID");
            entity.Property(e => e.Leiras).HasMaxLength(256);
            entity.Property(e => e.Megnevezes).HasMaxLength(64);

            entity.HasOne(d => d.Beszallito).WithMany(p => p.Termekeks)
                .HasForeignKey(d => d.BeszallitoId)
                .HasConstraintName("BeszallitoID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
