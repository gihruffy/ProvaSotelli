using System;
using System.Collections.Generic;
using System.Text;
namespace ProvaSotelli.Model
{
    public class Cedula
    {
        private List<int> CedulasPermitidas = new List<int> {5, 10, 20, 50, 100};

        public Cedula(string Nome, int Valor)
        {
            this.Nome = Nome;
            this.Valor = Valor;
   
        }

        public string Nome { get; set; }

        public int Valor { get; set; }

       
        public bool IsValid()
        {
            if (CedulasPermitidas.Contains(Valor)) return true;

            return false;
        }



    }
}
