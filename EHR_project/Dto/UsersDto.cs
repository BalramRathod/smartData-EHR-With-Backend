using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_project.Dto
{
    public class UsersDto
    {

        public string? Username { get; set; }
        public int? User_type { get; set; }  
        public string? Password { get; set; }
    }
}
