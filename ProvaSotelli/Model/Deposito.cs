using System;
using System.Collections.Generic;
using System.Text;

namespace ProvaSotelli.Model
{
    public class Deposito: Lancamento
    {
        public Deposito(DateTime DataHora, int Valor)
        {
            base.DataHora = DataHora;
            base.Valor = Valor;

        }

    }
}
