using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Data;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using static EtteplanMORE.ServiceManual.ApplicationCore.Entities.MaintenanceTask;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
	public class MaintenanceTaskService : IMaintenanceTaskService
	{
        private readonly DataContext _context;

        public MaintenanceTaskService(DataContext context)
        {
            _context = context;
        }

        //GET A LIST OF MAINTENANCE TASKS
        public async Task<IEnumerable<MaintenanceTask>> GetAll()
        {
            List<MaintenanceTask> tasks = await _context.MaintenanceTasks.ToListAsync();
            //for each maintenance task, add a factory device based on the foreign key
            foreach (MaintenanceTask task in tasks)
            {
                task.factoryDevice = await _context.FactoryDevices
                .Where(fd => fd.Id == task.FactoryDeviceId)
                .FirstOrDefaultAsync();
            }
            
            //Returns a list of tasks. Each task contains a factory device object.
            //Ordered firstly by severity and secondly by registration time
            return tasks.OrderBy(task => task.severity).ThenBy(task => task.TimeStamp);
        }


        //GET A SPESIFIC MAINTENANCE TASK BASED ON IT'S ID
        public async Task<MaintenanceTask> GetTask(int id)
        {
            MaintenanceTask task = await _context.MaintenanceTasks
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            task.factoryDevice = await _context.FactoryDevices
                .Where(fd => fd.Id == task.FactoryDeviceId)
                .FirstOrDefaultAsync();

            return task;
        }


        //ADD A TASK TO THE DB
        public async Task<IEnumerable<MaintenanceTask>> AddTask(MaintenanceTask task)
        {
            if (task.status != 0 && task.severity != 0 && task.Description != "")
            {
                var factoryDevice = await _context.FactoryDevices
                .Where(fd => fd.Id == task.FactoryDeviceId)
                .FirstOrDefaultAsync();

                if(factoryDevice == null)
                    throw new ArgumentException("No factory device for the given id");
        
                task.factoryDevice = factoryDevice;
                _context.MaintenanceTasks.Add(task);
                await _context.SaveChangesAsync();
                return await GetAll();
            }
            else
            {
                throw new ArgumentException("Error creating a maintenance task. Status, Severity and/or Description fields aren't properly configured.");
            }
        }


        //DELETE TASK BY IT'S ID
        public async Task<IEnumerable<MaintenanceTask>> DeleteTask(int id)
        {
            MaintenanceTask task = await GetTask(id);
            _context.MaintenanceTasks.Remove(task);
            await _context.SaveChangesAsync();
            return await GetAll();
        }


        //EDIT EXISTING MAINTENANCE TASK
        public async Task<MaintenanceTask> EditTask(MaintenanceTask task)
        {
            var factoryDevice = await _context.FactoryDevices
                .Where(fd => fd.Id == task.FactoryDeviceId)
                .FirstOrDefaultAsync();

            MaintenanceTask updatedTask = await _context.MaintenanceTasks.FindAsync(task.Id);
            if (updatedTask == null)
            {
                throw new ArgumentException("No maintenance task could be found for the given id.");
            }

            if (factoryDevice == null)
            {
                throw new ArgumentException("Invalid factory device id.");
            }

            if (task.status != 0 && task.severity != 0 && task.Description != "")
            {
                _context.Entry(updatedTask).State = EntityState.Detached;
                task.factoryDevice = factoryDevice;
                _context.MaintenanceTasks.Update(task);
                await _context.SaveChangesAsync();
                return await GetTask(task.Id);
            }
            else
            {
                throw new ArgumentException("Error updating a maintenance task. Status, Severity and/or Description fields aren't properly configured.");
            }

        }


        public async Task<IEnumerable<MaintenanceTask>> FilterByDevice()
        {
            throw new NotImplementedException();
        }
    }
} 

