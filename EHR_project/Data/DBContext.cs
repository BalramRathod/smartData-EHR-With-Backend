using EHR_project.Models;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace EHR_project.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Users> users { get; set; }
        public DbSet<Patient> patient { get; set; }
        public DbSet<Provider> provider { get; set; }
        public DbSet<Appointment> appointment { get; set; }
        public DbSet<SOAP> soap { get; set; }
        public DbSet<OTP> otp { get; set; }
        public DbSet<Speciality> speciality { get; set; }


    }
}
