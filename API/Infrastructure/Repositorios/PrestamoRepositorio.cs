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
    public class PrestamoRepositorio : BaseRepositorio<Prestamo>, IPrestamoRepostorio
    {
        public PrestamoRepositorio(AppDbContext context) : base(context)
        {
            
        }
		public override async Task<IEnumerable<Prestamo>> ObtenerTodosAsincrono()
		{
			return await base.dbSet.Include(x => x.Estado).Include(x => x.cliente).Include(x => x.plazo).ToListAsync();
        }
	}
}