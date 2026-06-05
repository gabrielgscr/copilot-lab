# Copilot Lab: API RESTful + CRUD UI

Este repositorio contiene un laboratorio practico para construir una aplicacion completa con ayuda de GitHub Copilot.

## Que se construye en el laboratorio

1. Una API RESTful con operaciones CRUD.
2. Una interfaz de usuario Blazor WebAssembly para gestionar datos.
3. Integracion frontend-backend para validar el flujo end-to-end.
4. Pruebas unitarias e integracion automatizadas con xUnit.

## Objetivo del laboratorio

Aprender a usar capacidades de Copilot durante el ciclo de desarrollo:

- Generacion de codigo base y estructura inicial.
- Sugerencias de endpoints, servicios y componentes.
- Refactorizacion y mejora incremental.
- Generacion de pruebas y datos de ejemplo.
- Soporte para documentacion y resolucion de errores.

## Estado actual del proyecto

- API implementada con Minimal APIs en `BancoX.Api/BancoX/Program.cs`.
- Frontend implementado con Blazor WebAssembly en `BancoX.App`.
- Modulos principales: clientes, cuentas y transacciones.
- Persistencia en memoria (sin base de datos externa), orientada a aprendizaje y practica.
- Suite de pruebas en `BancoX.Api/BancoX.Tests` con:
	- pruebas unitarias de `ClienteServicio` (`ClienteServicioTests.cs`),
	- pruebas de integracion de API con `WebApplicationFactory<Program>` (`ApiIntegrationTests.cs`).
- El proyecto de pruebas incluye `Microsoft.AspNetCore.Mvc.Testing`.
- El proyecto API expone internals a tests con `InternalsVisibleTo` en `BancoX.Api/BancoX/BancoX.csproj`.
- Se agregaron comentarios XML de documentacion en metodos del dominio (ejemplo: `TransaccionServicio`).

## Resultado de pruebas mas reciente

- Ejecucion completa de `dotnet test` en `BancoX.Api/BancoX.Tests`.
- Resultado: 19 pruebas totales, 19 correctas, 0 errores.

## Estructura principal

- `BancoX.Api`: backend (Minimal APIs), modelos y servicios en memoria.
- `BancoX.Api/BancoX.Tests`: pruebas unitarias e integracion.
- `BancoX.Api/docs/spec.md`: especificacion funcional y tecnica del dominio.
- `BancoX.App`: cliente Blazor WebAssembly que consume la API.
- `Parte 1 API`: guia de construccion de backend.
- `Parte 2 GUI`: guia de frontend e integracion.
- `Parte 3 Test`: guia de pruebas, refactoring y documentacion.

## Ejecucion local

### 1) API

Desde `BancoX.Api/BancoX`:

```bash
dotnet run
```

### 2) Frontend

Desde `BancoX.App`:

```bash
dotnet run
```

### 3) Pruebas

Desde `BancoX.Api/BancoX.Tests`:

```bash
dotnet test --verbosity normal
```

## Alcance del laboratorio

Este repo esta pensado para aprendizaje y practica. No incluye persistencia real, autenticacion/autorizacion ni hardening completo de seguridad para produccion.
