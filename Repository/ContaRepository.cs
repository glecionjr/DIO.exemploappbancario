using System;
using System.Collections.Generic;
using DIO.AppTransferenciaBancaria.Entities;
using DIO.AppTransferenciaBancaria.Entities.Exceptions;

namespace DIO.AppTransferenciaBancaria.Repository
{

    public static class ContaRepository
    {
        private static List<Conta> contas = new List<Conta>();

        public static void Add(Conta conta)
        {
            contas.Add(conta);
        }

        /* numeroConta: Conta que se deseja encontrar 

           return true: se encontrar a numeroConta no repositório
                  false: caso 'numeroConta' não seja encontrada
        */
        public static Conta FindContaPeloNumero(string numeroConta)
        {
            foreach (var conta in contas)
            {
                if (conta.NrConta == numeroConta)
                    return conta;                
            }

            throw new DomainException("Conta informada não existe!");
        }

        public static Conta FindContaPeloCpf(string cpf)
        {
            foreach (var conta in contas)
            {
                if (conta.Titular.Cpf == cpf)
                    return conta;
            }

            throw new DomainException("CPF de Titular não existe");
        }

        public static string GerarNumeroConta()
        {            
            return Convert.ToString(contas.Count + 1).PadLeft(8, '0');
        }

    }
}