using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
using RoyalGames.Dtos.PlataformaDtos;
using RoyalGames.Excpetions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }


        private static LerPlataformaDto MapearParaDto(Plataforma plataforma)
        {
            return new LerPlataformaDto
            {
                PlataformaId = plataforma.PlataformaID,
                Nome = plataforma.Nome
            };
        }

        public List<LerPlataformaDto> Listar()
        {
            var plataformas = _repository.Listar();
            return plataformas.Select(p => MapearParaDto(p)).ToList();
        }

        public LerPlataformaDto ObterPorId(int id)
        {
            var plataforma = _repository.ObterPorId(id);

            if (plataforma == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            return MapearParaDto(plataforma);
        }

        public LerPlataformaDto Cadastrar(CriarPlataformaDto dto)
        {

            Validacoes.ValidarPlataforma(dto);
            if (_repository.NomeExiste(dto.Nome))
            {
                throw new DomainException("Já existe uma plataforma cadastrada com este nome.");
            }

            var plataforma = new Plataforma
            {
                Nome = dto.Nome
            };

            _repository.Adicionar(plataforma);

            return MapearParaDto(plataforma);
        }

        public void Atualizar(int id, CriarPlataformaDto dto)
        {
            var plataformaExistente = _repository.ObterPorId(id);

            if (plataformaExistente == null)
            {
                throw new DomainException("Plataforma não encontrada para atualização.");
            }


            if (_repository.NomeExiste(dto.Nome, id))
            {
                throw new DomainException("Este nome de plataforma já está em uso.");
            }

            plataformaExistente.Nome = dto.Nome;

            _repository.Atualizar(plataformaExistente);
        }

        public void Deletar(int id)
        {
            var plataforma = _repository.ObterPorId(id);
            if (plataforma == null)
            {
                throw new DomainException("Não é possível deletar: Plataforma não encontrada.");
            }

            _repository.Deletar(id);
        }
    }
}