# Copilot Lab: API RESTful + CRUD UI

[![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor WebAssembly](https://img.shields.io/badge/Frontend-Blazor%20WebAssembly-5C2D91?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![xUnit](https://img.shields.io/badge/Tests-xUnit-5E5E5E?logo=githubactions&logoColor=white)](https://xunit.net/)
[![API](https://img.shields.io/badge/API-Minimal%20APIs-0EA5E9)](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)
[![Estado de pruebas](https://img.shields.io/badge/Pruebas-19%20passing-brightgreen)](#resultado-de-pruebas-mas-reciente)

Laboratorio practico para construir una aplicacion completa (API + UI + pruebas) con ayuda de GitHub Copilot.

## Tabla de contenidos

- [Vision general](#vision-general)
- [Objetivo del laboratorio](#objetivo-del-laboratorio)
- [Stack tecnologico](#stack-tecnologico)
- [Guia del laboratorio](#guia-del-laboratorio)
- [Estado actual del proyecto](#estado-actual-del-proyecto)
- [Estructura principal](#estructura-principal)
- [Quickstart](#quickstart)
- [Resultado de pruebas mas reciente](#resultado-de-pruebas-mas-reciente)
- [Flujo sugerido de trabajo](#flujo-sugerido-de-trabajo)
- [Alcance del laboratorio](#alcance-del-laboratorio)

## Vision general

En este laboratorio se construye:

1. Una API RESTful con operaciones CRUD.
2. Una interfaz Blazor WebAssembly para crear, listar, editar y eliminar registros.
3. Integracion frontend-backend para validar el flujo end-to-end.
4. Pruebas unitarias e integracion automatizadas con xUnit.

## Objetivo del laboratorio

Practicar el uso de Copilot en tareas reales de desarrollo:

- Generacion de codigo base y estructura inicial.
- Sugerencias de endpoints, servicios y componentes.
- Refactorizacion y mejora incremental.
- Generacion de pruebas y datos de ejemplo.
- Soporte para documentacion y resolucion de errores.

## Stack tecnologico

- Backend: ASP.NET Core Minimal APIs (.NET 10).
- Frontend: Blazor WebAssembly.
- Documentacion API: Swagger / OpenAPI.
- Pruebas: xUnit + Microsoft.AspNetCore.Mvc.Testing.
- Persistencia: en memoria (sin base de datos externa).

## Guia del laboratorio

Sigue las partes en este orden:

1. Parte 1 - API
  - [Guia general](./Parte%201%20API/README.md)
  - [Instrucciones paso a paso](./Parte%201%20API/instrucciones_laboratorio.md)
2. Parte 2 - GUI
  - [Instrucciones de frontend e integracion](./Parte%202%20GUI/instrucciones_laboratorio_gui.md)
3. Parte 3 - Tests y documentacion
  - [Instrucciones de pruebas, refactoring y doc](./Parte%203%20Test/instrucciones_laboratorio_test_docs.md)

Material de referencia:

- [Especificacion funcional y tecnica](./BancoX.Api/docs/spec.md)

## Estado actual del proyecto

- API implementada en `BancoX.Api/BancoX/Program.cs`.
- Frontend implementado en `BancoX.App`.
- Modulos principales: clientes, cuentas y transacciones.
- Suite de pruebas en `BancoX.Api/BancoX.Tests`:
  - Unitarias de `ClienteServicio` en `ClienteServicioTests.cs`.
  - Integracion de API con `WebApplicationFactory<Program>` en `ApiIntegrationTests.cs`.
- El proyecto de pruebas incluye `Microsoft.AspNetCore.Mvc.Testing`.
- El proyecto API expone internals a tests con `InternalsVisibleTo` en `BancoX.Api/BancoX/BancoX.csproj`.
- Se agregaron comentarios XML de documentacion en metodos de dominio (por ejemplo en `TransaccionServicio`).

## Estructura principal

- `BancoX.Api`: backend (Minimal APIs), modelos y servicios en memoria.
- `BancoX.Api/BancoX.Tests`: pruebas unitarias e integracion.
- `BancoX.Api/docs/spec.md`: especificacion funcional y tecnica del dominio.
- `BancoX.App`: cliente Blazor WebAssembly que consume la API.
- `Parte 1 API`: guia de construccion de backend.
- `Parte 2 GUI`: guia de frontend e integracion.
- `Parte 3 Test`: guia de pruebas, refactoring y documentacion.

## Quickstart

### 1) Levantar API

Desde `BancoX.Api/BancoX`:

```bash
dotnet run
```

### 2) Levantar frontend

Desde `BancoX.App`:

```bash
dotnet run
```

### 3) Ejecutar pruebas

Desde `BancoX.Api/BancoX.Tests`:

```bash
dotnet test --verbosity normal
```

## Flujo sugerido de trabajo

1. Definir modelo y endpoints.
2. Implementar API y validar en Swagger.
3. Construir UI CRUD y conectar con la API.
4. Agregar pruebas unitarias e integracion.
5. Iterar con Copilot para refactor, validaciones y documentacion.

## Alcance del laboratorio

Este repositorio esta orientado a aprendizaje y practica.
No incluye persistencia real, autenticacion/autorizacion ni hardening completo de seguridad para produccion.
