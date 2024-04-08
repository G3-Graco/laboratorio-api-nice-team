using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
    public class PlazoRepositorio : BaseRepositorio<Plazo>, IPlazoRepositorio
    {
        public PlazoRepositorio(AppDbContext context) : base(context)
        {
        }
    }
}