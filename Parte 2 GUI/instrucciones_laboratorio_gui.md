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

