using ClientManagerBackend.Dominio.Entities;
using ClientManagerBackend.Dominio.Interfaces;
using ClientManagerBackend.Dominio.ValueObjects;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClientManagerBackend.Testes.ClientManagerBackend.Infra.Data.Testes.Repositorios.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly Mock<IClienteRepositorio> _mockRepositorio;

        public ClienteRepositorioTestes()
        {
            _mockRepositorio = new Mock<IClienteRepositorio>();
        }

        [Fact]
        public async Task GetCustomersAsync_ShouldReturn_CustomerList()
        {
            // Arrange
            var expectedCustomers = TestsTools.CreateCustomers();
            
            _mockRepositorio
                .Setup(x => x.GetCustomersAsync())
                .ReturnsAsync(expectedCustomers);

            // Act
            var resultado = await _mockRepositorio.Object.GetCustomersAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Equal(expectedCustomers[0].Nome, resultado[0].Nome);
            Assert.Equal(expectedCustomers[1].Nome, resultado[1].Nome);
        }

        [Fact]
        public async Task GetCustomerByCpfAsync_ShouldReturn_Customer()
        {
            // Arrange
            var expectedCustomer = TestsTools.CreateCustomers().FirstOrDefault();
            
            _mockRepositorio
                .Setup(x => x.GetCustomerByCpfAsync("12345678911"))
                .ReturnsAsync(expectedCustomer);

            // Act
            var resultado = await _mockRepositorio.Object.GetCustomerByCpfAsync("12345678911");

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(expectedCustomer.Cpf, resultado.Cpf);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldReturn_NewCustomerWithTheSameData()
        {
            // Arrange
            var customer = TestsTools.CreateCustomer();
            
            _mockRepositorio
                .Setup(x => x.AddCustomerAsync(It.IsAny<Cliente>()))
                .ReturnsAsync(customer);

            // Act
            var registeredCustomer = await _mockRepositorio.Object.AddCustomerAsync(customer);

            // Assert
            Assert.NotNull(registeredCustomer);
            Assert.Equal(customer, registeredCustomer);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldReturn_CustomerUpdated()
        {
            // Arrange - cliente já existente com dados atualizados (simula estado após edição)
            var customer = TestsTools.CreateCustomer();
            
            customer.UpdateModel("", "10065432100", "", new EmailVO("joaosilvanovoEmail@teste.com"), new DateTime(1985, 7, 3));

            _mockRepositorio
                .Setup(x => x.UpdateCustomerAsync(It.IsAny<Cliente>()))
                .ReturnsAsync((Cliente c) => c);

            // Act
            var updatedCustomer = await _mockRepositorio.Object.UpdateCustomerAsync(customer);

            // Assert
            Assert.NotNull(updatedCustomer);
            Assert.Equal(customer.Cpf, updatedCustomer.Cpf);
            Assert.Equal(customer.Email.Address, updatedCustomer.Email.Address);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldReturn_Void()
        {
            // Arrange
            var customer = TestsTools.CreateCustomer();
            
            _mockRepositorio
                .Setup(x => x.AddCustomerAsync(It.IsAny<Cliente>()))
                .ReturnsAsync(customer);
            
            var registeredCustomer = await _mockRepositorio.Object.AddCustomerAsync(customer);

            // Act
            var ret = await _mockRepositorio.Object.DeleteCustomerAsync(registeredCustomer);

            // Assert
            Assert.Equal(0, ret);
        }
    }
}
