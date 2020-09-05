using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POS.DAL.ModelConfiguration;
using POS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DAL
{
    public class POSDBContext : DbContext
    {
        public POSDBContext(DbContextOptions<POSDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"],
                    sqlServer => sqlServer.MigrationsHistoryTable("__Migration", "_Migration"));
            }
        }


        public virtual DbSet<MasterItem> MasterItems { get; set; }
        public virtual DbSet<MasterCustomer> MasterCustomers { get; set; }
        public virtual DbSet<POSMain> POSMains { get; set; }
        public virtual DbSet<POSDetail> POSDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MasterItemConfiguration());
            modelBuilder.ApplyConfiguration(new MasterCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new POSMainConfiguration());
            modelBuilder.ApplyConfiguration(new POSDetailConfiguration());
        }
    }
}
