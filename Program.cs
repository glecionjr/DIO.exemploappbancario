using System;
using System.Collections.Generic;

using DIO.AppTransferenciaBancaria.Entities;
using DIO.AppTransferenciaBancaria.Entities.Exceptions;
using DIO.AppTransferenciaBancaria.Entities.Enums;
using DIO.AppTransferenciaBancaria.Repository;

namespace DIO.AppTransferenciaBancaria
{
    public class Program
    {

        static void Main(string[] args)
        {
            /* Instancia listas de CONTAS e TITULARES para manutenção em memória */ 
            string opcao = null;                    

            do {
                Console.WriteLine("");
                Console.WriteLine(" ----------------------- SISTEMA DE APLICAÇÕES BANCÁRIAS ------------------------");
                Console.WriteLine("");

                Console.WriteLine(" ---------------- CONTA ---------------------");
                Console.WriteLine(" [1] Abertura de Conta");
                Console.WriteLine(" [2] Deposito");
                Console.WriteLine(" [3] Saque");
                Console.WriteLine(" [4] Transferir recursos");
                Console.WriteLine("");

                Console.WriteLine(" ----------------- RELATÓRIOS ---------------");
                Console.WriteLine(" [5] Listar Movimentações de uma Conta");

                opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        AbrirConta();
                        break;

                    case "2":
                        Depositar();
                        break;

                    case "3":
                        Sacar();
                        break;

                    case "4":
                        Transferir();
                        break;

                    case "5":
                        ListarMovimentosConta();
                        break;

                    default:
                        Console.Clear();
                        break;
                }


            } while(opcao != "0");

        }


        public static void AbrirConta()
        {
            Console.WriteLine(" ------------ Abertura de Conta ----------------");
            Console.WriteLine("");
            Console.WriteLine("----------- Dados do Titular ---------");
            Console.WriteLine("Nome do Titular: ");
            string nomeTitular = Console.ReadLine();
            Console.WriteLine("CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("Telefone: ");
            string telefone = Console.ReadLine();
            Console.WriteLine("E-mail: ");
            string email = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Processando...");
            Console.WriteLine("----------- Dados da Conta ---------");
            Console.WriteLine("Agência: [00001]");
            Console.WriteLine("Conta: ");
            string novaConta = ContaRepository.GerarNumeroConta();
            
            Conta c = new Conta (new Titular(nomeTitular, cpf, telefone, email), 
                                 "00001", 
                                 novaConta, 
                                 TipoConta.PESSOA_FISICA, 
                                 new MovimentoConta(TipoMovimento.ABERTURA));

            ContaRepository.Add(c);
            
            Console.WriteLine(" --------------------------------------- ");
            Console.WriteLine("Abertura de Conta concluída com sucesso! ");
            Console.WriteLine("");
            Console.WriteLine(" Titular: {0}" 
                              +" Agência: 00001 " + " Conta: {1} "
                              +" Saldo: R$ 0.00  Limite: R$ 0.00 ", nomeTitular, novaConta);
            
            Console.WriteLine(" Tecle para voltar ao menu... ");
            Console.ReadKey();
            
        }

        public static void Depositar()
        {
            try
            {
                Console.WriteLine(" ------------ DEPÓSITO ----------------");
                Console.WriteLine("");
                Console.WriteLine("Agência: [00001]");
                Console.WriteLine("Número da Conta:");
                string conta = Console.ReadLine();
                Conta contaParaDeposito = ContaRepository.FindContaPeloNumero(conta);

                Console.WriteLine("");
                Console.WriteLine("Informe valor depósito: ");
                double valorDeposito = Convert.ToDouble(Console.ReadLine());
                contaParaDeposito.Depositar(valorDeposito);
                Console.WriteLine("------- Depósito concluído com sucesso.. ");
                Console.WriteLine("Novo saldo = " + contaParaDeposito.Saldo);

                /* Adicionar movimento de DEPOSITO para esta conta */
                contaParaDeposito.AdicionarMovimentoConta(new MovimentoConta(TipoMovimento.DEPOSITAR, valorDeposito));

                Console.WriteLine("");
                Console.WriteLine(" Tecle para voltar ao menu... ");
                Console.ReadKey();

            } catch (DomainException contaNaoExiste) 
            {
                Console.WriteLine("[ERR:]== " + contaNaoExiste);
                
                Console.WriteLine(" Tecle para voltar ao menu... ");                
                Console.ReadKey();

            }

        }

        public static void Sacar()
        {
            try
            {
                Console.WriteLine(" ------------ SAQUE ----------------");
                Console.WriteLine("");
                Console.WriteLine("Agência: [00001]");
                Console.WriteLine("Número da Conta:");
                string conta = Console.ReadLine();
                Conta contaParaDeposito = ContaRepository.FindContaPeloNumero(conta);

                Console.WriteLine("");
                Console.WriteLine("Informe valor saque: ");
                double valorSaque = Convert.ToDouble(Console.ReadLine());
                contaParaDeposito.Sacar(valorSaque);
                Console.WriteLine("------- Saque concluído com sucesso.. ");
                Console.WriteLine("Novo saldo = " + contaParaDeposito.Saldo);

                /* Adicionar movimento de SAQUE para esta conta */
                contaParaDeposito.AdicionarMovimentoConta(new MovimentoConta(TipoMovimento.SACAR, valorSaque));

                Console.WriteLine("");
                Console.WriteLine(" Tecle para voltar ao menu... ");
                Console.ReadKey();

            } catch (DomainException contaNaoExiste) 
            {
                Console.WriteLine("[ERR:]== " + contaNaoExiste);
                Console.WriteLine(" Tecle para voltar ao menu... ");                
                Console.ReadKey();

            }

        }        

        public static void Transferir()
        {
            try
            {
                Console.WriteLine(" ------------ SAQUE ----------------");
                Console.WriteLine("");
                Console.WriteLine("Agência: [00001]");
                Console.WriteLine("Conta Origem:");
                string numeroContaOrigem = Console.ReadLine();
                Conta contaOrigem = ContaRepository.FindContaPeloNumero(numeroContaOrigem);

                Console.WriteLine(" ");
                Console.WriteLine("Conta Destino:");
                string numeroContaDestino = Console.ReadLine();
                Conta contaDestino = ContaRepository.FindContaPeloNumero(numeroContaDestino);


                Console.WriteLine("");
                Console.WriteLine("Informe o valor a transferir: ");
                double valor = Convert.ToDouble(Console.ReadLine());

                /*  Conta Origem */
                contaOrigem.Sacar(valor);
                contaOrigem.AdicionarMovimentoConta(new MovimentoConta(TipoMovimento.TRANSFERIR, valor));
                Console.WriteLine("");
                Console.WriteLine("Novo saldo conta Origem {0} = ", numeroContaOrigem + "  R$ " + contaOrigem.Saldo);

                /*  Conta Destino */
                contaDestino.Depositar(valor);
                contaDestino.AdicionarMovimentoConta(new MovimentoConta(TipoMovimento.DEPOSITAR, valor));
                Console.WriteLine("");
                Console.WriteLine("Novo saldo conta Destino {0} = " + numeroContaDestino + " R$ " + contaDestino.Saldo);


                Console.WriteLine("");
                Console.WriteLine(" Tecle para voltar ao menu... ");
                Console.ReadKey();

            } catch (DomainException contaNaoExiste) 
            {
                Console.WriteLine("[ERR:]== " + contaNaoExiste);
                Console.WriteLine(" Tecle para voltar ao menu... ");                
                Console.ReadKey();

            }

        }                
        private static void ListarMovimentosConta()
        {            
            try
            {
                Console.WriteLine("Informe o CPF do Titular: ");
                string cpf = Console.ReadLine();
                Conta conta = ContaRepository.FindContaPeloCpf(cpf);
                List<IMovimentoConta> movimentos = conta.ListarMovimentacaoConta();
                
                Console.WriteLine("");
                Console.WriteLine("Conta localizada... tecle para visualizar movimentações");
                Console.ReadKey();

                Console.WriteLine(" ---------- MOVIMENTAÇÕES --------- ");
                
                Console.WriteLine("Titular = ", conta.Titular.Nome);
                Console.WriteLine("Conta = ", conta.NrConta);
                Console.WriteLine("CPF = ", conta.Titular.Cpf);
                Console.WriteLine("  ");
                Console.WriteLine(" Data  -  Tipo Movimento  - Valor ");

                foreach (var movimento in movimentos)
                {
                    MovimentoConta mc = (MovimentoConta) movimento;
                    Console.WriteLine(mc.ToString());
                }

                Console.WriteLine(" Tecle para voltar ao menu... ");                
                Console.ReadKey();

            }
            catch (DomainException cpfTitularNaoExiste)
            {
                Console.WriteLine("[ERR:]== " + cpfTitularNaoExiste);
                Console.WriteLine(" Tecle para voltar ao menu... ");                
                Console.ReadKey();

            }
        }

    }
}
