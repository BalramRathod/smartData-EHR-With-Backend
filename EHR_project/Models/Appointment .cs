using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }


        [ForeignKey("patient")]
        public int PatientId { get; set; }


        [ForeignKey("provider")]
        public int ProviderId { get; set;}

        [Column(TypeName = "date")]
        public DateTime? AppointmentDate { get; set;}

        [Column(TypeName = "varchar(10)")]
        public string AppointmentTime { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Note { get; set; } = "-";

        [Column(TypeName = "varchar(50)")]
        public string AppointmentStatus { get; set;}

        public virtual Patient patient { get; set;}
        public virtual Provider provider { get; set;}

        


    }
}
