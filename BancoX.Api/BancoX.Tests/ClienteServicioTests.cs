using BancoX.Models;
using BancoX.Services;
using Xunit;

namespace BancoX.Tests;

/// <summary>
/// Pruebas unitarias para <see cref="ClienteServicio"/>.
/// </summary>
public class ClienteServicioTests
{
    [Fact]
    public void ObtenerTodos_DebeRetornarListaCompletaInicial()
    {
        // Arrange
        var servicio = new ClienteServicio();

        // Act
        var clientes = servicio.ObtenerTodos();

        // Assert
        Assert.NotNull(clientes);
        Assert.Equal(3, clientes.Count);
        Assert.Contains(clientes, c => c.Id == 1);
        Assert.Contains(clientes, c => c.Id == 2);
        Assert.Contains(clientes, c => c.Id == 3);
    }

    [Fact]
    public void ObtenerPorId_ConIdValido_DebeRetornarCliente()
    {
        // Arrange
        var servicio = new ClienteServicio();

        // Act
        var cliente = servicio.ObtenerPorId(1);

        // Assert
        Assert.NotNull(cliente);
        Assert.Equal(1, cliente!.Id);
        Assert.Equal("Ana Torres", cliente.Nombre);
    }

    [Fact]
    public void ObtenerPorId_ConIdInvalido_DebeRetornarNull()
    {
        // Arrange
        var servicio = new ClienteServicio();

        // Act
        var cliente = servicio.ObtenerPorId(999);

        // Assert
        Assert.Null(cliente);
    }

    [Fact]
    public void Crear_ConDatosValidos_DebeCrearCliente()
    {
        // Arrange
        var servicio = new ClienteServicio();
        var dto = CrearDtoValido(
            nombre: "Pedro Ramos",
            email: "pedro.ramos@bancox.com",
            telefono: "555-2001",
            direccion: "Av. Central 123, CDMX");

        // Act
        var clienteCreado = servicio.Crear(dto);

        // Assert
        Assert.NotNull(clienteCreado);
        Assert.Equal(4, clienteCreado.Id);
        Assert.Equal(dto.Nombre, clienteCreado.Nombre);
        Assert.Equal(dto.Email, clienteCreado.Email);
        Assert.Equal(dto.Telefono, clienteCreado.Telefono);
        Assert.Equal(dto.Direccion, clienteCreado.Direccion);
        Assert.Equal(4, servicio.ObtenerTodos().Count);
    }

    [Fact]
    public void Crear_ConFaltaEmail_DebeCrearClienteSegunComportamientoActual()
    {
        // Arrange
        var servicio = new ClienteServicio();
        var dto = new ClienteDto(
            "Cliente Sin Email",
            null!,
            "555-3001",
            "Sin direccion");

        // Act
        var clienteCreado = servicio.Crear(dto);

        // Assert
        Assert.NotNull(clienteCreado);
        Assert.Equal(4, clienteCreado.Id);
        Assert.Null(clienteCreado.Email);
        Assert.Equal(4, servicio.ObtenerTodos().Count);
    }

    [Fact]
    public void Crear_ConFaltaNombre_DebeCrearClienteSegunComportamientoActual()
    {
        // Arrange
        var servicio = new ClienteServicio();
        var dto = new ClienteDto(
            null!,
            "sin.nombre@bancox.com",
            "555-3002",
            "Sin direccion");

        // Act
        var clienteCreado = servicio.Crear(dto);

        // Assert
        Assert.NotNull(clienteCreado);
        Assert.Equal(4, clienteCreado.Id);
        Assert.Null(clienteCreado.Nombre);
        Assert.Equal(4, servicio.ObtenerTodos().Count);
    }

    [Fact]
    public void Actualizar_ClienteExistente_DebeActualizarDatos()
    {
        // Arrange
        var servicio = new ClienteServicio();
        var dtoActualizado = CrearDtoValido(
            nombre: "Luis Mendoza Actualizado",
            email: "luis.actualizado@bancox.com",
            telefono: "555-9999",
            direccion: "Nueva direccion 999");

        // Act
        var actualizado = servicio.Actualizar(2, dtoActualizado);

        // Assert
        Assert.True(actualizado);

        var cliente = servicio.ObtenerPorId(2);
        Assert.NotNull(cliente);
        Assert.Equal(dtoActualizado.Nombre, cliente!.Nombre);
        Assert.Equal(dtoActualizado.Email, cliente.Email);
        Assert.Equal(dtoActualizado.Telefono, cliente.Telefono);
        Assert.Equal(dtoActualizado.Direccion, cliente.Direccion);
    }

    [Fact]
    public void Actualizar_ClienteNoExiste_DebeRetornarFalse()
    {
        // Arrange
        var servicio = new ClienteServicio();
        var dto = CrearDtoValido();

        // Act
        var actualizado = servicio.Actualizar(999, dto);

        // Assert
        Assert.False(actualizado);
        Assert.Equal(3, servicio.ObtenerTodos().Count);
    }

    [Fact]
    public void Eliminar_ClienteExistente_DebeEliminarCliente()
    {
        // Arrange
        var servicio = new ClienteServicio();

        // Act
        var eliminado = servicio.Eliminar(3);

        // Assert
        Assert.True(eliminado);
        Assert.Null(servicio.ObtenerPorId(3));
        Assert.Equal(2, servicio.ObtenerTodos().Count);
    }

    [Fact]
    public void Eliminar_ClienteNoExiste_DebeRetornarFalse()
    {
        // Arrange
        var servicio = new ClienteServicio();

        // Act
        var eliminado = servicio.Eliminar(999);

        // Assert
        Assert.False(eliminado);
        Assert.Equal(3, servicio.ObtenerTodos().Count);
    }

    private static ClienteDto CrearDtoValido(
        string nombre = "Cliente Prueba",
        string email = "cliente.prueba@bancox.com",
        string telefono = "555-0000",
        string direccion = "Direccion de prueba")
    {
        return new ClienteDto(nombre, email, telefono, direccion);
    }
}
