using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using ProjetoCompeticao.Domain.Academias.Entities;
using System.Reflection;

namespace ProjetoCompeticao.Infra.Data.DataContexts
{
    public class DataContext : DbContext
    {
        public DbSet<Academia> Academias { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore<Notification>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
