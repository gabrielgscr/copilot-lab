# Especificación Técnica - Proyecto Banco X

## 1. Resumen ejecutivo del sistema

Banco X es una API bancaria simple orientada a la gestión de clientes, cuentas bancarias y transacciones financieras básicas.

El sistema cubre los siguientes procesos:

- Administración de clientes: alta, consulta, actualización y baja lógica.
- Administración de cuentas: apertura, consulta, actualización de estado y cierre.
- Operaciones monetarias: depósitos, retiros y transferencias entre cuentas.
- Trazabilidad operativa: registro de transacciones con estado y fecha.

Objetivos principales:

- Mantener integridad financiera y consistencia de saldos.
- Exponer una API REST clara para consumo de canales web y móviles.
- Permitir evolución incremental con una arquitectura modular basada en .NET 10 y Minimal APIs.

---

## 2. Entidades del dominio

### 2.1 Cliente

| Campo | Tipo de dato | Requerido | Restricciones | Descripción |
|---|---|---|---|---|
| Id | Guid | Sí | Único | Identificador del cliente |
| NombreCompleto | string | Sí | Máx. 150 | Nombre completo del cliente |
| Email | string | Sí | Formato email, único | Correo principal |
| Telefono | string | Sí | Máx. 20 | Teléfono de contacto |
| Direccion | string | Sí | Máx. 250 | Dirección física |
| Estado | string (enum) | Sí | Activo, Inactivo | Estado del cliente |
| FechaCreacion | DateTime | Sí | UTC | Fecha de alta |

### 2.2 CuentaBancaria

| Campo | Tipo de dato | Requerido | Restricciones | Descripción |
|---|---|---|---|---|
| Id | Guid | Sí | Único | Identificador de cuenta |
| NumeroCuenta | string | Sí | Único, 10-20 caracteres | Número público de cuenta |
| ClienteId | Guid | Sí | FK a Cliente.Id | Titular de la cuenta |
| TipoCuenta | string (enum) | Sí | Ahorro, Corriente | Tipo de cuenta |
| Saldo | decimal(18,2) | Sí | >= 0.00 | Saldo actual |
| EstadoCuenta | string (enum) | Sí | Activa, Bloqueada, Cerrada | Estado operativo |
| Moneda | string | Sí | ISO 4217, por ejemplo MXN | Moneda de la cuenta |
| FechaApertura | DateTime | Sí | UTC | Fecha de apertura |

### 2.3 Transaccion

| Campo | Tipo de dato | Requerido | Restricciones | Descripción |
|---|---|---|---|---|
| Id | Guid | Sí | Único | Identificador de transacción |
| TipoTransaccion | string (enum) | Sí | Deposito, Retiro, Transferencia | Tipo de operación |
| CuentaOrigenId | Guid | Condicional | Requerido en retiro y transferencia | Cuenta debitada |
| CuentaDestinoId | Guid | Condicional | Requerido en depósito y transferencia | Cuenta acreditada |
| Monto | decimal(18,2) | Sí | > 0.00 | Monto de la operación |
| Estado | string (enum) | Sí | Pendiente, Confirmada, Rechazada | Resultado de la operación |
| Fecha | DateTime | Sí | UTC | Fecha de ejecución |
| Descripcion | string | No | Máx. 250 | Concepto o referencia |

### 2.4 Enums de dominio

| Enum | Valores |
|---|---|
| TipoCuenta | Ahorro, Corriente |
| EstadoCuenta | Activa, Bloqueada, Cerrada |
| TipoTransaccion | Deposito, Retiro, Transferencia |
| EstadoTransaccion | Pendiente, Confirmada, Rechazada |
| EstadoCliente | Activo, Inactivo |

---

## 3. Endpoints REST

### 3.1 Clientes

| Método | Ruta | Descripción | Códigos de respuesta |
|---|---|---|---|
| GET | /api/clientes | Lista clientes | 200, 500 |
| GET | /api/clientes/{id} | Obtiene cliente por Id | 200, 404, 500 |
| POST | /api/clientes | Crea cliente | 201, 400, 409, 500 |
| PUT | /api/clientes/{id} | Actualiza cliente | 200, 400, 404, 409, 500 |
| DELETE | /api/clientes/{id} | Baja lógica de cliente | 204, 404, 409, 500 |

### 3.2 Cuentas

| Método | Ruta | Descripción | Códigos de respuesta |
|---|---|---|---|
| GET | /api/cuentas | Lista cuentas | 200, 500 |
| GET | /api/cuentas/{id} | Obtiene cuenta por Id | 200, 404, 500 |
| GET | /api/clientes/{clienteId}/cuentas | Lista cuentas de un cliente | 200, 404, 500 |
| POST | /api/cuentas | Abre una cuenta | 201, 400, 404, 409, 500 |
| PUT | /api/cuentas/{id} | Actualiza estado o metadatos de cuenta | 200, 400, 404, 409, 500 |
| DELETE | /api/cuentas/{id} | Cierra cuenta (baja lógica) | 204, 404, 409, 500 |
| GET | /api/cuentas/{id}/saldo | Consulta saldo actual | 200, 404, 500 |

### 3.3 Transacciones

| Método | Ruta | Descripción | Códigos de respuesta |
|---|---|---|---|
| GET | /api/transacciones | Lista transacciones | 200, 500 |
| GET | /api/transacciones/{id} | Obtiene transacción por Id | 200, 404, 500 |
| GET | /api/cuentas/{cuentaId}/transacciones | Lista transacciones por cuenta | 200, 404, 500 |
| POST | /api/transacciones/deposito | Ejecuta depósito | 201, 400, 404, 409, 500 |
| POST | /api/transacciones/retiro | Ejecuta retiro | 201, 400, 404, 409, 422, 500 |
| POST | /api/transacciones/transferencia | Ejecuta transferencia | 201, 400, 404, 409, 422, 500 |

Notas de respuesta:

- 400: request inválido.
- 404: recurso no encontrado.
- 409: conflicto de estado o duplicidad.
- 422: violación de regla de negocio, por ejemplo saldo insuficiente.

---

## 4. Reglas de negocio

### 4.1 Reglas generales

- El monto de toda transacción debe ser mayor a 0.
- El email de cliente debe ser único en el sistema.
- El número de cuenta debe ser único.
- No se permite operar cuentas en estado Cerrada.

### 4.2 Reglas de saldo

- El saldo no puede quedar negativo.
- Retiro: requiere saldo suficiente en la cuenta origen.
- Transferencia: requiere saldo suficiente en cuenta origen y cuenta destino activa.
- Depósito: solo permitido en cuentas activas.

### 4.3 Reglas de tipos de cuenta

- Tipo Ahorro y Corriente son obligatorios en catálogo inicial.
- La cuenta debe tener exactamente un tipo de cuenta.
- El tipo de cuenta no se modifica tras la apertura en la versión inicial.

### 4.4 Reglas de estado

- Cliente Inactivo no puede abrir nuevas cuentas.
- Cuenta Bloqueada no permite retiros ni transferencias salientes.
- Cuenta Cerrada no permite ningún movimiento.
- Una cuenta solo puede cerrarse si su saldo es 0.00.

### 4.5 Reglas de integridad transaccional

- La transferencia debe ejecutarse de forma atómica.
- Si falla el crédito o el débito, toda la operación se revierte.
- Toda operación debe registrar fecha UTC y estado final.

---

## 5. Stack tecnológico

| Componente | Tecnología |
|---|---|
| Runtime principal | .NET 10 |
| Estilo de API | ASP.NET Core Minimal APIs |
| Persistencia | Entity Framework Core |
| Base de datos | SQL Server (recomendado para laboratorio) |
| Documentación API | Swagger / OpenAPI |
| Pruebas unitarias | xUnit |
| Mocking para pruebas | Moq (opcional) |

---

## 6. Estructura de carpetas esperada

Estructura sugerida dentro del proyecto BancoX.Api:

| Ruta | Propósito |
|---|---|
| src/BancoX.Api | Proyecto de entrada y configuración de Minimal APIs |
| src/BancoX.Api/Endpoints | Definición de grupos de endpoints |
| src/BancoX.Api/Contracts | DTOs de request y response |
| src/BancoX.Application | Casos de uso y servicios de negocio |
| src/BancoX.Domain | Entidades, enums y reglas de dominio |
| src/BancoX.Infrastructure | EF Core, DbContext, repositorios e integraciones |
| tests/BancoX.UnitTests | Pruebas unitarias con xUnit |
| docs | Documentación técnica y funcional |

Árbol de referencia:

| Nivel | Ruta ejemplo |
|---|---|
| 1 | src/BancoX.Api/Program.cs |
| 1 | src/BancoX.Api/Endpoints/ClientesEndpoints.cs |
| 1 | src/BancoX.Api/Endpoints/CuentasEndpoints.cs |
| 1 | src/BancoX.Api/Endpoints/TransaccionesEndpoints.cs |
| 1 | src/BancoX.Domain/Entities/Cliente.cs |
| 1 | src/BancoX.Domain/Entities/CuentaBancaria.cs |
| 1 | src/BancoX.Domain/Entities/Transaccion.cs |
| 1 | tests/BancoX.UnitTests/TransaccionesServiceTests.cs |

---

## 7. Datos de ejemplo

### 7.1 Clientes (3)

| Id | NombreCompleto | Email | Telefono | Direccion | Estado | FechaCreacion |
|---|---|---|---|---|---|---|
| 3f0ef29d-3f61-4da9-9b84-b8ed6b8788a1 | Ana Torres | ana.torres@bancox.com | +52-55-1010-1001 | Av. Reforma 100, CDMX | Activo | 2026-01-10T10:00:00Z |
| d42c0f17-4c9f-4df4-9e12-7eb6d27d5a2b | Luis Mendoza | luis.mendoza@bancox.com | +52-55-2020-2002 | Calle Norte 45, CDMX | Activo | 2026-01-11T10:00:00Z |
| 50ad4ab9-f1d8-4e7a-9e53-06e4d45b7d3c | Carla Ruiz | carla.ruiz@bancox.com | +52-55-3030-3003 | Blvd. Sur 220, Guadalajara | Inactivo | 2026-01-12T10:00:00Z |

### 7.2 Cuentas (3)

| Id | NumeroCuenta | ClienteId | TipoCuenta | Saldo | EstadoCuenta | Moneda | FechaApertura |
|---|---|---|---|---:|---|---|---|
| 0c21a728-4a27-4b5d-b5af-d36b4f27f1aa | 1002003001 | 3f0ef29d-3f61-4da9-9b84-b8ed6b8788a1 | Ahorro | 12500.75 | Activa | MXN | 2026-01-15T09:00:00Z |
| 78f5916d-91cc-4a9f-8d5f-cbc8b6f312bb | 1002003002 | d42c0f17-4c9f-4df4-9e12-7eb6d27d5a2b | Corriente | 5400.00 | Activa | MXN | 2026-01-16T09:00:00Z |
| e35ed939-6bb8-4e62-80e4-d5452f1234cc | 1002003003 | 50ad4ab9-f1d8-4e7a-9e53-06e4d45b7d3c | Ahorro | 0.00 | Bloqueada | MXN | 2026-01-17T09:00:00Z |

### 7.3 Transacciones (ejemplo opcional)

| Id | TipoTransaccion | CuentaOrigenId | CuentaDestinoId | Monto | Estado | Fecha | Descripcion |
|---|---|---|---|---:|---|---|---|
| 4f37e3df-0f34-4d77-a1b2-7bf0a0be99a1 | Deposito |  | 0c21a728-4a27-4b5d-b5af-d36b4f27f1aa | 1500.00 | Confirmada | 2026-02-01T12:00:00Z | Depósito en ventanilla |
| 9f8b2e0f-2dce-4f09-b464-e8b3f3a2c102 | Retiro | 78f5916d-91cc-4a9f-8d5f-cbc8b6f312bb |  | 500.00 | Confirmada | 2026-02-02T09:15:00Z | Retiro cajero |
| 0b6d2d6e-6dc9-4984-b9f6-83d1f1d3d203 | Transferencia | 0c21a728-4a27-4b5d-b5af-d36b4f27f1aa | 78f5916d-91cc-4a9f-8d5f-cbc8b6f312bb | 230.50 | Confirmada | 2026-02-03T16:45:00Z | Transferencia SPEI interna |

---

## Criterios de aceptación funcionales iniciales

- Se pueden crear y consultar clientes y cuentas sin inconsistencias de datos.
- Los retiros y transferencias rechazan operaciones con saldo insuficiente.
- Las cuentas cerradas no aceptan movimientos.
- La documentación OpenAPI refleja todos los endpoints públicos.
