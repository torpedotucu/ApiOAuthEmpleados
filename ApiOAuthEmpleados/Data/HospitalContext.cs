using ApiOAuthEmpleados.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthEmpleados.Data
{
    public class HospitalContext:DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> contextOptions) : base(contextOptions) { }

        public DbSet<Empleado> Empleados { get; set; }
    }
}
