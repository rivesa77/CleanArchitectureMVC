# CleanArchitectureMVC

Proyecto ASP.NET Core MVC organizado con una estructura cercana a Clean Architecture.

## Estructura de capas

```text
CleanArchitectureMVC
|-- CleanArchitectureMVC.csproj
|-- Migration.md
|-- Controllers
|-- Views
|-- wwwroot
`-- src
    |-- Domain
    |-- Application
    |   `-- Tests
    |-- Infrastructure
    |   `-- Tests
    `-- E2E
        `-- Tests
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
src/Application/Converters
```

### Decision temporal sobre DTOs y converters

En una aplicacion orientada a produccion, la informacion que cruza los limites entre la UI, los casos de uso y
el dominio deberia representarse mediante DTOs especificos de entrada y salida.

En este proyecto se ha optado, de forma intencionada y a modo de prueba, por utilizar converters para transformar
directamente objetos como `PersonViewModel`, `PersonEntity`, `PersonSearchCriteria` y `PersonSearchQuery`. El objetivo
es experimentar con la conversion de clases, la conversion independiente de propiedades y su registro mediante
inyeccion de dependencias.

Un converter resuelve el mapeo entre objetos, pero no sustituye el papel de un DTO como contrato explicito. Si el
proyecto evoluciona, se deberian introducir DTOs especificos y mantener los converters unicamente como mecanismo de
mapeo entre esos DTOs y los modelos internos.

### Que son los converters y como se utilizan

Los converters son componentes de la capa `Application` encargados de crear un objeto de destino a partir de un
objeto de origen. Centralizan el mapeo para evitar que los controladores y los casos de uso copien o transformen
propiedades manualmente.

Cada conversion se divide en dos niveles:

- Un converter de clase coordina la conversion completa, por ejemplo
  `PersonsViewModelToPersonEntityConverter`.
- Varios converters de propiedad se ocupan de campos concretos como DNI, nombre, email, telefono, identificador y
  usuario. Tambien pueden aplicar transformaciones; por ejemplo, `DniConverter` convierte el DNI a mayusculas.

Los converters y sus converters de propiedad se registran con ciclo de vida `Scoped` en el contenedor de inyeccion de
dependencias. Los casos de uso reciben la interfaz correspondiente mediante el constructor y llaman a `Convert`:

```csharp
private readonly IPersonsViewModelToPersonEntityConverter converter;

PersonEntity personEntity = this.converter.Convert(personViewModel);
```

El flujo utilizado por `AddPersonUseCase` es:

1. La UI entrega un `PersonViewModel` al caso de uso.
2. El caso de uso valida los datos.
3. `IPersonsViewModelToPersonEntityConverter` crea un `PersonEntity`.
4. Los converters de propiedad rellenan y transforman sus campos.
5. El caso de uso entrega la entidad resultante a `IPersonRepository`.

El mismo enfoque se utiliza para convertir `PersonEntity` en `PersonViewModel` al mostrar datos y
`PersonSearchCriteria` en `PersonSearchQuery` al realizar busquedas.

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

## Datos iniciales y usuario de prueba

La autenticacion utiliza la plantilla y la interfaz predeterminadas de ASP.NET Core Identity para registrar,
autenticar y validar al usuario. Identity proporciona las paginas de acceso y registro, la validacion de credenciales
y la autenticacion mediante cookies, mientras que los usuarios se almacenan en `ApplicationDbContext`. Actualmente
no se exige la confirmacion de la cuenta para iniciar sesion.

Al arrancar la aplicacion se aplican las migraciones pendientes y se ejecuta el proceso de seed. La primera vez que
se inicia con una base de datos vacia, se crea un usuario de demostracion y se cargan personas de prueba asociadas a
ese usuario.

El seed se comprueba en cada arranque, pero no vuelve a insertar los datos si la tabla `Persons` ya contiene registros,
por lo que no se generan duplicados.

Para iniciar sesion y consultar los datos de prueba:

```text
Usuario: demo@ricardo.local
Password: Demo1234!
```

## Tests

Los tests utilizan MSTest como framework de pruebas, Fluent Assertions para expresar las comprobaciones y Moq para
crear, configurar y verificar mocks. Actualmente existen tres proyectos de pruebas:

- `src/Application/Tests`: comprueba converters, el registro de converters en inyeccion de dependencias y distintos
  escenarios de `AddPersonUseCase`, como datos no validos, DNI duplicado, fallo de conversion y resultado correcto.
- `src/Infrastructure/Tests`: prueba `PersonRepository` con EF Core InMemory, incluyendo persistencia, busqueda,
  filtrado y ordenacion.
- `src/E2E/Tests`: contiene un flujo E2E con `WebApplicationFactory`, registro de usuario, creacion de personas,
  busqueda y ordenacion. Consulta su [documentacion especifica](src/E2E/Tests/README.md).

En cuanto a los casos de uso, por ahora solo existe una bateria de tests de `AddPersonUseCase` creada principalmente
para mostrar como crear, configurar, utilizar y verificar mocks con Moq para el repositorio y el converter.

Estas pruebas tienen un objetivo demostrativo y no cubren todos los casos de uso, controladores, reglas de negocio ni
escenarios de error. El proyecto no tiene una cobertura del 100 % y no debe interpretarse la suite actual como una
garantia de cobertura completa.

Para ejecutar todos los tests:

```powershell
dotnet test CleanArchitectureMVC.slnx
```

La prueba E2E requiere SQL Server LocalDB. Los tests de `Application` e `Infrastructure` pueden ejecutarse por separado:

```powershell
dotnet test "src\Application\Tests\Application.Tests.csproj"
dotnet test "src\Infrastructure\Tests\Infrastructure.Tests.csproj"
```

## Migraciones con Entity Framework Core

Consulta la guia de [migraciones con Entity Framework Core](Migration.md).

## Compilar

Desde la raiz:

```powershell
dotnet build CleanArchitectureMVC.slnx
```
