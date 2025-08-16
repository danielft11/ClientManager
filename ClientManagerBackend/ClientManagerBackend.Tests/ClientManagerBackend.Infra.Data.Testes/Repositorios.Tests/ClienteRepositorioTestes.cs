using ClientManagerBackend.Dominio.Interfaces;
using ClientManagerBackend.Dominio.ValueObjects;
using ClientManagerBackend.Infra.Data.Repositorios;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ClientManagerBackend.Testes.ClientManagerBackend.Infra.Data.Testes.Repositorios.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        
        public ClienteRepositorioTestes()
        {
            var context = TestsTools.CreateContext();
            _clienteRepositorio = new ClienteRepositorio(context);
        }

        [Fact]
        public async Task GetCustomersAsync_ShouldReturn_CustomerList()
        {
            var resultado = await _clienteRepositorio.GetCustomersAsync();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Equal("José Moreira Costa", resultado[0].Nome);
            Assert.Equal("Maria Cândida Teixeira", resultado[1].Nome);
        }

        [Fact]
        public async Task GetCustomerByCpfAsync_ShouldReturn_Customer()
        {
            var resultado = await _clienteRepositorio.GetCustomerByCpfAsync("12345678911");

            Assert.NotNull(resultado);
            Assert.Equal("12345678911", resultado.Cpf);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldReturn_NewCustomerWithTheSameData()
        {
            // Arrange
            var customer = TestsTools.CreateCustomer();

            // Act
            var registeredCustomer = await _clienteRepositorio.AddCustomerAsync(customer);

            // Assert
            Assert.NotNull(registeredCustomer);
            Assert.Equal(customer, registeredCustomer);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldReturn_CustomerUpdated()
        {
            // Arrange
            var customer = TestsTools.CreateCustomer();

            var registeredCustomer = await _clienteRepositorio.AddCustomerAsync(customer);

            registeredCustomer.UpdateModel("", "10065432100", "", new EmailVO("joaosilvanovoEmail@teste.com"), new DateTime(1985, 7, 3));
             
            // Act
            var updatedCustomer = await _clienteRepositorio.UpdateCustomerAsync(registeredCustomer);

            // Assert
            var customerFromDb = await _clienteRepositorio.GetCustomerByCpfAsync("10065432100");
            Assert.NotNull(customerFromDb);
            Assert.Equal(updatedCustomer.Id, customerFromDb.Id);
            Assert.Equal("joaosilvanovoEmail@teste.com", customerFromDb.Email.Address);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldReturn_Void()
        {
            // Arrange
            var customer = TestsTools.CreateCustomer();

            var registeredCustomer = await _clienteRepositorio.AddCustomerAsync(customer);

            // Act
            await _clienteRepositorio.DeleteCustomerAsync(registeredCustomer);

            // Assert
            var customerFromDb = await _clienteRepositorio.GetCustomerByCpfAsync(customer.Cpf);
            Assert.Null(customerFromDb);
        }

    }
}
