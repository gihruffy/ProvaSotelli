using System;
using System.Collections.Generic;
using System.Text;

namespace ProvaSotelli.Model
{
    public class SaldoCedula
    {
        public SaldoCedula(int qtdeCedula, Cedula cedula)
        {
            QtdeCedula = qtdeCedula;
            Cedula = cedula;
        }

        public int QtdeCedula { get; set; }

        public Cedula Cedula { get; set; }
    }
}
