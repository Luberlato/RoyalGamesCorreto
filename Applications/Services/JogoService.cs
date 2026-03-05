using RoyalGames.Domains;
using RoyalGames.Dtos.JogoDto;
using RoyalGames.Repositories;
using RoyalGames.Excpetions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public static LerJogoDto lerDto(Jogo jogo)
        {
            LerJogoDto dto = new LerJogoDto
            {

                JogoId = jogo.JogoID,
                Nome = jogo.Nome,
                Descricao = jogo.Descricao,
                Preco = jogo.Preco,
                StatusProduto = jogo.StatusJogo,

                
                CategoriasIds = jogo.Genero
                .Select(g => g.GeneroID)
                .ToList(),

                Categorias = jogo.Genero
                .Select(g => g.Nome)
                .ToList(),

                
                UsuarioId = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email
            };

            return dto;
        }

        public static void ValidarCadastro(CriarJogoDto jogoDto)
        {
            if (string.IsNullOrWhiteSpace(jogoDto.Nome))
            {
                throw new DomainException("Nome é obrigatorio");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preço inválido");
            }

            if (string.IsNullOrWhiteSpace(jogoDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatória");
            }

            if (jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma foto");
            }
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            List<LerJogoDto> jogosDto = jogos.Select(jogo => lerDto(jogo)).ToList();

            return jogosDto;
        }

        public LerJogoDto? ObterPorId(int id)
        {
            Jogo dto = _repository.ObterPorId(id);

            if (dto == null)
            {
                throw new DomainException("Jogo não identificado");
            }

            return lerDto(dto);
        }

        public List<LerJogoDto>? ObterPorNome(string nome)
        {
          List<Jogo> jogos = _repository.ObterPorNome(nome);

            if(jogos == null)
            {
                throw new DomainException("Jogo não identificado");
            }

            List<LerJogoDto> jogosDto = jogos.Select(jogo => lerDto(jogo)).ToList();

            return jogosDto;
        }

        //public LerJogoDto Adicionar(CriarJogoDto jogoDto)
        //{
            //ValidarCadastro(jogoDto);

            //Jogo jogo = new Jogo
            //{
                
            //};
        //}



    }
}
