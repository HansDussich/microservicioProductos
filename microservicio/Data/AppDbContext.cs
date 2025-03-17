using microservicio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace microservicio.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
    }
}
