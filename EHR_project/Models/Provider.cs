using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Models
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        [ForeignKey("users")]
        public int UserId { get; set; }

        [Column(TypeName ="varchar(20)")]
        public string First_name { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Last_name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DOB { get; set; }

        public int Experience { get; set; } = 0;

        public int Speciality { get; set; }= 0;

        [Column(TypeName = "varchar(20)")]
        public string? Position { get; set; } = string.Empty;
        [Column(TypeName = "varchar(20)")]
        public string? Mobile { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Address { get; set; }= string.Empty;

        public virtual Users users { get; set; }
    }
}

/*
emp_Id - pk integer
user_id-fk
First_name -varchar
Last_name - varchar
Emailid -varchar
Dateofbirth
Total Experience -varchar
Speciality - id
Position - varchar
Phone -integer
Address -varchar*/
