using System;
using DIO.AppTransferenciaBancaria.Entities.Enums;

namespace DIO.AppTransferenciaBancaria.Entities
{
    // TODO => Mudar para HistoricoMovimentoConta ??? ContaHistoricoMovimento ???
    public class MovimentoConta : IMovimentoConta{

        public DateTime Data { get; private set; }
        public TipoMovimento TipoMovimento { get; private set; }
        public double Valor { get; private set; }

        public MovimentoConta(TipoMovimento tipoMovimento, double valor)
        {
            Data = DateTime.Now;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public MovimentoConta(TipoMovimento tipoMovimento)
        {
            Data = DateTime.Now;
            TipoMovimento = tipoMovimento;
        }        

        public void AddMovimentoConta(TipoMovimento tipoMovimento)
        {
            Data = DateTime.Now;
            TipoMovimento = tipoMovimento;
        }

        public void AddMovimentoConta(TipoMovimento tipoMovimento, double valor)
        {
            TipoMovimento = tipoMovimento;
            Valor = valor;   
        }

        public override string ToString()
        {
            return Data + " - " + TipoMovimento + " - " + Valor;
        }

    }

}