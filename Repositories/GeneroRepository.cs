using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly RoyalGamesContext _context;
        public GeneroRepository(RoyalGamesContext context) => _context = context;

        public List<Genero> Listar() => _context.Genero.ToList();

        public Genero? ObterPorId(int id) => _context.Genero.Find(id);

        public void Adicionar(Genero genero)
        {
            _context.Genero.Add(genero);
            _context.SaveChanges();
        }

        public void Atualizar(Genero genero)
        {
            var banco = ObterPorId(genero.GeneroID);
            if (banco != null)
            {
                banco.Nome = genero.Nome;
                _context.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            var genero = ObterPorId(id);
            if (genero != null)
            {
                _context.Genero.Remove(genero);
                _context.SaveChanges();
            }
        }

        public bool NomeExiste(string nome, int? id = null)
            => _context.Genero.Any(x => x.Nome == nome && x.GeneroID != id);
    }
}
