# MVCPrueba1

Proyecto ASP.NET Core MVC organizado con una estructura cercana a Clean Architecture.

## Estructura de capas

```text
MVCPrueba1
|-- MVCPrueba1.csproj
|-- Controllers
|-- Views
|-- wwwroot
`-- src
    |-- Domain
    |-- Application
    `-- Infrastructure
```

## MVCPrueba1

Proyecto Web.

Responsabilidades:

- Contiene la UI MVC: controladores, vistas, Razor Pages y archivos estaticos.
- Contiene `Program.cs`, que actua como composition root.
- Configura los servicios de `Application` e `Infrastructure`.
- Arranca la aplicacion.

Este proyecto puede conocer `Application` e `Infrastructure`, porque es el punto donde se conectan todas las piezas.

## Domain

Capa de dominio.

Responsabilidades:

- Contiene las entidades del negocio.
- No debe depender de EF Core, ASP.NET Core, Identity ni de infraestructura.
- Representa el nucleo mas estable de la aplicacion.

Ejemplo actual:

```text
src/Domain/Entities/PersonEntity.cs
```

## Application

Capa de aplicacion.

Responsabilidades:

- Contiene los casos de uso.
- Contiene los contratos que necesita la logica, como `IPersonRepository`.
- Contiene modelos usados por los casos de uso y la UI.
- Contiene converters y servicios de aplicacion.

Esta capa depende de `Domain`, pero no debe depender de `Infrastructure`, EF Core, SQL Server, Identity ni `ApplicationDbContext`.

Ejemplos:

```text
src/Application/UseCases
src/Application/Repositories/IPersonRepository.cs
src/Application/Models
```

## Infrastructure

Capa de infraestructura.

Responsabilidades:

- Contiene detalles tecnicos.
- Contiene EF Core.
- Contiene `ApplicationDbContext`.
- Contiene migraciones.
- Contiene implementaciones concretas de repositorios.
- Contiene adaptadores relacionados con ASP.NET Core, como el acceso al usuario actual.

Esta capa implementa contratos definidos en `Application`.

Ejemplos:

```text
src/Infrastructure/Data/ApplicationDbContext.cs
src/Infrastructure/Data/Repositories/PersonRepository.cs
src/Infrastructure/Data/Migrations
src/Infrastructure/UserInfo/PersonUserDetails.cs
```

## Direccion de dependencias

La direccion correcta es:

```text
Web -> Application -> Domain
Web -> Infrastructure -> Application -> Domain
Infrastructure -> Domain
```

Regla importante:

```text
Domain no depende de nadie.
Application no depende de Infrastructure.
Infrastructure depende de Application para implementar sus contratos.
Web conecta todo.
```

## Migraciones con Entity Framework Core

Despues de separar la solucion en capas, las migraciones ya no pertenecen al proyecto Web `MVCPrueba1`.

Ahora viven en:

```text
src/Infrastructure
```

El proyecto que arranca la aplicacion sigue siendo:

```text
MVCPrueba1
```

Por eso los comandos de EF deben indicar siempre:

- `--project`: proyecto donde estan `ApplicationDbContext` y las migraciones.
- `--startup-project`: proyecto que contiene `Program.cs` y la configuracion de arranque.

## Crear una migracion

Desde la raiz de la solucion:

```powershell
dotnet ef migrations add NombreDeLaMigracion --project "src\Infrastructure\Infrastructure.csproj" --startup-project "csproj" --context ApplicationDbContext --output-dir "Data\Migrations"
```

Ejemplo:

```powershell
dotnet ef migrations add AddPersonBirthDate --project "src\Infrastructure\Infrastructure.csproj" --startup-project "csproj" --context ApplicationDbContext --output-dir "Data\Migrations"
```

## Eliminar la ultima migracion

Desde la raiz de la solucion:

```powershell
dotnet ef migrations remove --project "src\Infrastructure\Infrastructure.csproj" --startup-project "csproj" --context ApplicationDbContext
```

Este comando elimina la ultima migracion no aplicada o revierte los archivos de migracion generados.

## Actualizar la base de datos

Desde la raiz de la solucion:

```powershell
dotnet ef database update --project "src\Infrastructure\Infrastructure.csproj" --startup-project "csproj" --context ApplicationDbContext
```

## Comprobar si hay cambios pendientes en el modelo

```powershell
dotnet ef migrations has-pending-model-changes --project "src\Infrastructure\Infrastructure.csproj" --startup-project "csproj" --context ApplicationDbContext
```

Si todo esta sincronizado, EF mostrara:

```text
No changes have been made to the model since the last migration.
```

## Error habitual al eliminar migraciones

Si ejecutas:

```powershell
dotnet ef migrations remove
```

puede aparecer este error:

```text
Your target project 'MVCPrueba1' doesn't match your migrations assembly 'Infrastructure'.
```

Esto ocurre porque EF intenta usar el proyecto Web como proyecto de migraciones, pero las migraciones estan en
`Infrastructure`.

Solucion:

```powershell
dotnet ef migrations remove --project "src\Infrastructure\Infrastructure.csproj" --startup-project "csproj" --context ApplicationDbContext
```

## Package Manager Console de Visual Studio

En Visual Studio, si usas la Package Manager Console:

1. Selecciona como **Default project**:

```text
Infrastructure
```

2. Para crear una migracion:

```powershell
Add-Migration NombreDeLaMigracion -StartupProject MVCPrueba1 -Context ApplicationDbContext
```

3. Para eliminar la ultima migracion:

```powershell
Remove-Migration -StartupProject MVCPrueba1 -Context ApplicationDbContext
```

4. Para actualizar la base de datos:

```powershell
Update-Database -StartupProject MVCPrueba1 -Context ApplicationDbContext
```

## Compilar

Desde la raiz:

```powershell
dotnet build
```
