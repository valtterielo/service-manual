using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Data;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class FactoryDeviceService : IFactoryDeviceService
    {
        private readonly DataContext _context;

        public FactoryDeviceService(DataContext context)
        {
            _context = context;
        }

        //GET ALL FACTORY DEVICES
        public async Task<IEnumerable<FactoryDevice>> GetAll()
        {
            return await _context.FactoryDevices.ToListAsync();
        }

        //GET FACTORY DEVICE BY ID
        public async Task<FactoryDevice> Get(int id)
        {
            FactoryDevice factoryDevice = await _context.FactoryDevices
                .Where(fd => fd.Id == id)
                .FirstAsync();

            return factoryDevice;
        }

        //ADD A NEW FACTORY DEVICE TO THE DB
        public async Task<IEnumerable<FactoryDevice>> AddTask(FactoryDevice factoryDevice)
        {
            _context.FactoryDevices.Add(factoryDevice);
            await _context.SaveChangesAsync();
            return await GetAll();
        }

        //EDIT EXISTING FACTORY DEVICE
        public async Task<FactoryDevice> EditTask(FactoryDevice factoryDevice)
        {
            FactoryDevice device = await _context.FactoryDevices.FindAsync(factoryDevice.Id);
            _context.Entry(device).State = EntityState.Detached;
            _context.FactoryDevices.Update(factoryDevice);
            await _context.SaveChangesAsync();
            return await Get(factoryDevice.Id);
        }

        //DELETE FACTORY DEVICE BASED ON IT'S ID
        public async Task<IEnumerable<FactoryDevice>> DeleteTask(int id)
        {
            FactoryDevice factoryDevice = await Get(id);
            _context.FactoryDevices.Remove(factoryDevice);
            await _context.SaveChangesAsync();
            return await GetAll();
        }
    }
}