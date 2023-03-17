using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryDevicesController : Controller
    {
        private readonly IFactoryDeviceService _factoryDeviceService;

        public FactoryDevicesController(IFactoryDeviceService factoryDeviceService)
        {
            _factoryDeviceService = factoryDeviceService;
        }


        /// <summary>
        ///     HTTP GET: api/factorydevices/
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var devices = await _factoryDeviceService.GetAll();
            var res = devices.Select(fd => new FactoryDeviceDto
            {
                Id = fd.Id,
                Name = fd.Name,
                Year = fd.Year,
                Type = fd.Type

            });
            return Ok(res);  
        }

        /// <summary>
        ///     HTTP GET: api/factorydevices/1
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var fd = await _factoryDeviceService.Get(id);
                return Ok(new FactoryDeviceDto
                {
                    Id = fd.Id,
                    Name = fd.Name,
                    Year = fd.Year,
                    Type = fd.Type
                });
            }
            catch
            {
                return NotFound("Factory device not found.");
            }
        }

        /// <summary>
        ///     HTTP POST: api/factorydevices/
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FactoryDeviceDto factoryDeviceDto)
        {
            try
            {
                FactoryDevice factoryDevice = new FactoryDevice
                {
                    Id = factoryDeviceDto.Id,
                    Name = factoryDeviceDto.Name,
                    Year = factoryDeviceDto.Year,
                    Type = factoryDeviceDto.Type
                };
                var devices = await _factoryDeviceService.AddTask(factoryDevice);
                var res = devices.Select(fd => new FactoryDeviceDto
                {
                    Id = fd.Id,
                    Name = fd.Name,
                    Year = fd.Year,
                    Type = fd.Type

                });
                return Ok(res);
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        /// <summary>
        ///     HTTP PUT: api/factorydevices/
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] FactoryDeviceDto factoryDeviceDto)
        {
            try
            {
                FactoryDevice factoryDevice = new FactoryDevice
                {
                    Id = factoryDeviceDto.Id,
                    Name = factoryDeviceDto.Name,
                    Year = factoryDeviceDto.Year,
                    Type = factoryDeviceDto.Type
                };
                var fd = await _factoryDeviceService.EditTask(factoryDevice);
                return Ok(new FactoryDeviceDto
                {
                    Id = fd.Id,
                    Name = fd.Name,
                    Year = fd.Year,
                    Type = fd.Type
                });
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        /// <summary>
        ///     HTTP DELETE: api/factorydevices/
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var devices = await _factoryDeviceService.DeleteTask(id);
                var res = devices.Select(fd => new FactoryDeviceDto
                {
                    Id = fd.Id,
                    Name = fd.Name,
                    Year = fd.Year,
                    Type = fd.Type

                });
                return Ok(res);
            }
            catch
            {
                return NotFound("Factory device not found.");
            }
        }
    }
}