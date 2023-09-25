using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Models
{
    public class Speciality
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="varchar(20)")]
        public string speciality { get; set; }
    }
}
