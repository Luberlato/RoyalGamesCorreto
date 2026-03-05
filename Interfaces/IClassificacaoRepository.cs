using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IClassificacaoRepository
    {
        List<ClassificacaoIndicativa> Listar();
        ClassificacaoIndicativa? ObterPorId(int id);
        void Adicionar(ClassificacaoIndicativa classificacao);
        void Atualizar(ClassificacaoIndicativa classificacao);
        void Deletar(int id);
        bool FaixaExiste(string nome, int? id = null);
    }
}
