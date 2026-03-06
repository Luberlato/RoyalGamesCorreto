namespace RoyalGames.Dtos.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;

        public List<int> GenerosIds { get; set; } = new();

        public bool? StatusProduto { get; set; } = true;
    }
}
     