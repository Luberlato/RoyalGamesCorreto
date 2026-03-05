namespace RoyalGames.Dtos.UsuarioDtos
{
    public class LerUsuarioDto
    {
        public int Id { get; set; }
        public string Nome {  get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool? StatusUsuario { get; set; } = true;
    }
}
