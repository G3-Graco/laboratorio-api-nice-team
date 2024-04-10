using Core.Interfaces;
using Core.Interfaces.Repositorios;
using Core.Interfaces.Servicios;
using Infrastructure.Data;
using Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.Servicios;
using System.Reflection;
using Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IUnidadDeTrabajo), typeof(UnidadDeTrabajo));
builder.Services.AddScoped(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));

builder.Services.AddScoped(typeof(IClienteRepositorio), typeof(ClienteRepositorio));
builder.Services.AddScoped(typeof(IClienteServicio), typeof(ClienteServicio));

builder.Services.AddScoped(typeof(ICuentaRepositorio), typeof(CuentaRepositorio));
builder.Services.AddScoped(typeof(ICuentaServicio), typeof(CuentaServicio));

builder.Services.AddScoped(typeof(IMovimientoRepositorio), typeof(MovimientoRepositorio));
builder.Services.AddScoped(typeof(IMovimientoServicio), typeof(MovimientoServicio));

builder.Services.AddScoped(typeof(IPagoRepositorio), typeof(PagoRepositorio));
builder.Services.AddScoped(typeof(IPagoServicio), typeof(PagoServicio));

builder.Services.AddScoped(typeof(IUsuarioRepositorio), typeof(UsuarioRepositorio));
builder.Services.AddScoped(typeof(IUsuarioServicio), typeof(UsuarioServicio));

builder.Services.AddScoped(typeof(ITipoMovimientoRepositorio), typeof(TipoMovimientoRepositorio));

builder.Services.AddScoped(typeof(IFechaActualRepositorio), typeof(FechaActualRepositorio));


builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "ASP.NET Core API para el banco \"B Banco\"",
		Description = ".NET Core API desarrollada para la gestión y uso exclusivo del banco \"B Banco\"",
		TermsOfService = new Uri("https://github.com/G3-Graco/laboratorio-api-nice-team/blob/main/LICENSE"),
		Contact = new OpenApiContact
		{
			Name = "Nice team",
			Url = new Uri("https://github.com/G3-Graco/laboratorio-api-nice-team")
		}
	});

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization",
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}

					}
				});

	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();

app.Run();
