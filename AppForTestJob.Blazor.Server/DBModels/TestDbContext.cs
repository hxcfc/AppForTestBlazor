using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AppForTestJob.Blazor.Server.DBModels
{
    public partial class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountNumber> AccountNumbers { get; set; }
        public virtual DbSet<AuthorizedClerk> AuthorizedClerks { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityPerson> EntityPeople { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Pesel> Pesels { get; set; }
        public virtual DbSet<Representative> Representatives { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(PrivateClass.PrivateConnString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountNumber>(entity =>
            {
                entity.Property(e => e.AccountNumberId).HasColumnName("accountNumberID");

                entity.Property(e => e.EntityId).HasColumnName("entityID");

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.AccountNumbers)
                    .HasForeignKey(d => d.EntityId)
                    .HasConstraintName("FK_AccountNumbers_Entity");
            });

            modelBuilder.Entity<AuthorizedClerk>(entity =>
            {
                entity.Property(e => e.AuthorizedClerkId).HasColumnName("authorizedClerkID");

                entity.Property(e => e.EntityId).HasColumnName("entityID");

                entity.Property(e => e.EntityPersonId).HasColumnName("entityPersonID");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.AuthorizedClerks)
                    .HasForeignKey(d => d.EntityId)
                    .HasConstraintName("FK_AuthorizedClerks_Entity");

                entity.HasOne(d => d.EntityPerson)
                    .WithMany(p => p.AuthorizedClerks)
                    .HasForeignKey(d => d.EntityPersonId)
                    .HasConstraintName("FK_AuthorizedClerks_EntityPerson");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.ToTable("Entity");

                entity.Property(e => e.EntityId).HasColumnName("entityID");

                entity.Property(e => e.HasVirtualAccounts).HasColumnName("hasVirtualAccounts");

                entity.Property(e => e.Krs)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("krs");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nip)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nip");

                entity.Property(e => e.PeselId).HasColumnName("peselID");

                entity.Property(e => e.RegistrationDenialBasis)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("registrationDenialBasis");

                entity.Property(e => e.RegistrationDenialDate)
                    .HasColumnType("date")
                    .HasColumnName("registrationDenialDate");

                entity.Property(e => e.RegistrationLegalDate)
                    .HasColumnType("date")
                    .HasColumnName("registrationLegalDate");

                entity.Property(e => e.Regon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("regon");

                entity.Property(e => e.RemovalBasis)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("removalBasis");

                entity.Property(e => e.RemovalDate)
                    .HasColumnType("date")
                    .HasColumnName("removalDate");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("residenceAddress");

                entity.Property(e => e.RestorationBasis)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("restorationBasis");

                entity.Property(e => e.RestorationDate)
                    .HasColumnType("date")
                    .HasColumnName("restorationDate");

                entity.Property(e => e.StatusVat).HasColumnName("statusVat");

                entity.Property(e => e.WorkingAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("workingAddress");

                entity.HasOne(d => d.Pesel)
                    .WithMany(p => p.Entities)
                    .HasForeignKey(d => d.PeselId)
                    .HasConstraintName("FK_Entity_Pesels");
            });

            modelBuilder.Entity<EntityPerson>(entity =>
            {
                entity.ToTable("EntityPerson");

                entity.Property(e => e.EntityPersonId).HasColumnName("entityPersonID");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("companyName");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.Nip)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nip");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.Property(e => e.PartnerId).HasColumnName("partnerID");

                entity.Property(e => e.EntityId).HasColumnName("entityID");

                entity.Property(e => e.EntityPersonId).HasColumnName("entityPersonID");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.EntityId)
                    .HasConstraintName("FK_Partners_Entity");

                entity.HasOne(d => d.EntityPerson)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.EntityPersonId)
                    .HasConstraintName("FK_Partners_EntityPerson");
            });

            modelBuilder.Entity<Pesel>(entity =>
            {
                entity.Property(e => e.PeselId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("peselID");

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.HasOne(d => d.PeselNavigation)
                    .WithOne(p => p.Pesel)
                    .HasForeignKey<Pesel>(d => d.PeselId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pesels_EntityPerson");
            });

            modelBuilder.Entity<Representative>(entity =>
            {
                entity.Property(e => e.RepresentativeId).HasColumnName("representativeID");

                entity.Property(e => e.EntityId).HasColumnName("entityID");

                entity.Property(e => e.EntityPersonId).HasColumnName("entityPersonID");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.Representatives)
                    .HasForeignKey(d => d.EntityId)
                    .HasConstraintName("FK_Representatives_Entity");

                entity.HasOne(d => d.EntityPerson)
                    .WithMany(p => p.Representatives)
                    .HasForeignKey(d => d.EntityPersonId)
                    .HasConstraintName("FK_Representatives_EntityPerson");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
