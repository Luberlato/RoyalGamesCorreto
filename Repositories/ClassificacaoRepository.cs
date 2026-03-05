using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class ClassificacaoRepository : IClassificacaoRepository
    {
        private readonly RoyalGamesContext _context;
        public ClassificacaoRepository(RoyalGamesContext context) => _context = context;

        public List<ClassificacaoIndicativa> Listar() => _context.ClassificacaoIndicativa.ToList();
        public ClassificacaoIndicativa? ObterPorId(int id) => _context.ClassificacaoIndicativa.Find(id);

        public void Adicionar(ClassificacaoIndicativa classificacao)
        {
            _context.ClassificacaoIndicativa.Add(classificacao);
            _context.SaveChanges();
        }

        public void Atualizar(ClassificacaoIndicativa classificacao)
        {
            _context.ClassificacaoIndicativa.Update(classificacao);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var item = ObterPorId(id);
            if (item != null)
            {
                _context.ClassificacaoIndicativa.Remove(item);
                _context.SaveChanges();
            }
        }

        public bool FaixaExiste(string nome, int? id = null)
            => _context.ClassificacaoIndicativa.Any(x => x.Faixa == nome && x.ClassificacaoIndicativaID != id);
    }
}
