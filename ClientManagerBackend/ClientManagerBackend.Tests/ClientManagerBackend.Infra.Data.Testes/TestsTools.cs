using ClientManagerBackend.Dominio.Entities;
using ClientManagerBackend.Dominio.ValueObjects;
using ClientManagerBackend.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ClientManagerBackend.Testes.ClientManagerBackend.Infra.Data.Testes
{
    public static class TestsTools
    {
        public static ClientManagerContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ClientManagerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var clientes = CreateCustomers();

            var context = new ClientManagerContext(options);
            context.Clientes.AddRange(clientes);
            context.SaveChanges();

            return context;
        }

        public static Cliente CreateCustomer() 
        {
            return new Cliente(3, "João da Silva", "12345678900", "(31) 99999-9999", new EmailVO("joaosilva@teste.com"), new DateTime(1985, 7, 3));
        }

        public static List<Cliente> CreateCustomers() 
        {
            return new List<Cliente>
            {
                new(1, "José Moreira Costa", "12345678911", "(31) 88888-8888", new EmailVO("josemoreira@teste.com"), new DateTime(1988, 12, 3)), 
                new(2, "Maria Cândida Teixeira", "12345678911", "(31) 77777-7777", new EmailVO("mariacandida@teste.com"), new DateTime(1980, 2, 15)) 
            };
        }

    }
}
