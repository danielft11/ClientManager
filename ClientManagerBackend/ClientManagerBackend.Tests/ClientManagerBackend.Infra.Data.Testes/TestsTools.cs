using ClientManagerBackend.Dominio.Entidades;
using ClientManagerBackend.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerBackend.Testes.ClientManagerBackend.Infra.Data.Testes
{
    public static class TestsTools
    {
        public static ClientManagerContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ClientManagerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var clientes = CriarClientes();

            var context = new ClientManagerContext(options);
            context.Clientes.AddRange(clientes);
            context.SaveChanges();

            return context;
        }

        public static Cliente CreateClient() 
        {
            return new Cliente
            {
                Id = 3,
                Nome = "João da Silva",
                Cpf = "12345678900",
                Telefone = "(31) 99999-9999",
                Email = "joaosilva@teste.com",
                Nascimento = new DateTime(1985, 7, 3)
            };
        }

        public static List<Cliente> CriarClientes() 
        {
            return new List<Cliente>
            {
                new Cliente 
                { 
                    Id = 1, 
                    Nome = "José Moreira Costa", 
                    Cpf = "12345678911",
                    Telefone = "(31) 88888-8888",
                    Email = "josemoreira@teste.com",
                    Nascimento = new DateTime(1988, 12, 3)
                },
                new Cliente 
                { 
                    Id = 2, 
                    Nome = "Maria Cândida Teixeira",
                    Cpf = "12345678911",
                    Telefone = "(31) 77777-7777",
                    Email = "mariacandida@teste.com",
                    Nascimento = new DateTime(1980, 2, 15)
                }

            };
        }

    }
}
