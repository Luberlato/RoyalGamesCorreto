namespace RoyalGames.Dtos.JogoDto
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }

        public List<int> CategoriasIds { get; set; } = new List<int>();

        public byte[]? Imagem { get; set; } = null!;
    }
}