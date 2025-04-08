# Arquitectura y Diseño de FS.FakeTwitter

## 🧱 Estilo Arquitectónico

Se aplicó el patrón **Onion Architecture**, dividido en 4 capas principales:

- **Domain**: Entidades y contratos (interfaces de repositorio)
- **Application**: Casos de uso, comandos y queries (CQRS con MediatR)
- **Infrastructure**: Acceso a datos, implementación de repositorios y servicios
- **Api**: Entrada HTTP (Controllers, Swagger, Middlewares)

> Esta separación permite desacoplar la lógica del negocio de los detalles de infraestructura.

---

## 🛠️ Componentes y Tecnologías Clave

- **.NET 8 + C#**
- **Entity Framework Core** (InMemory en desarrollo, PostgreSQL sugerido para producción)
- **MediatR**: para implementar CQRS
- **Swagger**: para la exploración de API
- **Custom Middlewares**: manejo centralizado de errores
- **Unit of Work + Repositorios**

---

## 🗄️ Base de Datos recomendada (Producción)

Se recomienda utilizar **PostgreSQL**, por los siguientes motivos:

- Soporte robusto para queries complejas y relaciones
- Open Source y ampliamente adoptado
- Optimizado para lecturas con `GIN indexes`, `materialized views` y `partitioning`

> En desarrollo se utilizó EF Core InMemory para facilitar el testing.

---

## 📈 Escalabilidad y Performance

Este diseño permite escalar horizontalmente tanto la API como la capa de base de datos:

- ✅ Queries desacopladas mediante MediatR (CQRS)
- ✅ El modelo de datos está optimizado para lecturas (p.ej., timeline por usuario)
- ✅ La infraestructura puede escalar con:
  - Load balancers (Ej: NGINX)
  - Cache distribuido (Ej: Redis)
  - Mensajería asincrónica (Ej: RabbitMQ)
  - Sharding o particionado por ID de usuario
- ✅ El código es testable y mantenible

---

## 🧪 Tests

- Unit tests para cada handler de comando/query
- Integration tests para controllers
- Cobertura del 100%



## Base de datos

Durante el desarrollo y testing se utiliza `Microsoft.EntityFrameworkCore.InMemory` para mantener el proyecto ligero y sin dependencias externas. Esta implementaci�n permite levantar y testear el sistema f�cilmente, persistiendo datos en memoria.

### Alternativa para producci�n

Para producci�n se sugiere el uso de **PostgreSQL**, por su soporte a relaciones complejas, facilidad de escalar horizontalmente y robustez frente a cargas altas.
