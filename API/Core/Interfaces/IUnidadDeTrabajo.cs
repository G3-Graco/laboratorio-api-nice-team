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
    IPlazoRepositorio PlazoRepositorio { get; }
    ICuotaRepositorio CuotaRepositorio { get; }
    IDocumentoRepositorio DocumentoRepositorio { get; }
    ITipoDocumentoRepositorio TipoDocumentoRepositorio { get; }
    IEstadoPrestamoRepositorio EstadoPrestamoRepositorio { get; }
    IPrestamoRepostorio PrestamoRepostorio { get; }
    

	Task<int> CommitAsync();
}