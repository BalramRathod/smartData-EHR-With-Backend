using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Models
{
    public class SOAP
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("appointment")]
        public int  AppointmentId { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Subjective { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Objective { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Assessment { get; set; }
        public bool isDeleted { get; set; } = false;

        [Column(TypeName = "varchar(200)")]
        public string Plan { get; set; }
        public virtual Appointment appointment { get; set; }

        
    
    }
}
