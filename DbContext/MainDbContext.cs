using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Configuration;
using DbModels;
using Microsoft.Extensions.Hosting.Internal;
using DbContext.Extensions;
using Models;
using System.ComponentModel.DataAnnotations;

namespace DbContext;

//DbContext namespace is a fundamental EFC layer of the database context and is
//used for all Database connection as well as for EFC CodeFirst migration and database updates 
public class MainDbContext : Microsoft.EntityFrameworkCore.DbContext
{
        DatabaseConnections _databaseConnections;

#if DEBUG
    // remove password from connection string in debug mode
    // this is useful for debugging and logging purposes, but should not be used in production code
    public string dbConnection => System.Text.RegularExpressions.Regex.Replace(
        this.Database.GetConnectionString() ?? "", @"(pwd|password)=[^;]*;?", "",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase);
#endif


    public DbSet<AttractionDbM> AttractionDbM { get; set;}
    public DbSet<AddressDbM> AddressDbM { get; set; }
    public DbSet<UsersDbM> UsersDbM { get; set; }
    public DbSet<CommentDbM> CommentDbM { get; set; }
   
    public MainDbContext() { }
    public MainDbContext(DbContextOptions options, DatabaseConnections databaseConnections) : base(options)
    { 
        _databaseConnections = databaseConnections;
    }

    //Here we can modify the migration building
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      

        base.OnModelCreating(modelBuilder);
        
        // Ignore base model classes
        modelBuilder.Ignore<Models.Attraction>();
        modelBuilder.Ignore<Models.Address>();
        modelBuilder.Ignore<Models.Users>();
        modelBuilder.Ignore<Models.Comment>();

        // Address Configuration
        modelBuilder.Entity<AddressDbM>(entity =>
        {
            entity.HasKey(a => a.AddressId);
            entity.Property(a => a.StreetAddress).HasMaxLength(200);
            entity.Property(a => a.City).HasMaxLength(100);
            entity.Property(a => a.Country).HasMaxLength(100);
            entity.Property(a => a.ZipCode).IsRequired();
        });

        // Users Configuration
        modelBuilder.Entity<UsersDbM>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.FullName).HasMaxLength(200).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(250).IsRequired();
            
            entity.HasOne(u => u.AddressDbM)
                  .WithMany(a => a.UsersDbM)
                  .HasForeignKey(u => u.AddressId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(u => u.CommentDbM)
                  .WithOne(c => c.UsersDbM)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AttractionDbM>(entity =>
        {
            entity.HasKey(a => a.AttractionId);
            entity.Property(a => a.Name).HasMaxLength(200).IsRequired();
            entity.Property(a => a.Description).HasMaxLength(1000);
            entity.Property(a => a.Category).HasConversion<string>();
            entity.Property(a => a.Type).HasConversion<string>();
            
            entity.HasOne(a => a.AddressDbM)
                  .WithOne(addr => addr.AttractionDbM)
                  .HasForeignKey<AttractionDbM>(a => a.AddressId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Comment Configuration
        modelBuilder.Entity<CommentDbM>(entity =>
        {
            entity.HasKey(c => c.CommentId);
            entity.Property(c => c.Text).HasMaxLength(1000).IsRequired();
            
            entity.HasOne(c => c.AttractionDbM)
                  .WithMany(a => a.CommentDbM)
                  .HasForeignKey(c => c.AttractionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
    
        
  

    public class SqlServerDbContext : MainDbContext
    {
        public SqlServerDbContext() { }
        public SqlServerDbContext(DbContextOptions options, DatabaseConnections databaseConnections)
            : base(options, databaseConnections) { }


        //Used only for CodeFirst Database Migration and database update commands
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = optionsBuilder.ConfigureForDesignTime(
                    (options, connectionString) => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HaveColumnType("money");
            configurationBuilder.Properties<string>().HaveColumnType("varchar(200)");

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add your own modelling based on done migrations
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MySqlDbContext : MainDbContext
    {
        public MySqlDbContext() { }
        public MySqlDbContext(DbContextOptions options) : base(options, null) { }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = optionsBuilder.ConfigureForDesignTime(
                    (options, connectionString) =>
                        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                            b => b.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}")));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar(200)");

            base.ConfigureConventions(configurationBuilder);

        }
    }

    public class PostgresDbContext : MainDbContext
    {
        public PostgresDbContext() { }
        public PostgresDbContext(DbContextOptions options) : base(options, null){ }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = optionsBuilder.ConfigureForDesignTime(
                    (options, connectionString) => options.UseNpgsql(connectionString));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar(200)");
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
