# Arquitectura y Diseño de FS.FakeTwitter

## 🧱 Estilo Arquitectónico

Se aplicó el patrón **Onion Architecture**, dividido en 4 capas principales:

- **Domain**: Entidades y contratos (interfaces de repositorio)
- **Application**: Casos de uso, comandos y queries (CQRS con MediatR)
- **Infrastructure**: Acceso a datos, implementación de repositorios y servicios
- **Api**: Entrada HTTP (Controllers, Swagger, Middlewares)

FS.FakeTwitter.sln
│
├── src/
│   ├── FS.FakeTwitter.Api             # Capa de presentación (controllers, Swagger, middlewares)
│   ├── FS.FakeTwitter.Application     # CQRS, servicios, DTOs, lógica de negocio
│   ├── FS.FakeTwitter.Domain          # Entidades y contratos del dominio
│   └── FS.FakeTwitter.Infrastructure  # Repositorios, servicios, DbContext, UnitOfWork
│
├── tests/
│   ├── FS.FakeTwitter.UnitTests         # Unit tests
│   └── FS.FakeTwitter.IntegrationTests  # Integration tests + coverage


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


# 🏛️ Arquitectura High-Level – FS.FakeTwitter

> Esta documentación describe la arquitectura y componentes utilizados en la solución del challenge técnico de Ualá.

---

## 🧱 Arquitectura Utilizada: Onion Architecture

La solución sigue los principios de la arquitectura en capas (Onion), asegurando una separación de responsabilidades clara:

Presentation (Api) │ ├── Application (CQRS, servicios, DTOs, lógica de casos de uso) │ └── MediatR (Commands / Queries / Handlers) │ ├── Domain (Entidades + Interfaces del dominio) │ └── Infrastructure (Repositorios, acceso a datos, EF Core, UnitOfWork)


---

## 🔧 Componentes Clave

| Componente                      | Propósito                                                         |
| ------------------------------ | ----------------------------------------------------------------- |
| `MediatR`                      | Implementación de CQRS (Commands y Queries con Handlers)         |
| `Entity Framework Core (InMemory)` | ORM y persistencia en memoria para pruebas                          |
| `Swagger (Swashbuckle)`        | Documentación y exploración de la API                            |
| `Unit of Work`                 | Coordinación de múltiples repositorios                           |
| `Middlewares personalizados`   | Manejo centralizado de errores y excepciones personalizadas      |
| `xUnit + coverlet`             | Tests unitarios y de integración con cobertura                   |
| `reportgenerator`              | Generación de reportes de cobertura en HTML                      |

---

## 🧠 Principios y Patrones Aplicados

- **CQRS (Command Query Responsibility Segregation)**: separación entre operaciones de lectura y escritura usando MediatR.
- **DRY y SOLID**: el código sigue principios de diseño limpio y reutilizable.
- **DTOs y Mappers**: se utiliza una capa de transformación entre entidades y objetos de transferencia.
- **Manejo de errores**: mediante excepciones personalizadas (`NotFoundException`, `ValidationException`, etc.).

---

## 🧪 Testing

- ✅ Pruebas unitarias completas.
- ✅ Pruebas de integración con WebApplicationFactory.
- ✅ 100% cobertura de código validada con Coverlet + ReportGenerator.
- ✅ Estrategia de testing ubicada según la arquitectura Onion.

---

## 📂 Estructura General del Proyecto

FS.FakeTwitter.sln │ ├── FS.FakeTwitter.Api # Capa de presentación ├── FS.FakeTwitter.Application # Lógica de negocio, CQRS, servicios ├── FS.FakeTwitter.Domain # Entidades e interfaces del dominio ├── FS.FakeTwitter.Infrastructure # Acceso a datos, EF Core, repositorios ├── tests # Pruebas unitarias e integración


---

## 🚀 Consideraciones de Escalabilidad y Extensibilidad

> Se agregará una sección dedicada a estrategias de base de datos y escalabilidad si lo solicitás.