using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public JogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            return _context.Jogo
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Usuario)
                .ToList();
        }

        public Jogo? ObterPorId(int id)
        {
            return _context.Jogo.Find(id);
        }

        public List<Jogo>? ObterPorNome(string nome)
        {
            return _context.Jogo.Where(jogo => jogo.Nome.Contains(nome)).ToList();
        }

        public byte[]? ObterImagemPorId(int id)
        {
            var imagem = _context.Jogo.Where(jogo => jogo.JogoID == id).Select(jogo => jogo.Imagem).FirstOrDefault();

            return imagem;
        }
        public void Adicionar(Jogo jogo, List<int> GenerosIds)
        {
            List<Genero> generos = _context.Genero.
                Where(genero => GenerosIds.Contains(genero.GeneroID)).ToList();

            jogo.Genero = generos;

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> GenerosIds)
        {
            Jogo? jogoBanco = _context.Jogo
                .Include(j => j.Genero)
                .FirstOrDefault(j => j.JogoID == jogo.JogoID);

            if (jogoBanco == null)
                return;

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Descricao = jogo.Descricao;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.DataLancamento = jogo.DataLancamento;
            jogoBanco.Plataforma = jogo.Plataforma;
            jogoBanco.ClassificacaoIndicativa = jogo.ClassificacaoIndicativa;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
                jogoBanco.Imagem = jogo.Imagem;

            if (jogo.StatusJogo.HasValue)
                jogoBanco.StatusJogo = jogo.StatusJogo;

            var generos = _context.Genero
                .Where(g => GenerosIds.Contains(g.GeneroID))
                .ToList();

            jogoBanco.Genero.Clear();

            foreach (var genero in generos)
                jogoBanco.Genero.Add(genero);

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Jogo? jogo = _context.Jogo.Where(jogo => jogo.JogoID == id).FirstOrDefault();

            if (jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }


    }
}
