using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<FactoryDevice> FactoryDevices { get; set; }
        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }

    }
}

