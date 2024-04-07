using Core.Interfaces;
using Core.Interfaces.Repositorios;
using Infrastructure.Repositorios;

namespace Infrastructure.Data;

public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly AppDbContext _context;
        private IUsuarioRepositorio _usuarioRepositorio;
	    private IClienteRepositorio _clienteRepositorio;
	    private ICuentaRepositorio _cuentaRepositorio;
	    private IMovimientoRepositorio _movimientoRepositorio;
	    private ITipoMovimientoRepositorio _tipoMovimientoRepositorio;
	    private IPagoRepositorio _pagoRepositorio;
	    private IFechaActualRepositorio _fechaActualRepositorio;


	public UnidadDeTrabajo(AppDbContext context)
        {
            this._context = context;
        }

	    public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ??= new UsuarioRepositorio(_context);

		public IClienteRepositorio ClienteRepositorio => _clienteRepositorio ??= new ClienteRepositorio(_context);

		public ICuentaRepositorio CuentaRepositorio => _cuentaRepositorio ??= new CuentaRepositorio(_context);

		public IMovimientoRepositorio MovimientoRepositorio => _movimientoRepositorio ??= new MovimientoRepositorio(_context);

		public ITipoMovimientoRepositorio TipoMovimientoRepositorio => _tipoMovimientoRepositorio ??= new TipoMovimientoRepositorio(_context);

		public IPagoRepositorio PagoRepositorio => _pagoRepositorio ??= new PagoRepositorio(_context);

		public IFechaActualRepositorio FechaActualRepositorio => _fechaActualRepositorio ??= new FechaActualRepositorio(_context);

		public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
	}