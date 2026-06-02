using BancoX.Models;
using BancoX.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ClienteServicio>();
builder.Services.AddSingleton<CuentaServicio>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opciones =>
{
	opciones.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "API de Banco X",
		Version = "v1"
	});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("/index.html"))
	.WithName("Inicio")
	.WithOpenApi();

var clientes = app.MapGroup("/api/clientes").WithTags("Clientes");

clientes.MapGet("/", (ClienteServicio clienteServicio) => clienteServicio.ObtenerTodos())
	.WithName("ObtenerClientes")
	.Produces<IReadOnlyList<Cliente>>(StatusCodes.Status200OK)
	.WithOpenApi();

clientes.MapGet("/{id:int}", (int id, ClienteServicio clienteServicio) =>
	{
		var cliente = clienteServicio.ObtenerPorId(id);
		return cliente is null ? Results.NotFound() : Results.Ok(cliente);
	})
	.WithName("ObtenerClientePorId")
	.Produces<Cliente>(StatusCodes.Status200OK)
	.Produces(StatusCodes.Status404NotFound)
	.WithOpenApi();

clientes.MapPost("/", (ClienteDto clienteDto, ClienteServicio clienteServicio) =>
	{
		var clienteCreado = clienteServicio.Crear(clienteDto);
		return Results.Created($"/api/clientes/{clienteCreado.Id}", clienteCreado);
	})
	.WithName("CrearCliente")
	.Produces<Cliente>(StatusCodes.Status201Created)
	.Produces(StatusCodes.Status400BadRequest)
	.WithOpenApi();

clientes.MapPut("/{id:int}", (int id, ClienteDto clienteDto, ClienteServicio clienteServicio) =>
	{
		var actualizado = clienteServicio.Actualizar(id, clienteDto);
		return actualizado ? Results.NoContent() : Results.NotFound();
	})
	.WithName("ActualizarCliente")
	.Produces(StatusCodes.Status204NoContent)
	.Produces(StatusCodes.Status404NotFound)
	.WithOpenApi();

clientes.MapDelete("/{id:int}", (int id, ClienteServicio clienteServicio) =>
	{
		var eliminado = clienteServicio.Eliminar(id);
		return eliminado ? Results.NoContent() : Results.NotFound();
	})
	.WithName("EliminarCliente")
	.Produces(StatusCodes.Status204NoContent)
	.Produces(StatusCodes.Status404NotFound)
	.WithOpenApi();

var cuentas = app.MapGroup("/api/cuentas").WithTags("Cuentas");

cuentas.MapGet("/", (CuentaServicio cuentaServicio) => cuentaServicio.ObtenerTodos())
	.WithName("ObtenerCuentas")
	.Produces<IReadOnlyList<Cuenta>>(StatusCodes.Status200OK)
	.WithOpenApi();

cuentas.MapGet("/{id:int}", (int id, CuentaServicio cuentaServicio) =>
	{
		var cuenta = cuentaServicio.ObtenerPorId(id);
		return cuenta is null ? Results.NotFound() : Results.Ok(cuenta);
	})
	.WithName("ObtenerCuentaPorId")
	.Produces<Cuenta>(StatusCodes.Status200OK)
	.Produces(StatusCodes.Status404NotFound)
	.WithOpenApi();

cuentas.MapPost("/", (CuentaDto cuentaDto, CuentaServicio cuentaServicio) =>
	{
		var cuentaCreada = cuentaServicio.Crear(cuentaDto);
		return Results.Created($"/api/cuentas/{cuentaCreada.Id}", cuentaCreada);
	})
	.WithName("CrearCuenta")
	.Produces<Cuenta>(StatusCodes.Status201Created)
	.Produces(StatusCodes.Status400BadRequest)
	.WithOpenApi();

cuentas.MapPut("/{id:int}", (int id, CuentaDto cuentaDto, CuentaServicio cuentaServicio) =>
	{
		var actualizada = cuentaServicio.Actualizar(id, cuentaDto);
		return actualizada ? Results.NoContent() : Results.NotFound();
	})
	.WithName("ActualizarCuenta")
	.Produces(StatusCodes.Status204NoContent)
	.Produces(StatusCodes.Status404NotFound)
	.WithOpenApi();

cuentas.MapDelete("/{id:int}", (int id, CuentaServicio cuentaServicio) =>
	{
		var eliminada = cuentaServicio.Eliminar(id);
		return eliminada ? Results.NoContent() : Results.NotFound();
	})
	.WithName("EliminarCuenta")
	.Produces(StatusCodes.Status204NoContent)
	.Produces(StatusCodes.Status404NotFound)
	.WithOpenApi();

app.Run();
