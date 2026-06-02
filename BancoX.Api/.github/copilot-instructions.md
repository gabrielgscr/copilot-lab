# Instrucciones para GitHub Copilot - Proyecto Banco X

## Idioma
- Todo el codigo, comentarios y documentacion debe estar en espanol.
- Mensajes de error en espanol.
- Nombres de variables, propiedades y metodos en espanol (excepto palabras tecnicas estandar como Get, Post, Api, Id).

## Estandares de Codigo

| Aspecto | Estandar |
|---|---|
| Tecnologia | .NET 10 (LTS) |
| Estilo de API | Minimal APIs |
| Documentacion API | Swagger / OpenAPI |
| Estilo | Convenciones de C# de Microsoft |
| Documentacion de codigo | Comentarios XML (///) en espanol |

## Nomenclatura
- Propiedades y metodos publicos: PascalCase en espanol (ObtenerClientes, CrearCuenta).
- Variables locales y parametros: camelCase en espanol (clienteId, saldoActual).
- Clases y records: PascalCase (Cliente, CuentaBancaria, ClienteServicio).
- Constantes: PascalCase (SaldoMinimo, MaximoTransferencia).
- Interfaces: prefijo I + PascalCase (IClienteServicio).

## Contexto del Proyecto
Este es un sistema bancario para Banco X que gestiona clientes, cuentas y transacciones.

Reglas de implementacion:
- Usar datos en memoria (colecciones C#), sin base de datos externa.
- Usar Minimal APIs (no Controllers).
- Usar records para DTOs cuando sea apropiado.
- Basar decisiones funcionales y de dominio en la especificacion del proyecto en docs/spec.md.
