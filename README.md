# CleanArchitectureMVC

Proyecto ASP.NET Core MVC organizado con una estructura cercana a Clean Architecture.

## Estructura de capas

```text
CleanArchitectureMVC
|-- CleanArchitectureMVC.csproj
|-- Controllers
|-- Views
|-- wwwroot
`-- src
    |-- Domain
    |-- Application
    `-- Infrastructure
```

## CleanArchitectureMVC

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

Consulta la guia de [migraciones con Entity Framework Core](Migration.md).

## Compilar

Desde la raiz:

```powershell
dotnet build
```
