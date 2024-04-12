using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Web.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        // private IUserService _service;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuracion)
        {
            // _service = userService;
            _next = next;
            _configuration = configuracion;
        }

        public async Task Invoke(HttpContext context, IUsuarioServicio usuarioServicio)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            //validar algun día si alguien no ingresa el idsesion en el query q pasaria
            var idsesion = context.Request.Query["idusuariosesion"].ToString();

	

			if (token != null)
            {
				if (!String.IsNullOrEmpty(idsesion) && token != null)
				{
					await attachUserToContextConId(context, usuarioServicio, token, int.Parse(idsesion));
				}
				else
				{
					await attachUserToContext(context, usuarioServicio, token);
				}
			}
                

            await _next(context);
        }

		private async Task attachUserToContextConId(HttpContext context, IUsuarioServicio usuarioServicio, string token, int idsesion)
        {
			
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var skey = _configuration["Jwt:Key"];
				var key = Encoding.ASCII.GetBytes(skey);
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				

				var jwtToken = (JwtSecurityToken)validatedToken;
				var usuarioId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

				
				var usuario = await usuarioServicio.ObternerPorIdAsincrono(usuarioId);

				if (usuario != null && usuarioId == idsesion)
                {
					context.Items["ok"] = true;
				}                 
				else
                {
					context.Items["ok"] = null;
				}

			}
			catch
			{
				//
			}
		}


		private async Task attachUserToContext(HttpContext context, IUsuarioServicio usuarioServicio, string token)
        {
            try
            {
				var tokenHandler = new JwtSecurityTokenHandler();
                var skey = _configuration["Jwt:Key"];
                var key = Encoding.ASCII.GetBytes(skey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
				
				var jwtToken = (JwtSecurityToken)validatedToken;
                var usuarioId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                

                var usuario = await usuarioServicio.ObternerPorIdAsincrono(usuarioId);

                if (usuario != null) context.Items["ok"] = true;
            }
            catch
            {
                //
            }
        }

    }
}
