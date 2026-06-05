## 🔬 Ejercicio 2: Frontend e Integración (25-30 min)

> ⚠️ **PRERREQUISITO:** Este ejercicio requiere que la API del Ejercicio 1 esté funcionando.

> 📝 **ENFOQUE DEL LABORATORIO:** El frontend se implementa como una aplicación **Blazor WebAssembly** (proyecto `BancoX.App`) que consume la API `BancoX.Api` vía `HttpClient`. Se usa Bootstrap 5 vía CDN para estilos, sin npm.

### Objetivos

- ✅ Crear una aplicación Blazor WebAssembly que consuma la API
- ✅ Crear un agente personalizado de Copilot para desarrollo web
- ✅ Usar el agente con una imagen de referencia para mejorar el frontend
- ✅ Integrar Bootstrap 5 vía CDN para una interfaz visual atractiva
- ✅ Practicar el uso de `/explain` para entender código generado

### Paso 2.1: Crear la aplicación Blazor Web Assembly del frontend


🤖 **PROMPT en Modo Agent:**

```
Crea una nuevo proyecto tipo Blazor Web Assembly en el workspace abierto, para el Banco X y dentro de ella haz una página principal para el Banco X.


Requisitos:
1. Usar Bootstrap 5 via CDN (no instalar localmente, no usar npm)
2. Header con el nombre "Banco X" y un ícono de banco (emoji o icono Bootstrap)
3. Barra de navegación con tabs: Inicio, Clientes, Cuentas, Transacciones
4. Sección de Inicio con:
   - Tarjetas (cards) mostrando estadísticas: Total Clientes, Total Cuentas
   - Las estadísticas se cargan dinámicamente desde la API.
5. Sección de Clientes con:
   - Tabla con los datos de clientes (se carga desde GET /api/clientes)
   - Botón "Nuevo Cliente" que muestra un modal con formulario
   - Botones de editar y eliminar en cada fila
6. Footer con "© Banco X - Sistema de Gestión Bancaria"

El proyecto creálo con la estructura recomendada para este tipo de proyectos.
Usa colores profesionales bancarios (azul oscuro #1a237e, blanco, gris claro).
Incluye manejo de errores con mensajes amigables al usuario.
```

---

### Paso 2.2: Crear un agente personalizado para desarrollo web 🤖

> 💡 **¿Qué es un agente personalizado?** Como vimos en los Conceptos Clave, un agente es una versión especializada de Copilot definida en un archivo Markdown con frontmatter YAML. Se crea mediante la instrucción /create-agent que define su rol, expertise y reglas. Una vez creado, puedes seleccionarlo en Copilot Chat para que todas sus respuestas sigan ese perfil especializado.

🤖 **PROMPT en Modo Agent:**

```
Es un especialista senior en frontend web con experiencia en interfaces para banca y finanzas. Domina HTML5 semántico, Bootstrap 5 vía CDN, JavaScript, Blazor Web Assembly, y diseño responsive mobile-first.

Todo lo que genere debe estar en español: código, comentarios y textos visibles en la interfaz. El look and feel debe ser profesional y bancario — azul oscuro, blanco y gris claro como paleta base. La usabilidad y la claridad siempre van por encima de la complejidad visual. Los montos deben mostrarse como moneda con separadores de miles, y la interfaz debe incluir estados de carga y mensajes de error que un usuario no técnico pueda entender.

Debe respetar la arquitectura del sistema que contiene Front-End en Blazor WebAssembly y un API la cual debe invocar para las operaciones.

Cuando reciba una imagen de referencia, debe analizar el layout, los colores, la tipografía y la distribución de elementos para extraer los mejores patrones y adaptarlos al stack del proyecto, sin copiar el diseño tal cual y manteniendo la identidad visual de Banco X.
```

📝 **Observa:** El agente queda registrado en el repositorio. Cualquier miembro del equipo que clone el repo tendrá acceso a este agente especializado desde Copilot Chat.
---

### Paso 2.3: Mejorar el frontend usando el agente con un screenshot 📸

> 💡 **CONCEPTO:** Ahora vamos a usar el agente que acabamos de crear para mejorar la página del Paso 2.1. El flujo es: tomas un screenshot de una interfaz que te guste como referencia, se lo pasas al agente, y le pides que adapte tu frontend con esa inspiración.

📍 **Instrucciones:**
1. Busca un screenshot de un dashboard bancario que te guste como referencia (puedes buscar "banking dashboard UI" en Google Imágenes y tomar un screenshot, o usar uno que el instructor proporcione)
2. En Copilot Chat, selecciona el agente de frontend recientemente creado
3. Arrastra o pega la imagen de referencia en el chat y escribe el siguiente prompt:

🤖 **PROMPT usando el nuevo agente:**

```
Aquí te comparto una imagen de referencia de un dashboard bancario.

Analiza el diseño y mejora la visualización gráfica de las pantallas creadas:

1. Mejora el layout de las tarjetas de estadísticas en la sección de Inicio
2. Mejora el diseño de la tabla de clientes (bordes, spacing, hover effects)
3. Agrega íconos visuales (puedes usar Bootstrap Icons vía CDN)
4. Mejora la navegación y la jerarquía visual general
5. Asegúrate de que el diseño sea responsive

Mantén toda la funcionalidad  existente.
No cambies las rutas de la API ni la lógica de negocio.
```

> 🌟 El agente interpreta la imagen de referencia y adapta el frontend manteniendo la funcionalidad existente. Esto demuestra cómo los agentes personalizados combinan expertise especializada con contexto visual para producir resultados más precisos que un prompt genérico.

> 📝 **Si no tienes una imagen de referencia**, puedes omitir la imagen y pedirle al agente: *"Mejora el diseño con tu criterio de experto en UX bancaria. Hazlo más profesional y moderno manteniendo toda la funcionalidad existente."*

---

### Paso 2.4: Agregar las funcionalidades de cuentas al sistema

🤖 **PROMPT en Modo Agent:**

```
Actualiza la aplicación web para agregar las funcionalidades pendientes de cuentas bancarias.

Haz lo siguiente para la pantalla de cuentas bancarias:
1. Realiza mejoras visuales aplicadas en Clientes. Asegúrate que la tabla muestre: número, tipo, saldo, estado, cliente asociado
2. Los saldos deben mostrarse en formato de moneda ($ con separadores de miles)
3. Botón "Nueva Cuenta" con modal que incluya:
   - Selector de cliente (dropdown cargado desde la API)
   - Tipo de cuenta (ahorro/corriente)
   - Saldo inicial
4. Badge de color para el estado: activa (verde), inactiva (amarillo), bloqueada (rojo)
5. Funcionalidad de eliminar cuenta con confirmación

Usa el mismo patrón que ya existe en la sección de Clientes.
```

> 💡 **Observa:** Al mencionar "el patrón que ya existe", Copilot genera código **consistente** con lo que ya escribiste, todo dentro del mismo archivo HTML.

---

### Paso 2.5: Ejecutar y probar la integración

🤖 **PROMPT en Modo Agent:**

```
Ejecuta la aplicación de Banco X
```

📝 **Alternativa manual:**
```bash
cd BancoX
dotnet run
```

**Abre en el navegador:**
- Frontend (Blazor Web Assembly): `http://localhost:52XX/`
- Swagger UI (documentación API): `http://localhost:50XX/swagger`

✅ **Verificar:**
- La página HTML de Banco X carga correctamente
- Las estadísticas se muestran con datos reales de la API
- La tabla de clientes muestra los datos de ejemplo
- Se puede crear un nuevo cliente desde el formulario modal
- La sección de cuentas funciona con los datos de la API
- Swagger UI sigue accesible en `/swagger`

---
### Paso 2.6: Usar /explain para entender código (demostración)

> 💡 **CONCEPTO:** El comando `/explain` es perfecto para entender código que Copilot generó.

📍 **Instrucciones:**
1. Selecciona algún bloque Razor que invoque al API (por ejemplo, la carga de cuentas desde la API).
2. Abre Copilot Chat y escribe:

```
/explain ¿Qué hace este código paso a paso? ¿Hay algún problema potencial?
```

📝 **Observa:** Copilot explica el flujo del código y puede señalar posibles mejoras como manejo de errores, timeouts o validaciones faltantes.

---

