namespace RoyalGames.Dtos.UsuarioDtos
{
    public class LerUsuarioDto
    {
        public int Id { get; set; }
        public string Nome {  get; set; }
        public string Email {  get; set; }
        public bool? StatusUsuario { get; set; } = true;
    }
}
