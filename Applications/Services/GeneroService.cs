using RoyalGames.Domains;
using RoyalGames.Dtos.GeneroDtos;
using RoyalGames.Excpetions;
using RoyalGames.Interfaces;


namespace RoyalGames.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        private static LerGeneroDto MapearParaDto(Genero genero)
        {
            return new LerGeneroDto
            {
                Id = genero.GeneroID,
                Nome = genero.Nome
            };
        }

        public List<LerGeneroDto> Listar()
        {
            var generos = _repository.Listar();
            return generos.Select(g => MapearParaDto(g)).ToList();
        }

        public LerGeneroDto ObterPorId(int id)
        {
            var genero = _repository.ObterPorId(id);

            if (genero == null)
            {
                throw new DomainException("Gênero não encontrado.");
            }

            return MapearParaDto(genero);
        }

        public LerGeneroDto Cadastrar(CriarGeneroDto dto)
        {
            if (_repository.NomeExiste(dto.Nome))
            {
                throw new DomainException("Este gênero já está cadastrado no sistema.");
            }

            var novoGenero = new Genero
            {
                Nome = dto.Nome
            };

            _repository.Adicionar(novoGenero);

            return MapearParaDto(novoGenero);
        }

        public void Atualizar(int id, CriarGeneroDto dto)
        {
            var generoBanco = _repository.ObterPorId(id);

            if (generoBanco == null)
            {
                throw new DomainException("Gênero não encontrado para atualização.");
            }

            if (_repository.NomeExiste(dto.Nome, id))
            {
                throw new DomainException("Já existe outro gênero com este nome.");
            }

            generoBanco.Nome = dto.Nome;

            _repository.Atualizar(generoBanco);
        }

        public void Deletar(int id)
        {
            var genero = _repository.ObterPorId(id);

            if (genero == null)
            {
                throw new DomainException("Não é possível excluir: Gênero não encontrado.");
            }


            _repository.Deletar(id);
        }
    }
}