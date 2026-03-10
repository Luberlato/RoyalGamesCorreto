using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
    using RoyalGames.Dtos.ClassificacaoDtos;
    using RoyalGames.Excpetions;
    using RoyalGames.Interfaces;

    namespace RoyalGames.Applications.Services
    {
        public class ClassificacaoIndicativaService
        {
            private readonly IClassificacaoRepository _repository;

            public ClassificacaoIndicativaService(IClassificacaoRepository repository)
            {
                _repository = repository;
            }

            private static LerClassificacaoDto MapearParaDto(ClassificacaoIndicativa entidade)
            {
                return new LerClassificacaoDto 
                {
                    Id = entidade.ClassificacaoIndicativaID,
                    Nome = entidade.Faixa
                };
            }

            public List<LerClassificacaoDto> Listar()
            {
                return _repository.Listar()
                    .Select(c => new LerClassificacaoDto { Id = c.ClassificacaoIndicativaID, Nome = c.Faixa })
                    .ToList();
            }

            public LerClassificacaoDto ObterPorId(int id)
            {
                var classificacao = _repository.ObterPorId(id);
                if (classificacao == null) throw new DomainException("Classificação não encontrada.");

                return new LerClassificacaoDto { Id = classificacao.ClassificacaoIndicativaID, Nome = classificacao.Faixa };
            }

            public void Cadastrar(CriarClassificacaoDto dto)
            {

                Validacoes.ValidarClassificao(dto);
                if (_repository.FaixaExiste(dto.Nome))
                    throw new DomainException("Esta faixa indicativa já existe.");

                var nova = new ClassificacaoIndicativa { Faixa = dto.Nome };
                _repository.Adicionar(nova);
            }

            public void Atualizar(int id, CriarClassificacaoDto dto)
            {
                var banco = _repository.ObterPorId(id);
                if (banco == null) throw new DomainException("Classificação não encontrada.");

                if (_repository.FaixaExiste(dto.Nome, id))
                    throw new DomainException("Já existe outra classificação com esta faixa.");

                banco.Faixa = dto.Nome;
                _repository.Atualizar(banco);
            }

            public void Deletar(int id)
            {
                var banco = _repository.ObterPorId(id);
                if (banco == null) throw new DomainException("Classificação não encontrada para exclusão.");

                _repository.Deletar(id);
            }
        }
    }
