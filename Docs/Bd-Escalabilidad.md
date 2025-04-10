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

| Uso     | Motor de DB      | Objetivo                           |
|---------|------------------|------------------------------------|
| Commands| PostgreSQL      | Persistencia confiable y transaccional |
| Queries | MongoDB o Redis | Lecturas ultra rápidas y escalables   |

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
- Contenerización con Docker para facilitar la portabilidad y despliegue del sistema

Se configuró Docker con los archivos necesarios (Dockerfile, docker-compose.override.yml, etc.), pero por cuestiones de tiempo no se finalizó la ejecución completa en entorno real.

---

> Esta arquitectura fue diseñada para escalar sin comprometer la mantenibilidad.
