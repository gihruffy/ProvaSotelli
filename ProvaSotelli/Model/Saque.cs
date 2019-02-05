using System;
using System.Collections.Generic;
using System.Text;

namespace ProvaSotelli.Model
{
    public class Saque: Lancamento
    {
        public Saque(DateTime DataHora, int Valor)
        {
            base.DataHora = DataHora;
            base.Valor = Valor;

        }


     
    }
}
