using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using TestDAL.Model;

namespace TestDAL.Infrastructure
{
    public class ModelContext : DbContextBase, IModelContext
    {
        /// <summary>
        /// Used by wWeb Application
        /// </summary>
        /// <param name="options"></param>
        public ModelContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Used by Lambda's
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="loggerProvider"></param>
        public ModelContext(string connectionString, ILoggerProvider loggerProvider = null) : base(connectionString, loggerProvider) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Code for constraints on entities here
        }

        #region Data Members
        public DbContext DbContext => this;

        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
       
        #endregion
    }
}
