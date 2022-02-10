using System;

namespace ClientManagerBackend.Dominio.Entidades
{
    public sealed class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Telefone { get; private set; }
        public string Email{ get; private set; }
        public DateTime Nascimento { get; private set; }
    }
}
