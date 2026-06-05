## Ejemplo de desarrollo asistido con Copilot .NET 10

![GitHub Copilot](https://img.shields.io/badge/GitHub%20Copilot-Enabled-green)
![.NET](https://img.shields.io/badge/.NET-10.0%20LTS-purple)
![Minimal APIs](https://img.shields.io/badge/Minimal%20APIs-.NET%2010-blue)

## 🛠️ Pre-requisitos

### Software Necesario

```bash
# Verificar instalaciones
dotnet --version    # Debe ser 10.x (ejemplo: 10.0.201)
code --version      # Visual Studio Code
git --version       # Git
```

> 📝 **NOTA:** Este ejercicio usa datos en memoria (colecciones de C#) para no requerir instalación de bases de datos. Los datos se pierden al reiniciar la aplicación, pero se cargan datos de ejemplo automáticamente al iniciar.

### Extensiones de VS Code

1. **GitHub Copilot** — Extensión principal
2. **GitHub Copilot Chat** — Chat integrado
3. **C# Dev Kit** (Microsoft) — Soporte para C# y .NET

### Cuenta de GitHub

- Necesitas una cuenta con acceso a **GitHub Copilot**
- Puede ser licencia individual, de organización o educativa

### ¿Qué es GitHub Copilot?

GitHub Copilot es un **asistente de programación impulsado por IA** que se integra directamente en tu editor de código. Funciona como un **par de programación** que:

- Sugiere líneas completas o bloques de código mientras escribes
- Entiende el contexto de tu proyecto (nombres de archivos, comentarios, código existente)
- Aprende de tus patrones y se adapta a tu estilo

### El Arte del Prompting en Copilot

La clave para obtener buenos resultados con Copilot está en **cómo describes lo que necesitas**. Los comentarios descriptivos e intencionales guían mejor a Copilot que instrucciones rígidas paso a paso.

**❌ Prompt débil:**
```
Desarrolla mi app bancaria
```

> ¿Qué está mal? No hay contexto del negocio, ni tecnología, ni qué se espera como resultado. Copilot tiene que **adivinar** todo.

**✅ Prompt efectivo:**
```csharp
// Endpoint Minimal API para obtener el saldo actual de una cuenta bancaria de Banco X
// Recibe el número de cuenta como parámetro de ruta
// Retorna el saldo disponible y la fecha de última actualización como JSON
// Incluye documentación OpenAPI con .WithTags() y .Produces<>()
```

> Observa cómo el segundo comentario le da a Copilot **contexto** (Banco X), **intención** (obtener saldo), **detalles** (parámetros y retorno) y **tecnología** (Minimal APIs con OpenAPI). Cuanto más específico seas con la intención, mejores serán las sugerencias.

> 📚 **¿Quieres más ejemplos de buenas prácticas?** Consulta el repositorio [github/awesome-copilot](https://github.com/github/awesome-copilot) — contiene instrucciones, agentes y configuraciones contribuidas por la comunidad para sacar el máximo provecho de GitHub Copilot.

### Modos de GitHub Copilot Chat

> ⚠️ **Nota importante:** La interfaz de los modos (íconos, ubicación del selector, nombres) puede variar según tu versión de VS Code y la extensión de GitHub Copilot. Si ves una interfaz diferente a la descrita aquí, consulta con el instructor o revisa la [documentación oficial](https://docs.github.com/en/copilot).

GitHub Copilot tiene tres modos principales de operación:

#### 1️⃣ Modo Ask (Preguntar) 💬

| Aspecto | Detalle |
|---------|---------|
| Ícono | 💬 Burbuja de mensaje |
| Función | Solo responde preguntas, **NO** modifica archivos |
| Uso ideal | Explorar, entender, planificar, aprender |

**Ejemplo:**
```
[Modo Ask]
"¿Cuál es la mejor forma de implementar una API REST con Minimal APIs y Swagger?"

→ Copilot EXPLICA las opciones pero NO crea archivos
```

#### 2️⃣ Modo Agent (Agente) 🤖

| Aspecto | Detalle |
|---------|---------|
| Ícono | 🤖 Robot o chispa |
| Función | **PUEDE** crear y modificar archivos automáticamente |
| Uso ideal | Implementar cambios, crear código, refactorizar |

**Ejemplo:**
```
[Modo Agent]
"Crea una API REST con Minimal APIs para gestionar clientes de Banco X"

→ Copilot CREA los archivos con todo el código
```

#### 3️⃣ Modo Plan (Planificar) 📋

| Aspecto | Detalle |
|---------|---------|
| Ícono | 📋 Lista o documento |
| Función | Genera un plan detallado **ANTES** de ejecutar |
| Uso ideal | Tareas complejas que involucran múltiples archivos |

**Ejemplo:**
```
[Modo Plan]
"Implementa la funcionalidad completa de transacciones bancarias 
con modelo, servicio y endpoints"

→ Copilot MUESTRA el plan:
  1. Crear Transaccion.cs (modelo)
  2. Crear TransaccionServicio.cs (lógica de negocio)
  3. Agregar endpoints de transacciones en Program.cs
  4. Registrar el servicio en el contenedor de DI

→ Tú APRUEBAS cada paso antes de que se ejecute
```

#### Comparativa de Modos

| Característica | Ask 💬 | Agent 🤖 | Plan 📋 |
|----------------|--------|----------|---------|
| Modifica archivos | ❌ No | ✅ Sí | ✅ Sí (con aprobación) |
| Velocidad | Rápido | Rápido | Más lento |
| Control | N/A | Bajo | Alto |
| Ideal para | Aprender | Implementar | Tareas complejas |
| Riesgo | Ninguno | Medio | Bajo |



### Personalización de Copilot: Instrucciones, Prompt Files, Agentes y Skills

GitHub Copilot se puede personalizar a varios niveles usando archivos de configuración dentro de tu repositorio. Estos son los componentes principales del sistema de personalización:

| Componente | Ubicación | Propósito | Cuándo usarlo |
|------------|-----------|-----------|---------------|
| **Instrucciones** | `.github/copilot-instructions.md` | Reglas globales que Copilot aplica **siempre** en todo el proyecto (idioma, convenciones, stack) | Desde el inicio del proyecto — se define una vez |
| **Prompt Files** | `.github/prompts/*.prompt.md` | Prompts reutilizables para tareas repetitivas, invocados manualmente con `/nombre` | Cuando repites el mismo tipo de petición frecuentemente |
| **Agentes personalizados** | `.github/agents/*.md` | Versiones especializadas de Copilot con un rol, herramientas y comportamiento definido | Cuando necesitas un "especialista" (frontend, testing, docs, seguridad) |
| **Skills** | `.github/skills/*/SKILL.md` | Paquetes de instrucciones con scripts y recursos incluidos para tareas multi-paso | Cuando la tarea requiere archivos auxiliares o scripts bundled |

Piensa en las **instrucciones** como las reglas del equipo que todos siguen, los **prompt files** como plantillas reutilizables para tareas comunes, los **agentes** como compañeros especialistas que puedes invocar según la tarea, y los **skills** como mini-runbooks que incluyen todo lo necesario para ejecutar una tarea compleja.

> 📚 Para esta práctica usaremos **instrucciones** y crearemos un **agente personalizado**. Para explorar más componentes, consulta el repositorio [github/awesome-copilot](https://github.com/github/awesome-copilot).


### Comandos Especiales (/comando)

| Comando | Descripción | Ejemplo de uso |
|---------|-------------|----------------|
| `/tests` | Genera pruebas unitarias | Selecciona código → `/tests` |
| `/doc` | Genera documentación | Selecciona función → `/doc` |
| `/fix` | Propone corrección de errores | Selecciona código con error → `/fix` |
| `/explain` | Explica código seleccionado | Selecciona código → `/explain` |



## 📚 Recursos Adicionales

### Documentación Oficial

- [GitHub Copilot Docs](https://docs.github.com/en/copilot)
- [VS Code + Copilot](https://code.visualstudio.com/docs/copilot/overview)
- [Custom Agents](https://docs.github.com/en/copilot/how-tos/use-copilot-agents/coding-agent/create-custom-agents)
- [Prompt Files](https://docs.github.com/en/copilot/tutorials/customization-library/prompt-files)
- [.NET 10 Documentation](https://learn.microsoft.com/dotnet/)
- [Minimal APIs Overview](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/overview)
- [Swashbuckle Documentation](https://learn.microsoft.com/aspnet/core/tutorials/web-api-help-pages-using-swagger)
- [xUnit Documentation](https://xunit.net/docs/getting-started/netcore/cmdline)
- [Bootstrap 5](https://getbootstrap.com/docs/5.3/)

### Patrones y Buenas Prácticas

- [C# Coding Conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [Minimal API Best Practices](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/min-api-filter)
- [Testing ASP.NET Core Apps](https://learn.microsoft.com/aspnet/core/test/integration-tests)
- [Awesome GitHub Copilot](https://github.com/github/awesome-copilot) — Agentes, instrucciones y skills de la comunidad

### Siguiente Nivel con Copilot

- **Copilot en la terminal:** Usa `Ctrl+I` en la terminal integrada de VS Code para generar comandos `dotnet`
- **Más agentes personalizados:** Crea agentes para testing, seguridad, documentación o DevOps en `.github/agents/`
- **Prompt files reutilizables:** Define tareas comunes como `/deploy`, `/review`, `/changelog` en `.github/prompts/`
- **Skills con scripts:** Combina instrucciones con scripts y assets en `.github/skills/` para tareas complejas
- **Copilot para Git:** Usa Copilot Chat para generar mensajes de commit descriptivos