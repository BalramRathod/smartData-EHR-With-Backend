namespace EHR_project.Dto
{
    public class AppointmentListPatientDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public string? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string? Note { get; set; } = "-";
        public string AppointmentStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
