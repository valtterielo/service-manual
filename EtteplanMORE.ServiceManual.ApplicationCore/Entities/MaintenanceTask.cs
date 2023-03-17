using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
	public class MaintenanceTask
	{
        public enum Severity
        {
            Undefined,
            Critical,
            Important,
            Unimportant,
        }

        public enum Status
        {
            Undefined,
            Open,
            Closed,
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
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

