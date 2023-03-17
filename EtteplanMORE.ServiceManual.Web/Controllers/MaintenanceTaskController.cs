using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.Web.Dtos;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Route("api/MaintenanceTasks")]
    [ApiController]
    public class MaintenanceTaskController : Controller
    {
        private IMaintenanceTaskService _maintenanceTaskService;
        private IFactoryDeviceService _factoryDeviceService;

        public MaintenanceTaskController([FromServices] IMaintenanceTaskService maintenanceTaskService, [FromServices] IFactoryDeviceService factoryDeviceService)
        {
            _maintenanceTaskService = maintenanceTaskService;
            _factoryDeviceService = factoryDeviceService;
        }

        /// <summary>
        ///     HTTP GET: api/maintenancetasks/
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            //Get a list of Maintenance Tasks
            var tasks = await _maintenanceTaskService.GetAll();
            //Convert all Tasks into DTOS
            var taskDtos = tasks.Select(mt => new MaintenanceTaskDto
            {
                Id = mt.Id,
                Description = mt.Description,
                severity = mt.severity,
                status = mt.status,
                FactoryDeviceId = mt.FactoryDeviceId,
                factoryDevice = mt.factoryDevice
            });
            //return the list of DTOS
            return Ok(taskDtos);
        }

        /// <summary>
        ///     HTTP GET: api/maintenancetasks/1
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var mt = await _maintenanceTaskService.GetTask(id);
                var res = new MaintenanceTaskDto{
                    Id = mt.Id,
                    Description = mt.Description,
                    severity = mt.severity,
                    status = mt.status,
                    FactoryDeviceId = mt.FactoryDeviceId,
                    factoryDevice = mt.factoryDevice
                };
                return Ok(res);
            }
            catch
            {
                return NotFound("Maintenance task not found.");
            }
        }

        [HttpGet]
        [Route("Filter/deviceId")]
        public async Task<ActionResult> GetByDevice(int deviceId)
        {
            var tasks = await _maintenanceTaskService.FilterByDevice(deviceId);

            var taskDtos = tasks.Select(mt => new MaintenanceTaskDto
            {
                Id = mt.Id,
                Description = mt.Description,
                severity = mt.severity,
                status = mt.status,
                FactoryDeviceId = mt.FactoryDeviceId,
                factoryDevice = mt.factoryDevice
            });
            return Ok(taskDtos);
        }


        /// <summary>
        ///     HTTP POST: api/maintenancetasks/
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MaintenanceTaskDto maintenanceTaskDto)
        {
            try
            {
                //convert DTO body into a MaintenanceTask and add it to the db
                MaintenanceTask maintenanceTask = new MaintenanceTask
                {
                    TimeStamp = DateTime.Now,
                    Description = maintenanceTaskDto.Description,
                    severity = maintenanceTaskDto.severity,
                    status = maintenanceTaskDto.status,
                    FactoryDeviceId = maintenanceTaskDto.FactoryDeviceId
                };
                var tasks = await _maintenanceTaskService.AddTask(maintenanceTask);
                //convert the newly changed list of objects in db into dtos before returning the result for the user
                var res = tasks.Select(mt => new MaintenanceTaskDto
                {
                    Id = mt.Id,
                    Description = mt.Description,
                    severity = mt.severity,
                    status = mt.status,
                    FactoryDeviceId = mt.FactoryDeviceId,
                    factoryDevice = mt.factoryDevice
                });
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     HTTP PUT: api/maintenancetasks/
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] MaintenanceTaskDto maintenanceTaskDto)
        {
            try
            {
                MaintenanceTask maintenanceTask = new MaintenanceTask
                {
                    Id = maintenanceTaskDto.Id,
                    TimeStamp = DateTime.Now,
                    Description = maintenanceTaskDto.Description,
                    severity = maintenanceTaskDto.severity,
                    status = maintenanceTaskDto.status,
                    FactoryDeviceId = maintenanceTaskDto.FactoryDeviceId,
                };
                var mt = await _maintenanceTaskService.EditTask(maintenanceTask);

                return Ok(new MaintenanceTaskDto
                {
                    Id = mt.Id,
                    Description = mt.Description,
                    severity = mt.severity,
                    status = mt.status,
                    FactoryDeviceId = mt.FactoryDeviceId,
                    factoryDevice = mt.factoryDevice
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     HTTP DELETE: api/maintenancetasks/
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var tasks = await _maintenanceTaskService.DeleteTask(id);
                var res = tasks.Select(mt => new MaintenanceTaskDto
                {
                    Id = mt.Id,
                    Description = mt.Description,
                    severity = mt.severity,
                    status = mt.status,
                    FactoryDeviceId = mt.FactoryDeviceId,
                    factoryDevice = mt.factoryDevice
                });
                return Ok(res);
            }
            catch
            {
                return NotFound("No maintenance task could be found for the given id.");
            }
        }
    }
}

