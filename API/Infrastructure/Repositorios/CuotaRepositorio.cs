using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
    public class CuotaRepositorio : BaseRepositorio<Cuota>, ICuotaRepositorio
    {
        public CuotaRepositorio(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Cuota>> AgregarVariosAsincrono(List<Cuota> cuotas)
        {
            await dbSet.AddRangeAsync(cuotas);
            return cuotas;
        }
    }
}