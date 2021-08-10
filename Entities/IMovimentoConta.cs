using DIO.AppTransferenciaBancaria.Entities.Enums;

namespace DIO.AppTransferenciaBancaria.Entities
{
    public interface IMovimentoConta
    {
        void AddMovimentoConta(TipoMovimento tipoMovimento, double valor);
    }
}