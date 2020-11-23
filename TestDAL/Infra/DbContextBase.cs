using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TestDAL.Infrastructure
{
    public class DbContextBase : DbContext
    {
        private readonly string connectionString;
        private readonly ILoggerProvider loggerProvider;

        /// <summary>
        /// Used by wWeb Application
        /// </summary>
        /// <param name="options"></param>
        public DbContextBase(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Used by Lambda's
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="loggerProvider"></param>
        public DbContextBase(string connectionString, ILoggerProvider loggerProvider = null)
        {
            this.connectionString = connectionString;
            this.loggerProvider = loggerProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseSqlServer(connectionString);

                if (loggerProvider != null)
                {
                    LoggerFactory loggerFactory = new LoggerFactory();
                    loggerFactory.AddProvider(loggerProvider);
                    optionsBuilder.UseLoggerFactory(loggerFactory);
                }
            }
        }

        public virtual void Save()
        {
            base.SaveChanges();
        }

        //Data Memebers added in child class
    }
}
