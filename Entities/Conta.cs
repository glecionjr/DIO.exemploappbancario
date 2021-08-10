using System;
using System.Collections.Generic;

using DIO.AppTransferenciaBancaria.Entities.Enums;

namespace DIO.AppTransferenciaBancaria.Entities 
{
    public class Conta
    {
        public Conta(Titular titular, string agencia, string conta, TipoConta tipoConta, IMovimentoConta movimento)
        {
            Titular = titular;
            NrAgencia = agencia;
            NrConta = conta;            
            TipoConta = tipoConta;
            _movimentoConta = new List<IMovimentoConta>();            
            _movimentoConta.Add(movimento); /* ABERTURA */
            Limite = 0.0d;
            Saldo = 0.0d;
        }        

        public string NrConta { get; private set; }
        private string NrAgencia { get; set; }
        public double Limite { get; private set; }        
        public double Saldo { get; private set; }
        public Titular Titular { get; private set; }
        private TipoConta TipoConta { get; set; }

        private List<IMovimentoConta> _movimentoConta;
        
        public void Depositar (double valorDeposito)
        {
            this.Saldo += valorDeposito;
        }

        public void Sacar (double valorSaque)
        {
            this.Saldo -= valorSaque;
        }

        public void AdicionarMovimentoConta(IMovimentoConta movimento)
        {
            _movimentoConta.Add(movimento);
        }

        public List<IMovimentoConta> ListarMovimentacaoConta()
        {
             return _movimentoConta;
        }

        public override string ToString()
        {
            return this.Saldo.ToString();
        }

    }

}