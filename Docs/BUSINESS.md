
# 📘 Business.txt - FS.FakeTwitter

Este archivo documenta las decisiones técnicas, arquitectónicas y de escalabilidad tomadas en el desarrollo del proyecto **FS.FakeTwitter**.

---

# 🏛️ Arquitectura y Diseño de FS.FakeTwitter

## 🧱 Estilo Arquitectónico

Se aplicó el patrón **Onion Architecture**, dividido en 4 capas principales:

- **Domain**: Entidades y contratos (interfaces de repositorio)
- **Application**: Casos de uso, comandos y queries (CQRS con MediatR)
- **Infrastructure**: Acceso a datos, implementación de repositorios y servicios
- **Api**: Entrada HTTP (Controllers, Swagger, Middlewares)

```
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
```

> Esta separación permite desacoplar la lógica del negocio de los detalles de infraestructura.

---

## 🛠️ Componentes y Tecnologías Clave

- **.NET 8 + C#**
- **Entity Framework Core** (InMemory en desarrollo, PostgreSQL sugerido para producción)
- **MediatR**: para implementar CQRS
- **Swagger**: para la exploración de API
- **Custom Middlewares**: manejo centralizado de errores
- **Unit of Work + Repositorios**
- **FluentValidation**: validaciones desacopladas
- **JWT y API Key Authentication**: mecanismos de autenticación soportados

---

## 🗄️ Base de Datos recomendada (Producción)

### ✅ PostgreSQL (Relacional)
- Soporte avanzado para relaciones complejas
- Transacciones ACID garantizadas
- Escalabilidad con particionado, materialized views y índices GIN
- JSONB para almacenar seguidores embebidos

### ✅ MongoDB (No Relacional)
- Ideal para queries de lectura rápidas
- Flexible, schema-less y orientado a documentos
- Escalabilidad horizontal nativa mediante sharding

> En desarrollo se utiliza EF Core InMemory. En producción se sugiere PostgreSQL por escalabilidad y compatibilidad.

---

## 🔄 Sincronización Relacional + NoSQL (CQRS Read Store)

| Uso     | Motor de DB     | Objetivo                           |
|---------|------------------|------------------------------------|
| Commands | PostgreSQL       | Persistencia confiable y transaccional |
| Queries  | MongoDB o Redis  | Lecturas ultra rápidas y escalables   |

**Ejemplo de JSONB en PostgreSQL para seguidores:**
```json
{
  "followers": ["user-1", "user-2"]
}
```

---

## 📈 Escalabilidad y Performance

El sistema soporta hasta 1 millón de usuarios concurrentes mediante:

- API escalable vía Docker + Kubernetes
- CQRS para desacoplar escritura y lectura
- Redis/MongoDB como ReadStore
- Event Sourcing opcional con Kafka o RabbitMQ
- Caching de timelines y seguidores
- Sharding por usuario en PostgreSQL/MongoDB
- Balanceadores de carga (NGINX, Azure Front Door)

---

## 🔐 Seguridad

El sistema admite **API Key** y **JWT Bearer Token**.

- Endpoints protegidos con `[Authorize]`
- Login: `POST /api/auth/login`
  - Cuerpo: `{ "email": "admin", "password": "admin123" }`
- Token generado válido para endpoints protegidos
- Swagger soporta autorización con ambos métodos

---

## ✅ Testing

- Cobertura 100% con Coverlet + ReportGenerator
- Pruebas unitarias (handlers, servicios, validaciones)
- Pruebas de integración con WebApplicationFactory
- Validaciones con FluentValidation

---

## 📂 Estructura Modular

```
FS.FakeTwitter.sln
│
├── Api: Controllers, Middlewares, Auth
├── Application: CQRS, FluentValidation, DTOs, Interfaces
├── Domain: Entidades y contratos
├── Infrastructure: EF Core, UoW, Repositorios
├── Tests: xUnit, Integration + Unit
```

---

## 🚀 Mejoras Futuras

- Autenticación real con usuarios persistentes
- Event Sourcing + Domain Events
- ElasticSearch para búsqueda avanzada
- Clustering + Multi-tenant architecture
- CDN para contenido estático

---

> Esta arquitectura fue diseñada para escalar sin comprometer la mantenibilidad.


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
A continuación, se describen las estrategias técnicas y arquitectónicas propuestas para escalar el sistema FS.FakeTwitter y soportar al menos 1 millón de usuarios activos al mismo tiempo.

---

### 🧱 Arquitectura Modular y Desacoplada

- **Onion Architecture** + **CQRS con MediatR**: permite aislar la lógica de negocio, facilitando el escalado por capas.
- **Separación de responsabilidad** en comandos (escritura) y queries (lectura) permite escalar cada una por separado.

---

### 🗃️ Base de Datos Escalable

- **Lecturas**: se sugiere utilizar **MongoDB** o **Redis** como proyección CQRS para los timelines (rápido acceso y agregación).
- **Escrituras**: utilizar **PostgreSQL** con índices, `partitioning` y `materialized views`.
- **Seguidores**: almacenar los seguidores como un campo JSONB en PostgreSQL para cada usuario (actualizable por eventos), reduciendo `JOINs`.

```json
{
  "followers": ["user-1", "user-2", "user-3"]
}
```

---

### 🧠 Caching

- **Redis** para cachear timelines, listas de seguidores, últimos tweets, etc.
- TTL corto para consistencia eventual.

---

### 💬 Event Driven Architecture (EDA)

- **RabbitMQ** o **Kafka** para desacoplar acciones como:
  - Usuario sigue a otro ➝ genera evento ➝ se actualiza la proyección en MongoDB.
  - Nuevo tweet ➝ notificación a seguidores ➝ colas de envío async.

---

### 🧵 Concurrencia y Rendimiento

- **Load Balancers** como NGINX o Azure Front Door.
- **Instancias horizontales** de la API con **Kubernetes** o **Docker Swarm**.
- **Rate limiting** y control de sesiones si se agrega autenticación real.

---

### 🌐 CDN + Edge Caching (en caso de front-end)

- A futuro: servir assets (p.ej., imágenes de perfil o medios) vía CDN (Cloudflare, Akamai).

---

### 🔍 Observabilidad

- **Logging distribuido** (Serilog + ElasticSearch).
- **Tracing** con Jaeger o OpenTelemetry.
- **Monitoreo** con Prometheus + Grafana.

---

### ✅ Resumen de Beneficios

| Técnica                    | Beneficio                              |
|---------------------------|----------------------------------------|
| CQRS + MediatR            | Escala lectura/escritura separadamente |
| PostgreSQL + JSONB        | Reducción de JOINs complejos           |
| Redis/Mongo como ReadStore| Respuesta rápida                       |
| Kafka/RabbitMQ            | Alta concurrencia sin bloqueo directo  |
| Kubernetes                | Escalado horizontal eficiente          |

---

> Estas prácticas garantizan que la solución puede crecer a millones de usuarios concurrentes sin reescribir la arquitectura base.

## ✅ Reglas Asumidas (Assumptions)

- No se requiere autenticación de usuario ni sesiones para las operaciones principales.
- Los identificadores de usuario se pasan en el body, query o path.
- Se prioriza la optimización para lectura por sobre la escritura.
- El sistema debe poder escalar fácilmente a millones de usuarios.
- Se puede realizar pruebas en memoria sin dependencias externas.

---

## 🏛️ Arquitectura Utilizada

Se aplicó el patrón **Onion Architecture**, con capas desacopladas:

- **Domain**: Entidades y contratos (interfaces)
- **Application**: Lógica de negocio, CQRS, servicios, validaciones
- **Infrastructure**: EF Core, repositorios, Unit of Work
- **Api**: Controllers, middleware, configuración

> Esta arquitectura permite testeo aislado, escalabilidad, separación de concerns y mantenimiento a largo plazo.

---

## 🧠 Decisiones Técnicas Clave

| Tema                       | Decisión                                                                 |
|---------------------------|--------------------------------------------------------------------------|
| Arquitectura              | Onion Architecture con CQRS + MediatR                                    |
| ORM                       | Entity Framework Core (InMemory para desarrollo/pruebas)                 |
| API Docs                  | Swagger con seguridad JWT + API Key documentada                          |
| Validaciones              | FluentValidation, centralizadas por comando                              |
| Testing                   | 100% de cobertura con xUnit + Coverlet + ReportGenerator                 |
| Seguridad                 | Autenticación dual (API Key o JWT), validada vía Middleware + Schemes    |

---

## 🗄️ Motor de Base de Datos para Producción

Para producción, se recomienda utilizar **PostgreSQL** por las siguientes razones:

- Rendimiento sólido para consultas de lectura
- Escalabilidad horizontal y replicación
- Compatibilidad con `JSONB`, índices GIN y operaciones complejas
- Soporte para queries híbridas (relacional + documento)

> Ejemplo: Se podría guardar en una columna JSONB el listado de seguidores de un usuario, optimizando la lectura de timeline.

---

## 🚀 Estrategias de Escalabilidad

- Caching con Redis para timelines o queries frecuentes
- Sharding por ID de usuario (por región o clúster)
- CQRS desacoplado: lectura y escritura independientes
- MongoDB como store de lectura futura (timeline precomputado)
- Event Sourcing y colas (ej: RabbitMQ, Kafka) para futuros microservicios

---

## ✅ Mejoras Aplicadas

- Validaciones por FluentValidation (con mensajes personalizados y reglas asincrónicas)
- Autenticación con JWT y/o API Key
- Soft Delete aplicado en usuarios
- DTOs normalizados en las respuestas (ApiResponse estándar)
- Control de errores global con middleware
- Controller de autenticación documentado con Swagger
- CRUD completo de usuarios vía CQRS
