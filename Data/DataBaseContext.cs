using Microsoft.EntityFrameworkCore;
using Trainee_Task.Models;

namespace Trainee_Task.Data
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options){}

        public DbSet<PersonModel> People { get; set; }
    }
}
