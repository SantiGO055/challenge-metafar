
# Challenge Metafar

Api challenge para la empresa Metafar

## Requerimiento

#### Se requiere desarrollar una API que permita las siguientes operaciones.

Endpoints:
- Endpoint Login: la api debe de contar con un endpoint donde dado un número de tarjeta y un pin, el sistema debe validar que ambos son correctos y se condicen entre ellos. El endpoint debe retornar un token JWT el cual será utilizado para acceder a los otros endpoints. El usuario puede ingresar un PIN invalido hasta 4 veces, luego de esto la tarjeta debe quedar bloqueada y ya no permitir el acceso.
- Endpoint Saldo: la api debe de contar con un endpoint el cual dado un nro de tarjeta retorne la siguiente informacion: nombre del usuario, numero de cuenta, saldo actual y fecha de la última extracción.
- Endpoint Retiro: la api debe contar con un endpoint el cual dado un número de tarjeta y un montón, le permita realizar una extracción. En caso de que el monto a retirar sea superior al saldo disponible de la tarjeta, el endpoint debe de retornar un código de error. En caso de que todo sea correcto se debe retornar un resumen de la operación realizada.
- Endpoint Operaciones: la api debe de contar con un endpoint el cual dado un número de tarjeta debe retornar el historial de todas las operaciones realizadas. Dicha respuesta debe de estar paginada, es decir, no debe devolver el historial todo junto, sino que lo debe de hacer en páginas de 10 registros.
## Tecnologías utilizadas

- Base de datos: Relacional (SQL Server 2022)
- Minimal API Rest con .NET 8
- Swagger con OpenApi
- Utilizacion de Entity Framework Core 8 (Code First) con patrón Repository
- Cross platform
- Uso de CQRS con MediatR

## Tarea

1. Crear la base de datos y todos los objetos que creas necesarios para que la aplicación funciones.
2. Insertar una cantidad mínima de datos para poder probar la aplicación.
3. Desarrollar una aplicación web, en un repositorio público, utilizando una herramienta de versionado como github/bitbucket/etc, que responda a los requisitos descriptos por el cliente.
4. Entregar un diagrama de entidad relación de las tablas planteadas (DER)
## Preparar entorno

##### Una vez clonado el repositorio, para la creacion de las tablas en la base se deberan ejecutar los siguientes comandos en una Consola de Administración de Paquetes:

```bash
  Add-Migration init
  Update-Database
```


##### Una vez creadas las tablas, para insertar los datos para probar la aplicacion, ejecutar el script que se encuentra en la raiz del proyecto


```bash
    INSERTAR DATOS.sql
```

## DER

![DER](https://github.com/SantiGO055/challenge-metafar/tree/master/Utils/DER.png)