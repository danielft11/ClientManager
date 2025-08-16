using ClientManagerBackend.Dominio.ValueObjects;
using System;

namespace ClientManagerBackend.Dominio.Entities
{
    public sealed class Cliente
    {
        public int Id { get; private set; }
        
        public string Nome { get; private set; }
        
        public string Cpf { get; private set; }
        
        public string Telefone { get; private set; }
        
        public EmailVO Email { get; private set; }
        
        public DateTime Nascimento { get; private set; }

        public Cliente(string nome, string cpf, string telefone, EmailVO email, DateTime nascimento)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.", nameof(Nome));

            CheckDateOfBirth(nascimento);

            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Email = email;
            Nascimento = nascimento;
        }

        public Cliente(int id, string nome, string cpf, string telefone, EmailVO email, DateTime nascimento) : 
            this(nome, cpf, telefone, email, nascimento)
        {
            if (id <= 0)
                throw new ArgumentException("Id é obrigatório e deve ser maior do que zero.", nameof(Id));
        }

        public void UpdateModel(string nome, string cpf, string telefone, EmailVO email, DateTime nascimento)
        {
            if (!string.IsNullOrWhiteSpace(nome))
                Nome = nome;
            
            if (!string.IsNullOrWhiteSpace(cpf))
                Cpf = cpf;
            
            if (!string.IsNullOrWhiteSpace(telefone))
                Telefone = telefone;

            UpdateEmail(email);

            CheckDateOfBirth(nascimento);

            Nascimento = nascimento;
        }

        private void UpdateEmail(EmailVO newEmail)
        {
            if (newEmail == null) 
                throw new ArgumentNullException(nameof(newEmail));

            if (Email.Equals(newEmail))
                return; 

            Email = newEmail; 
        }

        private void CheckDateOfBirth(DateTime dateOfBirth) 
        {
            if (dateOfBirth > DateTime.Now)
                throw new ArgumentException("Data de nascimento não pode ser superior à data atual.");
        }

    }
}
