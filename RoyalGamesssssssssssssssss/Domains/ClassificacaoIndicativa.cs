using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class ClassificacaoIndicativa
{
    public int ClassificacaoIndicativaID { get; set; }

    public string Faixa { get; set; } = null!;

    public virtual ICollection<Jogo> Jogo { get; set; } = new List<Jogo>();
}
