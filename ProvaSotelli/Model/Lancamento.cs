using System;
using System.Collections.Generic;
using System.Text;

namespace ProvaSotelli.Model
{
    public abstract class Lancamento
    {
        public DateTime DataHora { get; set; }
        public int Valor { get; set; }

    }
}
