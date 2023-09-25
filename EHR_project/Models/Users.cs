using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName ="varchar(20)")]
        public string? Username { get; set;}

        public int User_type { get; set;}

        [Column(TypeName = "varchar(50)")]
        public string? Password { get; set;}
        public bool? isValidate { get; set;}=false;
    }
}
