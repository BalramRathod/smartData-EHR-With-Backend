using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Email { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? Phone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? InsuranceNo { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Address { get; set; } = string.Empty;

        public bool? isDeleted { get; set; }=false;
        public virtual Users Users { get; set; }
    }
}

