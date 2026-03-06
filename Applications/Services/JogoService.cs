using RoyalGames.Applications.Conversões;
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
                GenerosIds = jogo.Genero?.Select(g => g.GeneroID).ToList() ?? new List<int>(),
                Generos = jogo.Genero?.Select(g => g.Nome).ToList() ?? new List<string>(),
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

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            var jogoBanco = _repository.ObterPorId(id);

            if (jogoBanco == null)
                throw new DomainException("Jogo não encontrado.");

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

            if (jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            _repository.Atualizar(
                jogoBanco,
                jogoBanco.Genero.Select(g => g.GeneroID).ToList()
            );

            _logRepository.SalvarLog(log);

            return lerDto(jogoBanco);
        }

        public LerJogoDto ObterPorId(int id)
        {
            var jogo = _repository.ObterPorId(id);
            if (jogo == null) throw new DomainException("Jogo não encontrado.");
            return lerDto(jogo);
        }

        public List<LerJogoDto> ObterPorNome(string nome)
        {
            var jogos = _repository.ObterPorNome(nome);
            if (jogos == null || jogos.Count == 0) throw new DomainException("Nenhum jogo encontrado com esse nome.");
            return jogos.Select(j => lerDto(j)).ToList();
        }

        public byte[]? ObterImagemPorId(int id)
        {
            if (_repository.ObterPorId(id) == null)
            {
                throw new DomainException("Jogo não encontrado");
            }

            return _repository.ObterImagemPorId(id);
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);
            Jogo jogo = new Jogo
            {
                Nome = jogoDto.Nome,
                Descricao = jogoDto.Descricao,
                Preco = jogoDto.Preco,
                UsuarioID = usuarioId,
                Imagem = jogoDto.Imagem
            };
            _repository.Adicionar(jogo, jogoDto.CategoriasIds);
            return lerDto(jogo);
        }

        public void remover(int id)
        {
            _repository.Remover(id);
        }



    }
}