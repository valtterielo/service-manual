using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EtteplanMORE.ServiceManual.ApplicationCore.Entities.MaintenanceTask;

namespace EtteplanMORE.ServiceManual.Web.Dtos
{
	public class MaintenanceTaskDto
	{
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public Severity severity { get; set; }
        [Required]
        public Status status { get; set; }
        [Required]
        [ForeignKey("FactoryDevices")]
        public int FactoryDeviceId { get; set; }
        [Required]
        public FactoryDevice factoryDevice { get; set; }
    }
}

