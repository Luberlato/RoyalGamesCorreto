using System;

namespace RoyalGames.DTOs.LogJogoDto
{
    public class LerLogJogoDto
    {
        public int AlteracaoID { get; set; }

        public int JogoID { get; set; }

        public string? NomeAnterior { get; set; }

        public decimal? PrecoAnterior { get; set; }

        public DateTime? DataAlteracao { get; set; }
    }
}