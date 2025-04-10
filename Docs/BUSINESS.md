
# 📘 Business.txt - FS.FakeTwitter

Este archivo documenta las decisiones técnicas, arquitectónicas y de escalabilidad tomadas en el desarrollo del proyecto **FS.FakeTwitter**.

---

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
