namespace RoyalGames.Dtos.JogoDto
{
    public class LerJogoDto
    {
        public int JogoId { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public bool? StatusProduto { get; set; }

        public List<int> GenerosIds { get; set; } = new();
        public List<string> Generos { get; set; } = new();

        public int? UsuarioId {  get; set; }
        public string? UsuarioNome {  get; set; }
        public string? UsuarioEmail {  get; set; }

    }


}
