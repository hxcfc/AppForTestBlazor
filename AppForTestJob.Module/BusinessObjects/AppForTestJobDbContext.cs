using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using System.Text.Json;

namespace AppForTestJob.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class AppForTestJobContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<AppForTestJobEFCoreDbContext>()
            .UseSqlServer(@"Server=tcp:testserwer.database.windows.net,1433;Initial Catalog=TestDb;Persist Security Info=False;User ID=testserwer;Password=Lol1234567;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        return new AppForTestJobEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class AppForTestJobDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppForTestJobEFCoreDbContext> {
	public AppForTestJobEFCoreDbContext CreateDbContext(string[] args) {
		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		//var optionsBuilder = new DbContextOptionsBuilder<AppForTestJobEFCoreDbContext>();
		//optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AppForTestJob");
		//return new AppForTestJobEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(AppForTestJobContextInitializer))]
public class AppForTestJobEFCoreDbContext : DbContext
{
    public AppForTestJobEFCoreDbContext(DbContextOptions<AppForTestJobEFCoreDbContext> options) : base(options)
    {
    }
    public virtual DbSet<AccountNumber> AccountNumbers { get; set; }
    public virtual DbSet<AuthorizedClerk> AuthorizedClerks { get; set; }
    public virtual DbSet<Entity> Entities { get; set; }
    public virtual DbSet<EntityPerson> EntityPeople { get; set; }
    public virtual DbSet<Partner> Partners { get; set; }
    public virtual DbSet<Pesel> Pesels { get; set; }
    public virtual DbSet<Representative> Representatives { get; set; }

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
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.Property(e => e.Nip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nip");

            entity.Property(e => e.PeselId).HasColumnName("peselID");

            entity.Property(e => e.RegistrationDenialBasis)
                .HasMaxLength(150)
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
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("removalBasis");

            entity.Property(e => e.RemovalDate)
                .HasColumnType("date")
                .HasColumnName("removalDate");

            entity.Property(e => e.ResidenceAddress)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("residenceAddress");

            entity.Property(e => e.RestorationBasis)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("restorationBasis");

            entity.Property(e => e.RestorationDate)
                .HasColumnType("date")
                .HasColumnName("restorationDate");

            entity.Property(e => e.StatusVat).HasMaxLength(50).IsUnicode(false).HasColumnName("statusVat");

            entity.Property(e => e.WorkingAddress)
                .HasMaxLength(150)
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
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("companyName");

            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("firstName");

            entity.Property(e => e.LastName)
                .HasMaxLength(150)
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

    }


}

