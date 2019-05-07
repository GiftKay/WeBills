using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeBills.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Gender")]
        public string gender { get; set; }
        [Required]
        [Display(Name = "First Name(s)")]
        public string fname { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string lname { get; set; }
        [Required]
        [Display(Name = "Race")]
        public string race { get; set; }
        [Required]
        [Display(Name = "Marital Status")]
        public string mstatus { get; set; }
        [Required]
        [Display(Name = "ID Number")]
        public string idno { get; set; }
        [Display(Name = "Age")]
        public int age { get; set; }
        public byte[] Uimg { get; set; }
        public ICollection<Residence> houses { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Client> clients { get; set; }
        public DbSet<Residence> residents { get; set; }
        public DbSet<bills> bill { get; set; }
    }
}