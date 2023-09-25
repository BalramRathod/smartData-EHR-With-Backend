using EHR_project.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EHR_project.Dto
{
    public class ProviderDto
    {
        public int UserId { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int? Experience { get; set; }
        public int? Speciality { get; set; }
        public string? Position { get; set; }  
        public string Mobile { get; set; }
        public string Address { get; set; }


    }
}

