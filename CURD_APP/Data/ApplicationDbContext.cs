using CURD_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
namespace CURD_APP.Data
{
    public class ApplicationDbContext: IdentityUserContext<IdentityUser>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Model1> Dish { get; set; }
        public DbSet<User> UserHandler { get;set; }
        public DbSet<Login> LoginHandler { get; set; }


    }
}
