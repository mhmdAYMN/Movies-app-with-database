using Microsoft.EntityFrameworkCore;
using System.Data;

namespace myfirstMVC_web_App.Models
{
    public class ApplicationDbContext : DbContext  
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)   
        {
                
        } 
        public DbSet<Movie> Movies { get; set; }  
        public DbSet<Categ> Categs { get; set; }    

    }
}
