using EHR_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Dto
{
    public class RegisterDto
    {
       

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DOB { get; set; }
        public string? InsuranceNo { get; set; }
        public string Address { get; set; }
        public bool? isDeleted { get; set; }=false;
        public string? Username { get; set; }
        public int? User_type { get; set; }

    }
}
