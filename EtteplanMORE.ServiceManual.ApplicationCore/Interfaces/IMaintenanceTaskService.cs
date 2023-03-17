using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
	public interface IMaintenanceTaskService
	{
        Task<IEnumerable<MaintenanceTask>> GetAll();

        Task<MaintenanceTask> GetTask(int id);

        Task<IEnumerable<MaintenanceTask>> AddTask(MaintenanceTask task);

        Task<MaintenanceTask> EditTask(MaintenanceTask task);

        Task<IEnumerable<MaintenanceTask>> DeleteTask(int id);

        Task<IEnumerable<MaintenanceTask>> FilterByDevice(int id);
    }
}

