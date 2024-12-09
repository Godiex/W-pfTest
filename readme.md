# Proyecto de Gestión de Usuarios y Áreas

Este proyecto es una aplicación de escritorio construida en **WPF** para gestionar usuarios y áreas en una organización. La solución implementa una arquitectura basada en capas, siguiendo principios de código limpio y utilizando tecnologías modernas para facilitar su mantenimiento y escalabilidad.

---

## **Estructura del Proyecto**

La solución está dividida en las siguientes capas:

### 1. **WpfApp** (Capa de Presentación)
- **Descripción**: Contiene la lógica de interacción con el usuario y las vistas de la aplicación.
- **Componentes clave**:
    - **Views**: Contiene las vistas en XAML para las interfaces gráficas de usuario.
    - **ViewModels**: Implementa el patrón MVVM para vincular la lógica de negocio con las vistas.
    - **Commands**: Maneja los comandos de la interfaz para acciones específicas del usuario.
    - **DependencyInjectionConfig.cs**: Configura la inyección de dependencias utilizando Unity.

### 2. **Application** (Capa de Aplicación)
- **Descripción**: Orquesta las operaciones entre las capas de presentación e infraestructura.
- **Responsabilidades**:
    - Procesar comandos enviados desde la capa de presentación.
    - Coordinar las interacciones con los repositorios y servicios de dominio.

### 3. **Domain** (Capa de Dominio)
- **Descripción**: Contiene la lógica de negocio principal y entidades del sistema.
- **Componentes clave**:
    - **Entities**: Define las clases del dominio como `User`, `Area` y `UserArea`.
    - **Services**: Contiene lógica de negocio avanzada, como la asignación de usuarios a áreas.
    - **Ports**: Define las interfaces que conectan la capa de dominio con la infraestructura.

### 4. **Infrastructure** (Capa de Infraestructura)
- **Descripción**: Implementa las operaciones de acceso a datos y conecta el sistema con recursos externos.
- **Componentes clave**:
    - **Data**: Contiene la clase `AppDbContext` para interactuar con la base de datos utilizando Entity Framework.
    - **Adapters**: Implementa los repositorios definidos en la capa de dominio.
    - **Extensions**: Configuración adicional como inyección de dependencias y validadores.

---

## **Tecnologías Utilizadas**

- **Lenguaje**: C# (.NET Framework 4.8)
- **Interfaz gráfica**: WPF (Windows Presentation Foundation)
- **Base de datos**: SQL Server
- **ORM**: Entity Framework 6
- **Micro ORM**: Dapper (para consultas específicas)
- **Inyección de dependencias**: Unity
- **Validación**: FluentValidation
- **Patrón de diseño**: MVVM (Model-View-ViewModel)

---

## **Funcionalidades**

### **Gestión de Usuarios**
- Crear, actualizar y listar usuarios.
- Validar que no existan duplicados basados en identificación, correo o número de teléfono.

### **Gestión de Áreas**
- Listar áreas disponibles.
- Asignar usuarios a áreas.
- Actualizar la asignación de un usuario si ya tiene un área asociada.

### **Consultas**
- Recuperar los últimos 10 usuarios registrados, mostrando el área asignada (o "No Asignado" si no tiene área).

---

## **Arquitectura**

El proyecto sigue una arquitectura basada en capas con las siguientes características clave:

1. **Separación de responsabilidades**:
    - Cada capa tiene un propósito claro, desde la presentación hasta el acceso a datos.
2. **Inversión de dependencias**:
    - Se utiliza Unity para desacoplar la lógica del negocio de las implementaciones concretas.
3. **Reutilización de componentes**:
    - Los servicios y repositorios están diseñados para ser reutilizables en diferentes contextos.

---

## **Estructura de Carpetas**

```plaintext
├── WpfApp
│   ├── Views
│   ├── ViewModels
│   ├── Commands
│   └── App.xaml.cs
├── Application
│   └── UseCases
├── Domain
│   ├── Entities
│   ├── Services
│   └── Ports
├── Infrastructure
│   ├── Adapters
│   ├── Data
│   ├── Extensions
│   └── Configuration
