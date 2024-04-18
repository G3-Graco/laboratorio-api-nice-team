using Core.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : ControllerBase
    {
        private IPrestamoRepostorio _servicio;
		public PrestamoController(IPrestamoRepostorio servicio)
		{
			_servicio = servicio;
		}

        

    }
}