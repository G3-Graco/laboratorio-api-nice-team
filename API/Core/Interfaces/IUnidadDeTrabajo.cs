using Core.Interfaces.Repositorios;

namespace Core.Interfaces;
public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuarioRepositorio UsuarioRepositorio { get; }
        IClienteRepositorio ClienteRepositorio { get; }
        ICuentaRepositorio CuentaRepositorio { get; }
        IMovimientoRepositorio MovimientoRepositorio { get; }
        ITipoMovimientoRepositorio TipoMovimientoRepositorio { get; }
        IPagoRepositorio PagoRepositorio { get; }
	    IFechaActualRepositorio FechaActualRepositorio { get; }
	Task<int> CommitAsync();
    }