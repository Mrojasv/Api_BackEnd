
# Api Rest Net Core

<table>
  <tr>
    <td><p align="center">Api Rest en .Net Core, un ejemplo practico con Entity Framework InMemory, para crear una factura, calcular el total a pagar, recibir los pagos y consultar todas las facturas</p>
    </td>
    <td>
    <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/490px-.NET_Core_Logo.svg.png" width="320" alt="Net Core" /></a></td>
  </tr>
</table>

## Herramientas
- [Github](https://github.com/)
- [Visual Studio Community edition](https://www.visualstudio.com/vs/community/)
- [Visual Studio Community for MAC](https://visualstudio.microsoft.com/es/vs/mac/)
- [Postman](https://www.getpostman.com/)

## Preparación del ambiente

Para usar el código solo hace falta clonar este repositorio:

    https://github.com/Mrojasv/Api_BackEnd.git

y luego hacer doble click en `Api_BackEnd.sln` (con Visual Studio instalado).

Para lanzar el proyecto podes hacerlo de la siguiente manera:

1. Click derecho en el proyecto > Establecer proyecto por defecto y luego darle al play (botón verde)

## Uso del Api

Crear un Invoice

-Operación: POST
-URL: api/invoice

Body
```Json
{
    "rojo":"#f00",
    "verde":"#0f0",
    "azul":"#00f",
    "cyan":"#0ff",
    "magenta":"#f0f",
    "amarillo":"#ff0",
    "negro":"#000"
}
```
