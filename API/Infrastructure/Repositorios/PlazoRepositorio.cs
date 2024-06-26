using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorios
{
    public class PlazoRepositorio : BaseRepositorio<Plazo>, IPlazoRepositorio
    {
        public PlazoRepositorio(AppDbContext context) : base(context)
        {
            
        }

		public async ValueTask<Plazo> ConsultarPlazoIdeal(int nroCuotasDeseada)
		{
			return await dbSet.Where(p => nroCuotasDeseada >= p.MinimoCuotas && nroCuotasDeseada <= p.MaximaCuotas ).FirstOrDefaultAsync();
		}

		public async ValueTask<int> ConsultarPlazoMaximo()
		{
			return await dbSet.MaxAsync(p => p.MaximaCuotas);
		}

		public async ValueTask<int> ConsultarPlazoMinimo()
		{
			return await dbSet.MinAsync(p => p.MinimoCuotas);
		}
	}
}