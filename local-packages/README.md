# Paquetes locales

Este directorio contiene los paquetes NuGet propios que todavía no están
publicados en un registro remoto.

Los paquetes se versionan junto al repositorio para que `dotnet restore`, los
tests y la compilación Docker funcionen después de clonar el proyecto en otro
equipo.

Cuando se actualice uno de estos paquetes:

1. Copiar aquí el nuevo archivo `.nupkg`.
2. Actualizar su versión en `Directory.Build.props`.
3. Eliminar la versión anterior cuando ningún proyecto la utilice.
4. Ejecutar `dotnet restore` y las pruebas.
