using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Dto
{
    public class PatientDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }       
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }      
        public DateTime? DOB { get; set; }      
        public string? InsuranceNo { get; set; }
        public string Address { get; set; }
    }
}
