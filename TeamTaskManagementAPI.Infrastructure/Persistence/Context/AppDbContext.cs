using Microsoft.EntityFrameworkCore;
using TeamTaskManagementAPI.Domain.Models;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using TeamTaskManagementAPI.Domain.Models;

namespace TeamTaskManagementAPI.Infrastructure.Persistence.Context
{
    public class AppDbContext:DbContext
    {
        //change your local cosmosdb with ->>> CosmosDb.Emulator /port=8080
        private IConfiguration _config;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config):base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"],
                b => b.MigrationsAssembly("TeamTaskManagementAPI"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //If the actual entity is an auditable type. 
                if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
                {
                    //add Global Query Filter to exclude deleted items
                    //https://docs.microsoft.com/en-us/ef/core/querying/filters
                    //That always excludes deleted items. Opt out by using dbSet.IgnoreQueryFilters()
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var deletedCheck = Expression.Lambda(Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
                }
            }
            // modelBuilder.Entity<Task>()
            //     .HasKey(t => t.Id); 
            modelBuilder.Entity<TeamUser>()
                .HasKey(tu => new { tu.UserId, tu.TeamId });

            modelBuilder.Entity<TeamUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TeamUsers)
                .HasForeignKey(tu => tu.UserId);

            modelBuilder.Entity<TeamUser>()
                .HasOne(tu => tu.Team)
                .WithMany(t => t.TeamUsers)
                .HasForeignKey(tu => tu.TeamId);

            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.Team)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.TeamId);

            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedByUserId);

            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToUserId);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.IsDeleted = false;
                        entry.Entity.Id = Guid.NewGuid().ToString();
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.Now;
                        break;
                }
            } 
            return base.SaveChanges();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        
    }
}
