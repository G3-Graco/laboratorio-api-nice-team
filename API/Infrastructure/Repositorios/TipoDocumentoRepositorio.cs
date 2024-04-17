using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
    public class TipoDocumentoRepositorio : BaseRepositorio<TipoDocumento>, ITipoDocumentoRepositorio
    {
        public TipoDocumentoRepositorio(AppDbContext context) : base(context)
        {
            
        }
    }
}