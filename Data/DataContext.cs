using Microsoft.EntityFrameworkCore;
using BirthdayAPI.Models;

namespace BirthdayAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Birthday> BirthdayPersons { get; set; }  
    }
}