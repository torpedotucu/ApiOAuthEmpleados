﻿using ApiOAuthEmpleados.Data;
using ApiOAuthEmpleados.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthEmpleados.Repositories
{
    public class RepositoryHospital
    {
        private HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context=context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            List<Empleado> empleados = await this.context.Empleados.ToListAsync();
            return empleados;
        }

        public async Task<Empleado>FindEmpleadoAsync(int idEmpleado)
        {
            return await this.context.Empleados.Where(x => x.IdEmpleado==idEmpleado).FirstOrDefaultAsync();
        }

        public async Task<Empleado>LogInEmpleadoAsync(string apellido,int idEmpleado)
        {
            return await this.context.Empleados.Where(x => x.Apellidos==apellido && x.IdEmpleado==idEmpleado).FirstOrDefaultAsync();
        }
    }
}
