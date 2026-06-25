# E2E Tests

Este proyecto contiene pruebas end-to-end de la aplicacion MVC.

## Como funciona

El test usa `WebApplicationFactory<Program>` para arrancar la aplicacion dentro del propio proceso de test. Por eso no hace falta tener la web levantada previamente en `https://localhost:7097`.

La prueba crea una base de datos LocalDB temporal con un nombre unico:

```text
MVCPrueba1_E2E_{uniqueId}
```

Al arrancar la aplicacion, se ejecuta el flujo normal de `Program.cs`: migraciones, configuracion de Identity, seed y rutas MVC/Razor Pages.

## Flujo del test

El test principal es:

```text
Persons_WhenCreated_CanBeSearchedAndSortedByNameDescending
```

Pasos que realiza:

1. Genera un identificador unico para la ejecucion.
2. Arranca la aplicacion con `WebApplicationFactory<Program>`.
3. Crea un `HttpClient` con cookies habilitadas.
4. Solicita `/Identity/Account/Register`.
5. Extrae el token antiforgery del formulario de registro.
6. Registra un usuario nuevo de prueba.
7. Crea dos personas mediante `POST /Persons/create`.
8. Consulta `/Persons` filtrando por nombre.
9. Ordena por `Name` en direccion descendente.
10. Comprueba que ambas personas aparecen.
11. Comprueba que la persona esperada aparece antes segun la ordenacion.

## Limpieza

El test elimina siempre la base de datos temporal en un bloque `finally`:

```text
EnsureDeletedAsync()
```

Asi, tanto si el test pasa como si falla, los datos creados por la prueba no quedan en la base de datos principal.

## Como ejecutar

Desde la raiz de la solucion:

```powershell
dotnet test src\E2E\Tests\E2E.Tests.csproj
```

Tambien se puede ejecutar toda la solucion:

```powershell
dotnet test MVCPrueba1.slnx
```

## Requisitos

- .NET SDK compatible con el proyecto.
- LocalDB disponible, porque el test usa SQL Server LocalDB.
- No es necesario arrancar la aplicacion manualmente.

