# Migraciones con Entity Framework Core

comandos de EF deben indicar siempre:

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
Your target project 'CleanArchitectureMVC' doesn't match your migrations assembly 'Infrastructure'.
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
Add-Migration NombreDeLaMigracion -StartupProject CleanArchitectureMVC -Context ApplicationDbContext
```

3. Para eliminar la ultima migracion:

```powershell
Remove-Migration -StartupProject CleanArchitectureMVC -Context ApplicationDbContext
```

4. Para actualizar la base de datos:

```powershell
Update-Database -StartupProject CleanArchitectureMVC -Context ApplicationDbContext
```
