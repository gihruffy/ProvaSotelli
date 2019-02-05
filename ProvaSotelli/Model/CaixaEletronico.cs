using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProvaSotelli.Model
{
    public class CaixaEletronico
    {
        public List<SaldoCedula> SaldoDasNotas { get; private set; } = new List<SaldoCedula>();
        public List<Saque> Saques { get; private set; } = new List<Saque>();
        public List<Deposito> Depositos { get; private set; } = new List<Deposito>();

       // public List<Lancamento> Lancamentos = new List<Lancamento>();

        public void ExibirExtrato()
        {
            ImprimirLancamentos(Saques.ToList<Lancamento>(), "Saque");
            ImprimirLancamentos(Depositos.ToList<Lancamento>(), "Deposito");
            Console.WriteLine("\n --- Pressione qualquer tecla para continuar --- \n");
        }

        public void ExibirSaldo()
        {
            var SaldoTotal = this.CalcularValorTotal(this.SaldoDasNotas.ToArray());
            Console.WriteLine("\nSaldo do Caixa:\n");
            this.ImprimirValorCedulas(this.SaldoDasNotas.ToArray());
            Console.WriteLine("\nValor total: R$ {0}\n", SaldoTotal);
        }

        public void RealizarDeposito(int valor)
        {
            var Deposito = new Deposito(DateTime.Now, valor);
            Depositos.Add(Deposito);
            Console.WriteLine("\n Deposito Realizado com Sucesso! \n");
        }

        public void RealizarSaque(int valor)
        {
            List<SaldoCedula> cedulasSaque = this.RetornarCedulasSacadas(valor);

            var valorSaque = this.CalcularValorTotal(cedulasSaque.ToArray());
            if(valorSaque == valor)
            {
                this.RemoverCedulasSacadas(cedulasSaque.ToArray());
                var Saque = new Saque(DateTime.Now, valor);
                Saques.Add(Saque);
                Console.WriteLine("\nNotas Sacadas:\n");
                this.ImprimirValorCedulas(cedulasSaque.ToArray());
                Console.WriteLine("\nValor total do Saque: R$ {0}", valorSaque);

                Console.WriteLine("\n Saque Realizado com Sucesso! \n");
            }
            else
            {
                Console.WriteLine("\nValor Indisponivel para Saque\n");
                List<SaldoCedula> cedulasDisponiveis  = this.CedulasDisponiveisParaSaque(this.SaldoDasNotas.ToArray());
                Console.WriteLine("\nCedulas Disponiveis para Saque\n");
                this.ImprimirValorCedulas(cedulasDisponiveis.ToArray());
                var valorDisponivel = this.CalcularValorTotal(cedulasDisponiveis.ToArray());
                Console.WriteLine("\nValor Disponivel para Saque: R$ {0}", valorDisponivel);
            }

            Console.WriteLine("\n --- Pressione qualquer tecla para continuar --- \n");
        }


        #region Metódos privados Auxiliares

        private void ImprimirLancamentos(List<Lancamento> lancamentos, string tipo)
        {
            Console.WriteLine($"\nExtrato de {tipo}: \n");
            if(lancamentos != null && lancamentos.Count > 0)
            {
                foreach (var lancamento in lancamentos)
                {
                    Console.WriteLine($"{ lancamento.DataHora:dd/MM/yyyy HH:mm:ss} - {tipo} - Valor: {lancamento.Valor} ");
                }
            }
            else
            {
                Console.WriteLine($"Nenhum lançamento do tipo {tipo} foi efetuado nesse caixa eletronico \n");
            }

        }
        // Remove as cedulas sacadas da lista de cedulas disponiveis;
        private void RemoverCedulasSacadas(SaldoCedula[] cedulasSaque)
        {
            foreach (var cedulaSaque in cedulasSaque)
            {
                var saldoNotas = this.SaldoDasNotas.Find(x => x.Cedula.Valor == cedulaSaque.Cedula.Valor);
                saldoNotas.QtdeCedula -= cedulaSaque.QtdeCedula;
            }
        }

        // Imprime uma Lista de Cedulas com o Valor e Quantidade de Cada
        private void ImprimirValorCedulas(SaldoCedula[] saldoCedulas)
        {
            foreach (SaldoCedula sc in saldoCedulas)
            {
                Console.WriteLine($"Nota de {sc.Cedula.Valor.ToString() } = { sc.QtdeCedula} Cedulas ");
            }
        }

        // Retornar a Lista de Cedulas Disponiveis Para Saque
        private List<SaldoCedula> CedulasDisponiveisParaSaque(SaldoCedula[] saldoCedulas)
        {
            var listNotas = new List<SaldoCedula>();
            foreach (SaldoCedula sc in saldoCedulas)
            {
                if (sc.QtdeCedula > 0)
                {
                    listNotas.Add(sc);
                }
            }

            return listNotas;
        }

        // Calcula o Valor Total de uma lista de Cedulas
        private int CalcularValorTotal(SaldoCedula[] saldoCedulas)
        {
            var SaldoTotal = 0;
            foreach (SaldoCedula sc in saldoCedulas)
            {
                var SomaNota = sc.Cedula.Valor * sc.QtdeCedula;
                SaldoTotal += SomaNota;
            }

            return SaldoTotal;
        }

        // Retorna uma lista de Cedulas a Serem Sacadas
        private List<SaldoCedula> RetornarCedulasSacadas(int valor)
        {
            var listaCedulasSacadas = new List<SaldoCedula>();
            var listaSaldoCedula = SaldoDasNotas.OrderByDescending(x => x.Cedula.Valor).ToList();
            var valorSaque = valor;
            foreach (var saldoNotas in listaSaldoCedula.ToArray())
            {

                if (valorSaque > 0)
                {
                    var qtdeNecesaria = valorSaque / saldoNotas.Cedula.Valor;
                    var saldo = new SaldoCedula(saldoNotas.QtdeCedula, saldoNotas.Cedula);

                    if (saldo.QtdeCedula - qtdeNecesaria >= 0)
                    {
                        saldo.QtdeCedula = qtdeNecesaria;
                        valorSaque -= qtdeNecesaria * saldoNotas.Cedula.Valor;
                    }

                    listaCedulasSacadas.Add(saldo);
                }

            }

            return listaCedulasSacadas;
        }

        #endregion
    }
}
