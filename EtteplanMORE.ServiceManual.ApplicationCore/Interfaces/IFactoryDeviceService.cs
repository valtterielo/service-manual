﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IFactoryDeviceService
    {
        Task<IEnumerable<FactoryDevice>> GetAll();

        Task<FactoryDevice> Get(int id);

        Task<IEnumerable<FactoryDevice>> AddTask(FactoryDevice factoryDevice);

        Task<FactoryDevice> EditTask(FactoryDevice factoryDevice);

        Task<IEnumerable<FactoryDevice>> DeleteTask(int id);
    }
}