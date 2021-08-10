namespace DIO.AppTransferenciaBancaria.Entities
{
    public class Titular 
    {
        public Titular(string nome, string cpf, string telefone, string email)
        {
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Email = email;
        }

        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        private string Telefone {get; set; }
        private string Email { get; set; }

        public override string ToString()
        {
            return this.Nome + " " + this.Cpf;
        }
        
    }
}