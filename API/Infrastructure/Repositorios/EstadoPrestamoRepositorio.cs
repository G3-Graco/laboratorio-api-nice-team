using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
    public class EstadoPrestamoRepositorio : BaseRepositorio<EstadoPrestamo>, IEstadoPrestamoRepositorio
    {
        public EstadoPrestamoRepositorio(AppDbContext context) : base(context)
        {
        }
    }
}