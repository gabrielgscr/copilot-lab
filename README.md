# Copilot Lab: API RESTful + CRUD UI

Este repositorio contiene un laboratorio práctico para construir una aplicación completa con ayuda de GitHub Copilot.

## Qué se construirá

1. Una API RESTful con operaciones CRUD.
2. Una interfaz de usuario simple para crear, listar, editar y eliminar registros.
3. Integración entre frontend y backend para validar el flujo end-to-end.

## Objetivo del laboratorio

Aprender a usar distintas capacidades de Copilot durante el ciclo de desarrollo:

- Generación de código base y estructura inicial.
- Sugerencias de funciones, endpoints y componentes.
- Refactorización y mejora incremental.
- Generación de pruebas y datos de ejemplo.
- Soporte para documentación y resolución de errores.

## Dinámica sugerida

1. Definir el modelo de datos y los endpoints.
2. Implementar la API y probarla.
3. Construir la UI CRUD y conectar la API.
4. Iterar con Copilot para mejorar calidad, validaciones y DX.

## Resultado esperado

Al finalizar tendrás una mini aplicación funcional y una guía práctica de cómo aprovechar Copilot en tareas reales de desarrollo.

## Estado actual del laboratorio

- API implementada con Minimal APIs en `BancoX.Api/BancoX/Program.cs`.
- Frontend implementado con Blazor WebAssembly en `BancoX.App`.
- Módulos funcionales principales: clientes, cuentas y transacciones.
- Persistencia en memoria (sin base de datos externa), orientada a aprendizaje y práctica.

## Estructura principal

- `BancoX.Api`: solución de backend, endpoints y servicios en memoria.
- `BancoX.App`: aplicación cliente Blazor WebAssembly que consume la API.
- `BancoX.Api/docs/spec.md`: especificación funcional y técnica del dominio.
- `Parte 1 API` y `Parte 2 GUI`: guías del laboratorio por etapas.
