using RoyalGames.Domains;
using RoyalGames.Dtos.JogoDto;
using RoyalGames.Excpetions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;
        private readonly ILog_AlteracaoJogoRepository _logRepository;

        public JogoService(IJogoRepository repository, ILog_AlteracaoJogoRepository logRepository)
        {
            _repository = repository;
            _logRepository = logRepository;
        }

        public static LerJogoDto lerDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoId = jogo.JogoID,
                Nome = jogo.Nome,
                Descricao = jogo.Descricao,
                Preco = jogo.Preco,
                StatusProduto = jogo.StatusJogo,
                CategoriasIds = jogo.Genero?.Select(g => g.GeneroID).ToList() ?? new List<int>(),
                Categorias = jogo.Genero?.Select(g => g.Nome).ToList() ?? new List<string>(),
                UsuarioId = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email
            };
        }

        public List<LerJogoDto> Listar()
        {
            var jogos = _repository.Listar();
            return jogos.Select(j => lerDto(j)).ToList();
        }

        public void ValidarCadastro(CriarJogoDto jogoDto)
        {
            if (string.IsNullOrWhiteSpace(jogoDto.Nome))
                throw new DomainException("Nome é obrigatório");

            if (jogoDto.Preco < 0)
                throw new DomainException("Preço inválido");
        }

        public LerJogoDto Atualizar(int id, CriarJogoDto jogoDto)
        {
            var jogoBanco = _repository.ObterPorId(id);
            if (jogoBanco == null) throw new DomainException("Jogo não encontrado.");

            ValidarCadastro(jogoDto);

            var log = new Log_AlteracaoJogo
            {
                JogoID = jogoBanco.JogoID,
                NomeAnterior = jogoBanco.Nome,
                PrecoAnterior = jogoBanco.Preco,
                DataAlteracao = DateTime.Now
            };

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Descricao = jogoDto.Descricao;
            jogoBanco.Preco = jogoDto.Preco;


            _repository.Atualizar(jogoBanco, jogoDto.CategoriasIds);

            _logRepository.SalvarLog(log);

            return lerDto(jogoBanco);
        }
    }
}