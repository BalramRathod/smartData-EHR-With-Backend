using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Models
{
    public class OTP
    {
      [Key]
      public int Id { get; set; }

      [ForeignKey("user")]
      public int UserId { get; set; }

     public int? Otp { get; set; }   

     public virtual Users user { get; set; }
    }
}
