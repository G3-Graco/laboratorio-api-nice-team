namespace Core.Entidades
{
	public class Usuario
	{
		public int Id { get; set; }
		public string NombreUsuario { get; set; }
		public string Contrasena { get; set; }
		public int ClienteId { get; set; }
	}
}
