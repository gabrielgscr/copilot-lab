## 🔬 Ejercicio 3: Tests y Refactoring (20-25 min)

> ⚠️ **PRERREQUISITO:** Este ejercicio requiere haber completado el Ejercicio 1 con la API funcionando.

### Objetivos

- ✅ Generar pruebas unitarias automáticamente con `/tests`
- ✅ Usar Copilot para escribir tests de integración de la API
- ✅ Practicar refactoring asistido con `/fix`
- ✅ Generar documentación con `/doc`

### Paso 3.1: Generar pruebas unitarias con /tests

> 💡 **COMANDO ESPECIAL:** El comando `/tests` genera automáticamente pruebas unitarias para el código seleccionado.

📍 **Cómo usar /tests:**
1. Abre el archivo `BancoX/Services/ClienteServicio.cs`
2. Selecciona todo el contenido del archivo (`Ctrl+A` / `Cmd+A`)
3. Abre Copilot Chat y escribe:

🤖 **PROMPT:**

```
Genera pruebas unitarias completas con xUnit para este servicio.

Quiero pruebas que cubran:

1. Obtener todos los clientes (verifica que retorne la lista completa)
2. Obtener un cliente por Id válido e inválido
3. Crear un cliente con datos válidos
4. Crear un cliente con datos incompletos (falta Email, falta Nombre)
5. Actualizar un cliente existente
6. Actualizar un cliente que no existe
7. Eliminar un cliente existente
8. Eliminar un cliente que no existe

```

> 🌟 Copilot genera un suite completo de tests incluyendo casos positivos y negativos, ¡a partir del código que tú escribiste!

---

### Paso 3.2: Crear archivo de pruebas

🤖 **PROMPT en Modo Agent:**

```
Guarda las pruebas generadas en BancoX.Tests/ClienteServicioTests.cs

Asegúrate de que:
1. Los using/imports sean correctos para referenciar el proyecto BancoX
2. Cada test crea su propia instancia de ClienteServicio para que sean independientes
3. El namespace sea BancoX.Tests
```

> 📝 **Nota:** La referencia al proyecto principal ya se agregó en el Paso 1.4 con `dotnet add reference`, así que los imports deberían funcionar directamente.

---

### Paso 3.3: Generar pruebas de integración para la API

🤖 **PROMPT en Modo Agent:**

```
Crea pruebas de integración para la API de Banco X en BancoX.Tests/ApiIntegrationTests.cs

Usando WebApplicationFactory<Program> de Microsoft.AspNetCore.Mvc.Testing:

1. Agrega el paquete Microsoft.AspNetCore.Mvc.Testing al proyecto de tests
2. Asegúrate de que Program.cs sea accesible (agrega <InternalsVisibleTo Include="BancoX.Tests" /> al .csproj del proyecto principal, o un partial class Program {} al final de Program.cs)
3. Crea una clase de tests que use WebApplicationFactory para levantar la API en memoria
4. Pruebas que verifiquen:
   - Que se puedan listar, consultar, crear, actualizar y eliminar clientes vía HTTP
   - Que los endpoints respondan con los códigos HTTP correctos (200, 201, 404, 400)
   - Que al consultar un recurso que no existe se obtenga 404
   - Que los endpoints de cuentas también respondan correctamente
5. Usa HttpClient con GetAsync, PostAsJsonAsync, PutAsJsonAsync, DeleteAsync
6. Deserializa las respuestas con response.Content.ReadFromJsonAsync<T>()

Cada test debe ser independiente y usar nombres descriptivos en español.
```

---

### Paso 3.4: Ejecutar las pruebas

🤖 **PROMPT en Modo Agent:**

```
Ejecuta todas las pruebas del proyecto Banco X y muéstrame los resultados
```

📝 **Alternativa manual:**
```bash
dotnet test --verbosity normal
```

✅ **Verificar:**
- Todas las pruebas pasan (verde)
- Los tests unitarios y de integración se ejecutan correctamente
- No hay errores de compilación ni de referencia

---

### Paso 3.5: Refactoring con /fix y /explain

> 💡 **CONCEPTO:** Ahora usaremos Copilot para **mejorar** el código existente.

📍 **Ejercicio de refactoring:**

1. Abre `BancoX/Program.cs`
2. Selecciona un grupo de endpoints
3. Usa el siguiente prompt en Copilot Chat:

🤖 **PROMPT:**

```
/explain Analiza este código y dime:
1. ¿Hay código duplicado que se pueda simplificar?
2. ¿Faltan validaciones importantes?
3. ¿Hay algún problema de seguridad potencial?
4. ¿Cómo mejorarías el manejo de errores?
```

Después, si Copilot sugiere mejoras:

```
/fix Aplica las mejoras de seguridad y validación que acabas de sugerir
```

> 🌟 Copilot puede detectar **problemas de seguridad** como falta de validación de inputs, inyección potencial o manejo inadecuado de errores, y proponer correcciones específicas.

---

### Paso 3.6: Generar documentación con /doc *(si hay tiempo)*

📍 **Instrucciones:**
1. Abre `BancoX/Services/CuentaServicio.cs`
2. Selecciona un método que no tenga comentarios XML
3. Usa el comando `/doc`:

🤖 **PROMPT:**

```
/doc Genera comentarios XML (///) completos en español para estos métodos.

Incluye:
- <summary> con descripción clara de qué hace cada método
- <param> para cada parámetro con tipo y descripción
- <returns> con el valor de retorno
- <exception> para excepciones que puede lanzar
- <example> con un ejemplo de uso
```

---
