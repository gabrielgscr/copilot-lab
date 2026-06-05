using System.Net;
using System.Net.Http.Json;
using BancoX.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BancoX.Tests;

/// <summary>
/// Pruebas de integracion para la API de Banco X usando servidor en memoria.
/// </summary>
public class ApiIntegrationTests
{
    [Fact]
    public async Task Clientes_Listar_DebeRetornar200YListaConDatos()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var respuesta = await clienteHttp.GetAsync("/api/clientes");

        Assert.Equal(HttpStatusCode.OK, respuesta.StatusCode);

        var clientes = await respuesta.Content.ReadFromJsonAsync<List<Cliente>>();
        Assert.NotNull(clientes);
        Assert.NotEmpty(clientes!);
    }

    [Fact]
    public async Task Clientes_ConsultarPorIdExistente_DebeRetornar200YCliente()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var respuesta = await clienteHttp.GetAsync("/api/clientes/1");

        Assert.Equal(HttpStatusCode.OK, respuesta.StatusCode);

        var cliente = await respuesta.Content.ReadFromJsonAsync<Cliente>();
        Assert.NotNull(cliente);
        Assert.Equal(1, cliente!.Id);
        Assert.False(string.IsNullOrWhiteSpace(cliente.Nombre));
    }

    [Fact]
    public async Task Clientes_ConsultarPorIdInexistente_DebeRetornar404()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var respuesta = await clienteHttp.GetAsync("/api/clientes/99999");

        Assert.Equal(HttpStatusCode.NotFound, respuesta.StatusCode);
    }

    [Fact]
    public async Task Clientes_Crear_ActualizarYEliminar_DebeRetornarCodigosCorrectos()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var nuevoCliente = new ClienteDto(
            "Cliente Integracion",
            "cliente.integracion@bancox.com",
            "555-7777",
            "Direccion de Integracion 123");

        var respuestaCreacion = await clienteHttp.PostAsJsonAsync("/api/clientes", nuevoCliente);
        Assert.Equal(HttpStatusCode.Created, respuestaCreacion.StatusCode);

        var clienteCreado = await respuestaCreacion.Content.ReadFromJsonAsync<Cliente>();
        Assert.NotNull(clienteCreado);
        Assert.True(clienteCreado!.Id > 0);

        var clienteActualizado = new ClienteDto(
            "Cliente Integracion Actualizado",
            "cliente.integracion.actualizado@bancox.com",
            "555-8888",
            "Direccion Actualizada 999");

        var respuestaActualizacion = await clienteHttp.PutAsJsonAsync($"/api/clientes/{clienteCreado.Id}", clienteActualizado);
        Assert.Equal(HttpStatusCode.NoContent, respuestaActualizacion.StatusCode);

        var respuestaConsulta = await clienteHttp.GetAsync($"/api/clientes/{clienteCreado.Id}");
        Assert.Equal(HttpStatusCode.OK, respuestaConsulta.StatusCode);

        var clienteConsultado = await respuestaConsulta.Content.ReadFromJsonAsync<Cliente>();
        Assert.NotNull(clienteConsultado);
        Assert.Equal("Cliente Integracion Actualizado", clienteConsultado!.Nombre);

        var respuestaEliminacion = await clienteHttp.DeleteAsync($"/api/clientes/{clienteCreado.Id}");
        Assert.Equal(HttpStatusCode.NoContent, respuestaEliminacion.StatusCode);

        var respuestaNoEncontrado = await clienteHttp.GetAsync($"/api/clientes/{clienteCreado.Id}");
        Assert.Equal(HttpStatusCode.NotFound, respuestaNoEncontrado.StatusCode);
    }

    [Fact]
    public async Task Transacciones_CrearConMontoInvalido_DebeRetornar400()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var transaccionInvalida = new TransaccionDto(
            1,
            null,
            "retiro",
            -100m,
            "Intento de retiro con monto invalido");

        var respuesta = await clienteHttp.PostAsJsonAsync("/api/transacciones", transaccionInvalida);

        Assert.Equal(HttpStatusCode.BadRequest, respuesta.StatusCode);
    }

    [Fact]
    public async Task Cuentas_ListarYConsultarPorId_DebeRetornar200()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var respuestaListado = await clienteHttp.GetAsync("/api/cuentas");
        Assert.Equal(HttpStatusCode.OK, respuestaListado.StatusCode);

        var cuentas = await respuestaListado.Content.ReadFromJsonAsync<List<Cuenta>>();
        Assert.NotNull(cuentas);
        Assert.NotEmpty(cuentas!);

        var primeraCuentaId = cuentas![0].Id;
        var respuestaConsulta = await clienteHttp.GetAsync($"/api/cuentas/{primeraCuentaId}");
        Assert.Equal(HttpStatusCode.OK, respuestaConsulta.StatusCode);

        var cuenta = await respuestaConsulta.Content.ReadFromJsonAsync<Cuenta>();
        Assert.NotNull(cuenta);
        Assert.Equal(primeraCuentaId, cuenta!.Id);
    }

    [Fact]
    public async Task Cuentas_ConsultarPorIdInexistente_DebeRetornar404()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var respuesta = await clienteHttp.GetAsync("/api/cuentas/99999");

        Assert.Equal(HttpStatusCode.NotFound, respuesta.StatusCode);
    }

    [Fact]
    public async Task Cuentas_Crear_ActualizarYEliminar_DebeResponderCodigosCorrectos()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var clienteHttp = factory.CreateClient();

        var nuevaCuenta = new CuentaDto(
            1,
            $"IT{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            "ahorro",
            1250.50m,
            "activa");

        var respuestaCreacion = await clienteHttp.PostAsJsonAsync("/api/cuentas", nuevaCuenta);
        Assert.Equal(HttpStatusCode.Created, respuestaCreacion.StatusCode);

        var cuentaCreada = await respuestaCreacion.Content.ReadFromJsonAsync<Cuenta>();
        Assert.NotNull(cuentaCreada);

        var cuentaActualizada = new CuentaDto(
            cuentaCreada!.ClienteId,
            cuentaCreada.NumeroCuenta,
            "corriente",
            1500.00m,
            "inactiva");

        var respuestaActualizacion = await clienteHttp.PutAsJsonAsync($"/api/cuentas/{cuentaCreada.Id}", cuentaActualizada);
        Assert.Equal(HttpStatusCode.NoContent, respuestaActualizacion.StatusCode);

        var respuestaConsulta = await clienteHttp.GetAsync($"/api/cuentas/{cuentaCreada.Id}");
        Assert.Equal(HttpStatusCode.OK, respuestaConsulta.StatusCode);

        var cuentaConsultada = await respuestaConsulta.Content.ReadFromJsonAsync<Cuenta>();
        Assert.NotNull(cuentaConsultada);
        Assert.Equal("corriente", cuentaConsultada!.TipoCuenta);
        Assert.Equal("inactiva", cuentaConsultada.Estado);

        var respuestaEliminacion = await clienteHttp.DeleteAsync($"/api/cuentas/{cuentaCreada.Id}");
        Assert.Equal(HttpStatusCode.NoContent, respuestaEliminacion.StatusCode);

        var respuestaNoEncontrado = await clienteHttp.GetAsync($"/api/cuentas/{cuentaCreada.Id}");
        Assert.Equal(HttpStatusCode.NotFound, respuestaNoEncontrado.StatusCode);
    }
}
