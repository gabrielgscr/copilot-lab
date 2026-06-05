## Ejemplo de desarrollo asistido con Copilot .NET 10

![GitHub Copilot](https://img.shields.io/badge/GitHub%20Copilot-Enabled-green)
![.NET](https://img.shields.io/badge/.NET-10.0%20LTS-purple)
![Minimal APIs](https://img.shields.io/badge/Minimal%20APIs-8.x-blue)


# Primer ejercicio. Base de aplicación Bancaria.

 > Los resultados de este laboratorio se encuentran disponibles también en este repositorio para referencia.

 ### Paso 1.1: Explorar con Modo Ask 🔍

> 💡 **IMPORTANTE:** Asegúrate de estar en **Modo Ask** (ícono de mensaje 💬). Este modo NO modifica archivos, solo responde preguntas.

📍 **Cómo activar Modo Ask:**
1. Abre Copilot Chat (`Ctrl+Shift+I` / `Cmd+Shift+I`)
2. Busca el selector de modo en la parte superior
3. Selecciona **"Ask"** o el ícono de mensaje

🤖 **PROMPT — Copia y pega en Copilot Chat:**

```
Soy desarrollador en Banco X y necesito diseñar un sistema bancario simple.

Ayúdame a entender:

1. ¿Qué entidades necesitaría para un sistema que gestione:
   - Clientes (nombre, email, teléfono, dirección)
   - Cuentas bancarias (tipo: ahorro/corriente, saldo, estado)
   - Transacciones (depósitos, retiros, transferencias)

2. ¿Qué endpoints REST serían necesarios para un CRUD básico?

3. ¿Cómo organizar esto usando .NET 10 con Minimal APIs?

4. ¿Cómo se integra Swagger automáticamente con Minimal APIs?
```

📝 **Observa:** Copilot responde con información detallada pero **NO crea ningún archivo**. Esto es ideal para la fase de exploración y planificación.

> 🌟 Observa cómo Copilot comprende el dominio bancario y sugiere una arquitectura coherente sin que le des detalles técnicos excesivos.

---


### Paso 1.2: Documentar la especificación del sistema 📄

> 💡 **¿Por qué este paso?** Antes de escribir código, es buena práctica capturar la especificación de lo que vas a construir en un documento dentro del repositorio. Este documento cumple dos funciones: sirve como referencia para ti y tu equipo, y es la **base para que los agentes, skills y prompt files de Copilot tengan contexto detallado** sobre qué deben construir. En desarrollo agéntico, la calidad de la especificación determina la calidad del resultado.

> 💡 **IMPORTANTE:** Cambia a **Modo Agent** (ícono de robot 🤖). Este modo **PUEDE** crear y modificar archivos.

📍 **Cómo activar Modo Agent:**
1. En Copilot Chat, busca el selector de modo
2. Selecciona **"Agent"** o el ícono de robot/chispa

🤖 **PROMPT en Modo Agent:**

```
Basándote en la conversación anterior sobre el diseño del sistema bancario, crea el archivo docs/spec.md con la especificación técnica del proyecto Banco X. (Carpeta BancoX.Api)

El documento debe incluir:

1. Resumen ejecutivo del sistema
2. Entidades del dominio con sus campos y tipos de datos
3. Lista de endpoints REST (método HTTP, ruta, descripción, códigos de respuesta)
4. Reglas de negocio (validaciones de saldo, tipos de cuenta, estados)
5. Stack tecnológico (.NET 10, Minimal APIs, xUnit)
6. Estructura de carpetas esperada del proyecto
7. Datos de ejemplo para cada entidad (3 clientes, 3 cuentas)

Formato: Markdown estructurado con tablas donde aplique.
Idioma: Español.
```

📝 **¿Por qué es importante?** Este archivo `docs/spec.md` se convierte en el **contrato** del proyecto. Cuando más adelante uses Copilot en modo Agent para generar código, modelos o tests, puedes referenciarlo en tus prompts: *"Implementa los endpoints de clientes según la especificación en docs/spec.md"*. Esto es mucho más preciso que repetir los requisitos cada vez.

> 🌟 Copilot toma la conversación del Paso 1.1 como contexto y genera un documento de especificación completo y coherente. Este patrón de **explorar → documentar → implementar** es la base del desarrollo agéntico: defines qué quieres con precisión, y luego dejas que Copilot ejecute contra esa definición.

---

### Paso 1.3: Crear instrucciones de Copilot para el proyecto

> 💡 **¿Por qué ahora?** El archivo `copilot-instructions.md` configura a Copilot para que siga los estándares del proyecto en **todos** los archivos que genere de aquí en adelante. Crearlo antes de escribir código asegura que los modelos, la API y el frontend se generen con las convenciones correctas desde el inicio. A diferencia de la especificación (`docs/spec.md`) que describe **qué** construir, las instrucciones definen **cómo** debe comportarse Copilot.

🤖 **PROMPT en Modo Agent:**

```
Crea el archivo .github/copilot-instructions.md con instrucciones para que Copilot actúe como experto en C# y .NET para  Banco X:

# Instrucciones para GitHub Copilot - Proyecto Banco X

## Idioma
- Todo el código, comentarios y documentación debe estar en **español**
- Mensajes de error en español
- Nombres de variables, propiedades y métodos en español (excepto palabras técnicas estándar como Get, Post, Api, Id)

## Estándares de Código
| Aspecto | Estándar |
|---------|----------|
| Tecnología | .NET 10 (LTS) |
| Estilo de API | Minimal APIs |
| Swagger |
| Estilo | Convenciones de C# de Microsoft |
| Documentación | Comentarios XML (///) en español |

## Nomenclatura
- Propiedades y métodos públicos: PascalCase en español (ObtenerClientes, CrearCuenta)
- Variables locales y parámetros: camelCase en español (clienteId, saldoActual)
- Clases y records: PascalCase (Cliente, CuentaBancaria, ClienteServicio)
- Constantes: PascalCase (SaldoMinimo, MaximoTransferencia)
- Interfaces: IPrefijo + PascalCase (IClienteServicio)

## Contexto del Proyecto
Este es un sistema bancario para Banco X que gestiona clientes, cuentas y transacciones. Usa datos en memoria (colecciones C#) sin base de datos externa. Usa Minimal APIs (no Controllers). Usa records para DTOs cuando sea apropiado. La especificación completa está en docs/spec.md.
```

---

### Paso 1.4: Crear estructura del proyecto

🤖 **PROMPT en Modo Agent:**

```
Crea la estructura inicial del proyecto Banco X con .NET 10.

Necesito:
- Un proyecto web con Minimal APIs usando "dotnet new web" llamado BancoX
- Carpetas Models/ y Services/ dentro del proyecto
- Un proyecto de tests con xUnit llamado BancoX.Tests usando "dotnet new xunit"
- Un archivo de solución (.slnx) que agrupe ambos proyectos
- Agrega la referencia del proyecto principal al proyecto de tests

Ejecuta los comandos de dotnet CLI necesarios para crear todo.
```

📝 **Alternativa manual** (si el agente no ejecuta):
```bash
# Crear solución y proyectos
dotnet new slnx -n BancoX
dotnet new web -n BancoX -o BancoX
dotnet new xunit -n BancoX.Tests -o BancoX.Tests

# Agregar proyectos a la solución
dotnet slnx add BancoX/BancoX.csproj
dotnet slnx add BancoX.Tests/BancoX.Tests.csproj

# Referencia del proyecto de tests al principal
dotnet add BancoX.Tests reference BancoX

# Crear carpetas de organización
mkdir BancoX/Models BancoX/Services


```

> 📝 **Nota:** En .NET las carpetas son simplemente organización. Los namespaces se definen en cada archivo `.cs` y el compilador los resuelve automáticamente — no requieren archivos especiales para funcionar como paquetes importables.

---

### Paso 1.5: Crear los modelos de datos con Copilot

Ahora vamos a ver cómo Copilot nos ayuda a escribir código a partir de **comentarios descriptivos**. En lugar de pedirle el código exacto, le daremos contexto e intención.

🤖 **PROMPT en Modo Agent:**

```
Crea el archivo BancoX/Models/Cliente.cs con el modelo de datos para los clientes de Banco X según la especificación en docs/spec.md.

El modelo debe incluir:
- Una clase Cliente con propiedades: Id (int), Nombre (string), Email (string), Telefono (string), Direccion (string)
- Un record ClienteDto para crear/actualizar (sin Id, que se genera automáticamente)
- Una clase ClienteServicio en BancoX/Services/ClienteServicio.cs que use una List<Cliente> en memoria como almacenamiento
- Datos de ejemplo precargados (3 clientes ficticios con nombres en español)
- Métodos para: ObtenerTodos, ObtenerPorId, Crear, Actualizar, Eliminar
- Cada método debe tener un comentario XML (///) en español que describa su propósito
- Registra el servicio como Singleton en el contenedor de DI (se usará en Program.cs)
```

📝 **¿La sugerencia es útil?** Observa el código generado:
- ¿Copilot usó nombres en español?
- ¿Los datos de ejemplo son coherentes con un banco?
- ¿Usó records para los DTOs?
- ¿Necesitas ajustar alguna propiedad?

> 💡 **Tip:** Si Copilot genera el código en inglés, prueba agregar al prompt: *"Recuerda seguir las instrucciones de .github/copilot-instructions.md"*. Esto refuerza las convenciones que configuraste en el Paso 1.3.

---

### Paso 1.6: Crear el modelo de cuentas bancarias

Ahora usaremos Copilot para generar el modelo de cuentas, siguiendo el mismo patrón que usamos con clientes.

🤖 **PROMPT en Modo Agent:**

```
Crea los archivos para las cuentas bancarias de Banco X siguiendo el mismo patrón de Cliente:

1. BancoX/Models/Cuenta.cs con:
   - Clase Cuenta con propiedades: Id (int), ClienteId (int), NumeroCuenta (string), TipoCuenta (string: "ahorro"/"corriente"), Saldo (decimal), Estado (string: "activa"/"inactiva"/"bloqueada"), FechaApertura (DateTime)
   - Un record CuentaDto para crear/actualizar (sin Id ni FechaApertura)

2. BancoX/Services/CuentaServicio.cs con:
   - Misma estructura que ClienteServicio
   - Datos de ejemplo que coincidan con los clientes existentes (usa los mismos ClienteId)
   - Validación: el saldo no puede ser negativo (lanzar ArgumentException)
   - Usa decimal para montos (nunca double para dinero)

Sigue el mismo patrón y estilo que Cliente.cs y ClienteServicio.cs
```

📝 **Observa:**
- ¿Copilot detectó el patrón de `ClienteServicio.cs` y generó código consistente?
- ¿Los datos de ejemplo usan `ClienteId` que coinciden con los clientes existentes?
- ¿Incluyó la validación de saldo negativo que pediste?
- ¿Usó `decimal` para los montos en lugar de `double`?

> 🌟 Al mencionar "sigue el mismo patrón que ClienteServicio.cs", Copilot analiza el archivo existente y replica su estructura. ¡Cada persona puede obtener un resultado ligeramente diferente!

---

### Paso 1.7: Crear la aplicación con Minimal APIs y Swagger

Ahora le pediremos a Copilot que genere la aplicación principal. Observa cómo con un prompt conciso y orientado a la intención, Copilot puede generar una app completa.

🤖 **PROMPT en Modo Agent:**

```
Actualiza BancoX/Program.cs como la aplicación principal de Banco X.

Configura:
1. Swagger UI con título "API de Banco X" y versión "v1"
2. Registra ClienteServicio y CuentaServicio como Singleton en el contenedor de DI
3. Habilita archivos estáticos (UseStaticFiles) para servir el frontend desde wwwroot/
4. Redirige "/" a "/index.html" con un MapGet simple
5. Endpoints Minimal API con grupos:
   - var clientes = app.MapGroup("/api/clientes").WithTags("Clientes")
     GET /           → obtener todos
     GET /{id}       → obtener por id (retorna 404 si no existe)
     POST /          → crear (retorna 201 con Location header)
     PUT /{id}       → actualizar (retorna 404 si no existe)
     DELETE /{id}    → eliminar (retorna 404 si no existe)
   - var cuentas = app.MapGroup("/api/cuentas").WithTags("Cuentas")
     Mismos endpoints CRUD
6. Usa .WithName(), .WithOpenApi() y .Produces<T>() para documentar cada endpoint
7. Agrega app.UseSwagger() y app.UseSwaggerUI()
```

> 💡 **Si la sugerencia no incluye algo que necesitas** (por ejemplo, falta la documentación Swagger o la ruta del frontend), prueba un prompt de seguimiento como: *"Agrega .WithOpenApi() y .Produces<>() a todos los endpoints para mejorar la documentación Swagger"* o *"Agrega UseStaticFiles y un redirect de / a /index.html"*. Iterar es parte natural de trabajar con Copilot.

---
### Paso 1.8: Ejecutar y explorar Swagger

🤖 **PROMPT en Modo Agent:**

```
Ejecuta la aplicación de Banco X
```

📝 **Alternativa manual:**
```bash
cd Banco X
dotnet run
```

**Abre en el navegador:** `http://localhost:5088/swagger`

> 📝 **Nota sobre el puerto:** Por defecto, `dotnet run` usa el puerto configurado en `Properties/launchSettings.json`. Si ves un puerto diferente (como 5176 o 5xxx), usa ese. Puedes forzar el puerto agregando `app.Urls.Add("http://localhost:5088");` en Program.cs o usando `dotnet run --urls "http://localhost:5000"`.

✅ **Verificar:**
- Swagger UI se muestra con el título "API de Banco X"
- Los endpoints de clientes y cuentas aparecen organizados por tags
- Puedes probar los endpoints directamente desde Swagger (botón "Try it out")
- GET `/api/clientes` retorna los clientes de ejemplo

> 🌟 ** ¡Con Swashbuckle y `.WithOpenApi()`, Swagger UI se genera **automáticamente** a partir de los tipos de tus endpoints! Prueba hacer un POST desde Swagger para crear un nuevo cliente.

---

### Paso 1.9: Agregar endpoint de transacciones (⭐ ejercicio para la clase)

> 📝 **Este paso es un ejercicio.** Es un desafío para los asistentes del taller.

Este paso es un **mini-desafío**. Usa lo que aprendiste para crear la funcionalidad de transacciones con la ayuda de Copilot.

🤖 **PROMPT sugerido (adáptalo a tu estilo):**

```
Basándote en los patrones existentes del proyecto y la especificación en docs/spec.md, crea la funcionalidad de transacciones bancarias:

1. Modelo en Models/Transaccion.cs con:
   - Propiedades: Id, CuentaOrigenId, CuentaDestinoId (nullable), Tipo (string: "deposito"/"retiro"/"transferencia"), Monto (decimal), Fecha (DateTime), Descripcion (string)
   - Un record TransaccionDto para crear (sin Id ni Fecha)
   - Validación: el monto debe ser positivo

2. Servicio en Services/TransaccionServicio.cs con datos de ejemplo

3. Registra el servicio y agrega endpoints en Program.cs con un grupo /api/transacciones y tag "Transacciones"

4. Incluye documentación OpenAPI con .WithOpenApi() en cada endpoint
```

> 💡 **Observa:** Copilot analiza los archivos existentes y genera código que **sigue los mismos patrones** que ya usaste en clientes y cuentas.

---

### 🛠️ Revisión del primer ejercicio

| Problema | Solución |
|----------|----------|
| `dotnet: command not found` | Instala el .NET 10 SDK desde https://dot.net |
| Error al compilar modelos | Verifica que los namespaces coincidan (`namespace BancoX.Models`) |
| Swagger no aparece | Verifica que `app.UseSwagger()` y `app.UseSwaggerUI()` estén en Program.cs |
| Puerto en uso | Cambia con `dotnet run --urls "http://localhost:5001"` |
| Copilot genera Controllers en vez de Minimal APIs | Refuerza con "Usa Minimal APIs, NO Controllers. Sigue .github/copilot-instructions.md" |
| Error de inyección de dependencias | Verifica que los servicios estén registrados como Singleton antes de `var app = builder.Build()` |

---

