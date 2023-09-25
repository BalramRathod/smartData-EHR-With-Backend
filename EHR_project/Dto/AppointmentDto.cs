using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Dto
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Note { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
