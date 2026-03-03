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

        public void Adicionar(Jogo jogo, List<int> GenerosIds)
        {
            List<Genero> generos = _context.Genero.
                Where(genero => GenerosIds.Contains(genero.GeneroID)).ToList();

            jogo.Genero = generos;

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }


    }
}
