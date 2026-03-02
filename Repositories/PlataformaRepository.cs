using RoyalGames.Domains;
using RoyalGames.Interfaces;
using RoyalGames.Contexts;
using Microsoft.EntityFrameworkCore;

namespace RoyalGames.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly RoyalGamesContext _context;

        public PlataformaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataforma.ToList();
        }


        public Plataforma? ObterPorId(int id)
        {
            return _context.Plataforma.Find(id);
        }


        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataforma.Add(plataforma);
            _context.SaveChanges();
        }

 
        public void Deletar(int id)
        {
            var plataformaBuscada = _context.Plataforma.Find(id);

            if (plataformaBuscada != null)
            {
                _context.Plataforma.Remove(plataformaBuscada);
                _context.SaveChanges();
            }
        }

        public void Atualizar(Plataforma plataforma)
        {
            var plataformaBanco = _context.Plataforma.Find(plataforma.PlataformaID);

            if (plataformaBanco != null)
            {
                plataformaBanco.Nome = plataforma.Nome;
          
                _context.Plataforma.Update(plataformaBanco);
                _context.SaveChanges();
            }
        }


        public bool NomeExiste(string nome, int? id = null)
        {

            return _context.Plataforma.Any(x => x.Nome == nome && x.PlataformaID != id);
        }
    }
}