# Voultech Test - API de Gestión de Órdenes

Este proyecto es una API REST desarrollada en .NET 10 para la gestión de órdenes de compra y productos.

## Arquitectura
Se ha utilizado una arquitectura en capas para separar las responsabilidades:
- **VoultechTest.Api**: Controladores y configuración de la API
- **VoultechTest.Domain**: Entidades del negocio e interfaces
- **VoultechTest.Infrastructure**: Implementación de persistencia con Entity Framework Core y SQLite
- **VoultechTest.Services**: Lógica de negocio (Cálculo de descuentos)
- **VoultechTest.Tests**: Pruebas unitarias con xUnit

## Requisitos
- .NET 10 SDK

## Seguridad
Todos los endpoints de la API están protegidos con una clave, Se debe incluir la siguiente cabecera en cada petición:
- **Header**: `ApiKey`
- **Valor**: `c8e0d9a9-b90d-4317-b549-159d0b652ccd`

Swagger está configurado para permitir la autenticación mediante el botón "Authorize".

## Ejecución
1. Clonar el repositorio
2. Abrir una terminal en la raíz del proyecto
3. Ejecutar el siguiente comando para iniciar la API:
   ```bash
   dotnet run --project VoultechTest.Api
   ```
4. La API estará disponible en `https://localhost:5056` (en mi caso o el puerto configurado por defecto)

## Endpoints Principales
- **Productos**:
  - `GET /productos`: Listar todos los productos
  - `POST /productos`: Crear un nuevo producto
- **Ordenes**:
  - `GET /ordenes`: Listar órdenes (Soporta paginación: `?pagina=1&tamañoPagina=10`)
  - `GET /ordenes/{id}`: Obtener una orden por ID
  - `POST /ordenes`: Crear una orden (Calcula descuentos)
  - `PUT /ordenes/{id}`: Actualizar una orden existente
  - `DELETE /ordenes/{id}`: Eliminar una orden

## Reglas de Descuento (Bonus)
- **10% de descuento**: Si el total de la orden supera los $500
- **5% adicional**: Si la orden contiene más de 5 productos distintos

## Pruebas
Para ejecutar las pruebas unitarias:
```bash
dotnet test
```
Las 4 pruebas abarcan los casos sin descuento, descuento del 10%, descuento del 5% y por ultimo la combinación de las 2 reglas

## Screenshot
Deje pruebas con postman y de swagger en la carpeta screenshots
