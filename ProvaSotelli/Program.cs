using ProvaSotelli.Model;
using System;
using System.Collections.Generic;

namespace ProvaSotelli
{
    class Program
    {
        private static CaixaEletronico _caixaEletronico = new CaixaEletronico();
        private static List<Cedula> CedulasPermitidas = new List<Cedula>();

        static void Main(string[] args)
        {

            CarregarCedulas();

            var operacao = "0";
            while (operacao != "9")
            {
                Console.WriteLine("Qual operação deseja realiza no caixa Eletronico?");
                
                Console.WriteLine("1 - Exibir Saldo");
                Console.WriteLine("2 - Realizar Deposito");
                Console.WriteLine("3 - Realizar Saque");
                Console.WriteLine("4 - Exibir Extrato");
                Console.WriteLine("9 - Sair");

                operacao = Console.ReadLine();

                switch (operacao)
                {
                    case "1":
                        PedidoSaldo();
                        break;
                    case "2":
                        PedidoDeposito();
                        break;
                    case "3":
                        PedidoSaque();
                        break;
                    case "4":
                        PedidoExtrato();
                        break;
                    case "9":
                        break;

                    default:
                        Console.WriteLine("Por Favor digite uma informação válida");
                        operacao = "0";
                        break;
                }

            }

            Console.WriteLine("\nObrigado por utilizar o Sistema");
            Console.ReadKey();

        }


        public static void CarregarCedulas()
        {
            Console.WriteLine("Abastecendo o caixa");

            Cedula cedula5 = new Cedula("cinco", 5);
            Cedula cedula10 = new Cedula("dez", 10);
            Cedula cedula20 = new Cedula("vinte", 20);
            Cedula cedula50 = new Cedula("cinquenta", 50);
            Cedula cedula100 = new Cedula("cem", 100);

            CedulasPermitidas.Add(cedula5);
            CedulasPermitidas.Add(cedula10);
            CedulasPermitidas.Add(cedula20);
            CedulasPermitidas.Add(cedula50);
            CedulasPermitidas.Add(cedula100);

            AbastecerCaixa(cedula5, 10);
            AbastecerCaixa(cedula10, 10);
            AbastecerCaixa(cedula20, 10);
            AbastecerCaixa(cedula50, 10);
            AbastecerCaixa(cedula100, 10);

            Console.WriteLine("Caixa abastecido com sucesso!\n");
            Console.WriteLine("\n --- Pressione qualquer tecla para continuar --- \n");
            Console.ReadKey();
        }


        public static void AbastecerCaixa(Cedula cedula, int quantidade)
        {
            var saldoNotas = _caixaEletronico.SaldoDasNotas.Find(x => x.Cedula.Nome == cedula.Nome);

            if(saldoNotas != null)
            {
                saldoNotas.QtdeCedula += quantidade;
            }
            else
            {
                _caixaEletronico.SaldoDasNotas.Add(new SaldoCedula(quantidade, cedula));
            }
  
        }
        

        public static void PedidoSaldo()
        {
            _caixaEletronico.ExibirSaldo();
        }

        public static void PedidoDeposito()
        {
            var inputOperacao = "";

            while(inputOperacao != "0")
            {
                Console.WriteLine("\nPor Favor Insira as notas:");
                Console.WriteLine("5 - Nota de 5");
                Console.WriteLine("10 - Nota de 10");
                Console.WriteLine("20 - Nota de 20");
                Console.WriteLine("50 - Nota de 50");
                Console.WriteLine("100 - Nota de 100");
                Console.WriteLine("\nOu digite 0 para sair:\n");

                inputOperacao = Console.ReadLine();

                int valorCedula = 0;
                if (!Int32.TryParse(inputOperacao, out valorCedula)){
                    Console.WriteLine("\nPor Favor Informe um valor Valido\n");
                    continue;
                }

                var cedula = CedulasPermitidas.Find(x => x.Valor == valorCedula);
                if (cedula != null)
                {
                        Console.WriteLine("\nInforme a Quantidade de Cedulas\n");
                        var inputQuantidadeCedula = Console.ReadLine();
                        int quantidadeCedula = 0;
                        if (!Int32.TryParse(inputQuantidadeCedula, out quantidadeCedula))
                        {
                            Console.WriteLine("\nPor Favor Informe um valor Valido\n");
                            continue;
                        }

                        AbastecerCaixa(cedula, quantidadeCedula);
                        _caixaEletronico.RealizarDeposito(cedula.Valor * quantidadeCedula);
                }
            }
        }
        public static void PedidoSaque()
        {
            Console.WriteLine("\nPor Favor Informe o valor do Saques:");
            var inputValorSaque = Console.ReadLine();

            int valorSaque = 0;
            if (Int32.TryParse(inputValorSaque, out valorSaque))
            {
                _caixaEletronico.RealizarSaque(valorSaque);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nPor Favor Informe um valor Valido\n");
            }
        }

        public static void PedidoExtrato()
        {
            _caixaEletronico.ExibirExtrato();
            Console.ReadLine();
        }
     
        
    }
}
