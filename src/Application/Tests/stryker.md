# Pruebas de mutacion con Stryker

Stryker modifica temporalmente el codigo de Application y ejecuta sus tests
unitarios para comprobar si detectan esos cambios. No modifica los archivos
originales.

Instala la herramienta si todavia no esta disponible:

```powershell
dotnet tool install --global dotnet-stryker
```

Ejecuta Stryker desde el directorio de `Application.Tests` para que encuentre
`stryker-config.json` y `Application.Tests.csproj`:

```powershell
cd src\Application\Tests
dotnet stryker
```

Para obtener informacion detallada si la ejecucion falla:

```powershell
dotnet stryker --diag --verbosity trace
```

La configuracion utilizada es:

```json
{
  "stryker-config": {
    "solution": "../../../CleanArchitectureMVC.slnx",
    "project": "Application.csproj"
  }
}
```

El informe HTML se genera en:

```text
StrykerOutput\<fecha-y-hora>\reports\mutation-report.html
```

Estados principales del informe:

- `Killed`: algun test detecto el cambio. Es el resultado esperado.
- `Survived`: los tests no detectaron el cambio y puede faltar una comprobacion.
- `NoCoverage`: ningun test recorrio ese codigo.
- `CompileError`: el cambio creado por Stryker no podia compilarse.
- `Ignored`: Stryker descarto el cambio porque no era necesario ejecutarlo.

El mutation score indica el porcentaje de mutaciones detectadas. Stryker
complementa a `dotnet test`, pero no lo sustituye.
