using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RecruitmentProcessDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessInfrastructure.Persistence.Context
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
            optionsBuilder.UseCosmos(_config["CosmosDb:AccountEnd"], _config["CosmosDb:AccountKey"], _config["CosmosDb:Database"],
                options => options.ConnectionMode(ConnectionMode.Direct)
                                  .RequestTimeout(TimeSpan.FromSeconds(60)));
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
           modelBuilder.Entity<ApplicationForm>(p =>
           {
               p.ToContainer("ApplicationForm");
               p.HasPartitionKey(e => e.Id);
               p.OwnsMany(_ => _.AddQuestions, x=>x.OwnsMany(a=>a.Choices));
               p.OwnsOne(_ => _.CurrentResidence);
               p.OwnsOne(_ => _.DOB);
               p.OwnsOne(_ => _.IDNumber);
               p.OwnsOne(_ => _.Gender);
               p.OwnsOne(_ => _.Nationality);
               p.OwnsOne(_ => _.PhoneNumber);
           });
            modelBuilder.Entity<QuestionType> ()
                .ToContainer("QuestionTypes")
                .HasPartitionKey(e => e.Id);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.IsDeleted = false;
                        entry.Entity.Id = Guid.NewGuid().ToString();
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<ApplicationForm> ApplicationForm { get; set; }
    }
}
