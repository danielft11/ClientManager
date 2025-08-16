using System.Collections.Generic;
using cli = ClientManagerBackend.Dominio.Entities.Cliente;

namespace ClientManagerBackend.Grpc.Extensions
{
    public static class Extensions
    {
        public static List<ClienteLookupModel> ToClientesLookupList(this IList<cli> clientes)
        {
            var clientesLookup = new List<ClienteLookupModel>();

            foreach (var cliente in clientes)
            {
                clientesLookup.Add(new ClienteLookupModel
                {
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    Telefone = cliente.Telefone,
                    Email = cliente.Email.Address,
                    DataNascimento = cliente.Nascimento.ToString()
                });
            }
            return clientesLookup;  

        }

    }
}
