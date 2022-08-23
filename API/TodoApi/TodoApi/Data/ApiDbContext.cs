using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions options) : base(options)
        {
        }

        //dbset
       public DbSet<List> Lists { get; set; }

    }
}
