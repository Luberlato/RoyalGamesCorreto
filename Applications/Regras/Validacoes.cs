using Microsoft.Identity.Client;
using RoyalGames.Dtos.ClassificacaoDtos;
using RoyalGames.Dtos.GeneroDtos;
using RoyalGames.Dtos.JogoDto;
using RoyalGames.Dtos.PlataformaDtos;
using RoyalGames.Dtos.UsuarioDtos;
using RoyalGames.Excpetions;

namespace RoyalGames.Applications.Regras
{
    public class Validacoes
    {
        public static void ValidarUsuario(CriarUsuarioDto usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                throw new DomainException("Usuario deve ter um nome");
            }

            if(string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
            {
                throw new DomainException("Email não valido");
            }

            if (string.IsNullOrWhiteSpace(usuario.Senha))
            {
                throw new DomainException("Senha não pode ser nula");
            }
        }

        public static void ValidarImagem(byte[] imagem)
        {
            if (imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem nula ou não valida");
            }
        }

        public static void ValidarJogo(CriarJogoDto jogo)
        {
            if (string.IsNullOrWhiteSpace(jogo.Descricao))
            {
                throw new DomainException("Descrição nula");
            }

            if (jogo.Preco < 0)
            {
                throw new DomainException("Preço invalido");
            }

            if (string.IsNullOrWhiteSpace(jogo.Nome))
            {
                throw new DomainException("Jogo nulo");
            }
        }


        public static void ValidarPlataforma(CriarPlataformaDto plataforma)
        {
            if (string.IsNullOrWhiteSpace(plataforma.Nome))
            {
                throw new DomainException("Nome não pode ser nulo");
            }
        }

        public static void ValidarGenero(CriarGeneroDto genero)
        {
            if (String.IsNullOrWhiteSpace(genero.Nome))
            {
                throw new DomainException("Nome não pode ser nulo");
            }
        }

        public static void ValidarClassificao(CriarClassificacaoDto classficacao)
        {
            if (string.IsNullOrWhiteSpace (classficacao.Nome))
            {
                throw new DomainException("Nome não pode ser nulo");
            }
        }





    }
}
