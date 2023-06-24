using CURD_APP.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
namespace CURD_APP.Data
{
    public class ApplicationDbContext: DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Model1> Dish { get; set; }


    }
}
