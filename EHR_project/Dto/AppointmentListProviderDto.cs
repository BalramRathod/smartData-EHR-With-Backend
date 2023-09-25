using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Dto
{
    public class AppointmentListProviderDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public string? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string? Note { get; set; } = "-";
        public string AppointmentStatus { get; set; }
        public string First_name { get; set;}
        public string Last_name { get; set;}

    }
}
