# Docker

Esta guía describe cómo crear, ejecutar, actualizar y modificar los contenedores
de `CleanArchitectureMVC`, además de cómo conectarse a su base de datos mediante
SQL Server Management Studio (SSMS).

## Arquitectura

Docker Compose levanta dos servicios:

| Servicio | Tecnología | Acceso desde Windows |
| --- | --- | --- |
| `web` | ASP.NET Core 10 | `http://localhost:8080` |
| `sqlserver` | SQL Server 2022 Developer | `127.0.0.1,1433` |

La base de datos se guarda en el volumen `sqlserver-data`. Detener o recrear los
contenedores no elimina dicho volumen.

Los archivos principales son:

- [Dockerfile](./Dockerfile): restaura, compila y publica la aplicación.
- [compose.yaml](./compose.yaml): configura la aplicación, SQL Server, puertos,
  variables, comprobación de salud y volumen.
- [NuGet.config](./NuGet.config): combina nuget.org con el feed
  `local-packages` incluido en el repositorio.
- [.dockerignore](./.dockerignore): excluye archivos innecesarios del contexto de
  compilación.

## Requisitos

- Docker Desktop iniciado y configurado para contenedores Linux.
- Los paquetes NuGet propios presentes en [local-packages](./local-packages).
- Los puertos `8080` y `1433` libres.
- Una contraseña fuerte para el usuario `sa`. SQL Server exige al menos ocho
  caracteres y una combinación de mayúsculas, minúsculas, números y símbolos.

El feed local forma parte del repositorio. No se necesita configurar una ruta
NuGet externa después de clonar el proyecto.

## Primera instalación

Abrir PowerShell en la carpeta del proyecto:

```powershell
cd "C:\C#\CODIGO\RICARDO\CleanArchitectureMVC"
```

Definir la contraseña. Esta variable debe configurarse nuevamente al abrir una
terminal nueva:

```powershell
$env:MSSQL_SA_PASSWORD = "Sustituir-Por-Una-Clave-Fuerte_2026!"
```

Construir las imágenes e iniciar los servicios:

```powershell
docker compose up -d --build
```

Comprobar su estado:

```powershell
docker compose ps
```

El resultado esperado debe mostrar:

- `sqlserver` en estado `healthy`.
- El puerto SQL `127.0.0.1:1433->1433/tcp`.
- El puerto web `0.0.0.0:8080->8080/tcp`.

Abrir la aplicación en:

```text
http://localhost:8080
```

## Base de datos y migraciones

Al arrancar, la aplicación ejecuta las migraciones de Entity Framework Core y
crea la base `CleanArchitectureMVC` cuando todavía no existe.

Docker configura `ASPNETCORE_ENVIRONMENT=Production`. En este entorno:

- Se aplican las migraciones.
- No se ejecuta `ApplicationDbContextSeed`.
- No se crean el usuario demo ni los registros de personas de prueba.

Cambiar el entorno a `Development` vuelve a habilitar el seeding. No debe
hacerse en una instalación que necesite una base de datos vacía.

Los registros creados anteriormente no desaparecen al cambiar a `Production`,
porque permanecen en el volumen.

## Comandos habituales

Iniciar los servicios:

```powershell
$env:MSSQL_SA_PASSWORD = "contraseña-original"
docker compose up -d
```

Detenerlos sin borrar la base de datos:

```powershell
docker compose down
```

Reiniciar:

```powershell
docker compose restart
```

Consultar el estado:

```powershell
docker compose ps
```

Ver los logs de la aplicación:

```powershell
docker compose logs -f web
```

Ver los logs de SQL Server:

```powershell
docker compose logs -f sqlserver
```

## Actualizar la aplicación

Después de modificar código, proyectos o paquetes:

```powershell
$env:MSSQL_SA_PASSWORD = "contraseña-original"
docker compose up -d --build web
```

Para reconstruir completamente la imagen sin utilizar la caché:

```powershell
docker compose build --no-cache web
docker compose up -d web
```

Si se actualiza `CommonLibraries.Converters`, hay que:

1. Copiar la nueva versión `.nupkg` a [local-packages](./local-packages).
2. Actualizar `CommonLibrariesConvertersVersion` en
   [Directory.Build.props](./Directory.Build.props).
3. Confirmar el nuevo paquete en Git.
4. Reconstruir la imagen sin caché.

Para actualizar las imágenes base:

```powershell
docker compose pull sqlserver
docker compose build --pull web
docker compose up -d
```

El volumen conserva la base de datos durante estas actualizaciones.

## Modificar la configuración

### Cambiar el puerto web

El valor situado a la izquierda es el puerto de Windows:

```yaml
ports:
  - "8081:8080"
```

La aplicación quedaría disponible en `http://localhost:8081`.

### Cambiar el puerto de SQL Server

Para usar, por ejemplo, el puerto `14333` en Windows:

```yaml
ports:
  - "127.0.0.1:14333:1433"
```

SSMS deberá conectarse entonces a `tcp:127.0.0.1,14333`.

La dirección `127.0.0.1` impide que SQL Server quede expuesto directamente a
otros equipos de la red.

### Aplicar cambios de Compose

Después de modificar variables, puertos o volúmenes:

```powershell
$env:MSSQL_SA_PASSWORD = "contraseña-original"
docker compose up -d --force-recreate
```

## Conectar SQL Server Management Studio

Primero comprobar que el puerto responde:

```powershell
Test-NetConnection 127.0.0.1 -Port 1433
```

`TcpTestSucceeded` debe ser `True`.

Usar estos valores en SSMS:

| Campo | Valor |
| --- | --- |
| Tipo de servidor | Motor de base de datos |
| Nombre del servidor | `tcp:127.0.0.1,1433` |
| Autenticación | Autenticación de SQL Server |
| Inicio de sesión | `sa` |
| Contraseña | El valor original de `MSSQL_SA_PASSWORD` |
| Cifrado | Obligatorio |
| Confiar en el certificado del servidor | Activado |

Una cadena de conexión equivalente es:

```text
Data Source=127.0.0.1,1433;Initial Catalog=master;User ID=sa;Password=CONTRASEÑA;Encrypt=True;TrustServerCertificate=True;
```

SQL Server utiliza una coma para separar servidor y puerto:
`127.0.0.1,1433`. La notación `localhost:1433` no es válida para este caso.

Después de conectar, la base aparece como:

```text
Databases
└── CleanArchitectureMVC
```

## Contraseña de `sa`

`MSSQL_SA_PASSWORD` establece la contraseña únicamente cuando SQL Server
inicializa un volumen nuevo. Cambiar la variable posteriormente no modifica la
contraseña guardada en la base.

Al recrear un contenedor que conserva `sqlserver-data`, debe proporcionarse la
contraseña original:

```powershell
$env:MSSQL_SA_PASSWORD = "contraseña-original"
docker compose up -d --force-recreate sqlserver
```

No se debe guardar la contraseña directamente en `compose.yaml` ni incorporarla
al repositorio.

## Resolución de problemas

### Error 1225: el equipo remoto rechazó la conexión

Comprobar:

```powershell
docker compose ps
Test-NetConnection 127.0.0.1 -Port 1433
```

`docker compose ps` debe mostrar `127.0.0.1:1433->1433/tcp`. Si solo muestra
`1433/tcp`, el puerto no está publicado o el contenedor no se recreó después de
modificar `compose.yaml`.

Aplicar de nuevo la configuración:

```powershell
$env:MSSQL_SA_PASSWORD = "contraseña-original"
docker compose up -d --force-recreate sqlserver
```

### Error 18456: error de inicio de sesión para `sa`

La contraseña introducida no coincide con la almacenada en el volumen. Usar la
contraseña empleada durante la primera instalación y revisar:

```powershell
docker compose logs --tail 100 sqlserver
```

### SQL Server aparece como `unhealthy`

Revisar los logs:

```powershell
docker compose logs --tail 200 sqlserver
```

Las causas habituales son una contraseña que no cumple la política, falta de
memoria o un problema con el volumen.

### Error NU1101 para `CommonLibraries.Converters`

Verificar que exista:

```text
local-packages/CommonLibraries.Converters.1.0.3.nupkg
```

También debe existir `NuGet.config` en la raíz. Después, restaurar y reconstruir:

```powershell
dotnet restore
docker compose build --no-cache web
```

### La base de datos no aparece

Comprobar que la aplicación arrancó y ejecutó las migraciones:

```powershell
docker compose logs --tail 200 web
```

## Eliminar y reinstalar

Detener y eliminar contenedores conservando la base:

```powershell
docker compose down
```

Eliminar también la imagen local de la aplicación, pero conservar la base:

```powershell
docker compose down --rmi local
```

Para borrar completamente la base de datos y empezar desde cero:

```powershell
docker compose down -v
```

> **Advertencia:** `docker compose down -v` elimina permanentemente el volumen
> `sqlserver-data` y toda la información almacenada en la base.

Después de eliminar el volumen:

```powershell
$env:MSSQL_SA_PASSWORD = "una-nueva-contraseña-fuerte"
docker compose up -d --build
```

La base se volverá a crear mediante migraciones y, al estar el contenedor en
`Production`, permanecerá sin registros de prueba.

## Consideraciones para producción

- SQL Server Developer debe utilizarse únicamente para desarrollo y pruebas.
- No se debe publicar el puerto `1433` fuera del equipo que administra la base.
- La contraseña debe proporcionarse mediante un almacén de secretos del entorno
  de despliegue.
- El tráfico web debe publicarse mediante HTTPS y un proxy inverso.
- Antes de actualizar SQL Server o eliminar volúmenes se debe disponer de una
  copia de seguridad verificada.
