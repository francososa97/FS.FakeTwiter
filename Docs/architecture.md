# 🏛️ Arquitectura de FS.FakeTwitter

## 🧱 Estilo Arquitectónico: Onion Architecture

Se utilizó el patrón **Onion Architecture** con el objetivo de lograr una estructura de software desacoplada, mantenible y altamente testeable. La arquitectura está organizada en capas concéntricas:

- **Domain**: Entidades y contratos (interfaces)
- **Application**: Casos de uso, CQRS (Commands y Queries con MediatR), servicios, validaciones y DTOs
- **Infrastructure**: Implementaciones de repositorios, acceso a datos, UnitOfWork, servicios auxiliares y cache
- **Api**: Controllers, middlewares, configuración de Swagger, seguridad, puntos de entrada

```plaintext
FS.FakeTwitter.sln
│
├── src/
│   ├── FS.FakeTwitter.Api             # Capa de presentación (controllers, middlewares, Swagger)
│   ├── FS.FakeTwitter.Application     # CQRS, servicios, DTOs, validaciones, interfaces
│   ├── FS.FakeTwitter.Domain          # Entidades y contratos (interfaces de repositorio)
│   └── FS.FakeTwitter.Infrastructure  # EF Core, UnitOfWork, Repositorios, Cache
│
├── tests/
│   ├── FS.FakeTwitter.UnitTests         # Tests unitarios por handler / validador
│   └── FS.FakeTwitter.IntegrationTests  # Tests de integración con WebApplicationFactory
```

---

## ⚙️ Tecnologías y Librerías Clave

- **.NET 8 + C#**: framework principal
- **Entity Framework Core (InMemory)**: acceso a datos (test/dev)
- **PostgreSQL**: motor recomendado para producción
- **MediatR**: implementación de CQRS con Commands, Queries y Handlers
- **FluentValidation**: validaciones por comando, desacopladas
- **Swagger (Swashbuckle)**: documentación de la API
- **Middlewares personalizados**: manejo de errores global
- **UnitOfWork + Repositories**: control de cambios desacoplado
- **JWT + API Key**: doble autenticación
- **IMemoryCache**: cache en memoria para followers y timelines

---

### ⚡ Optimización de Lecturas con Cache

La aplicación está optimizada para lecturas, priorizando la performance y la escalabilidad. Para lograrlo, se implementó un mecanismo de **caching en memoria** usando `IMemoryCache`, encapsulado en un helper reutilizable (`CacheHelper`). Este cache mejora significativamente los tiempos de respuesta para operaciones frecuentes, como:

- 🧵 **Timeline de un usuario**: cacheado por `timeline:{userId}`, evitando rearmar la lista desde la base de datos en cada solicitud.
- 👥 **Lista de followers/followings**: cacheado por usuario, reduciendo lecturas repetidas y joins innecesarios.

El cache se invalida automáticamente al realizar acciones que alteren los datos (por ejemplo: publicar un tweet o seguir a un usuario).

> Este enfoque reduce la carga sobre la base de datos, mejora la latencia de respuesta y está preparado para ser escalado fácilmente a una solución como Redis en producción.

## 📈 Optimización de Lecturas

- Se incorporó caching con `IMemoryCache` para evitar reconsultas a la base de datos en:
  - Timeline de usuarios
  - Seguidores y seguidos
  - Tweets propios
- Resultado: reducción de tiempo promedio de respuesta de **19 ms** a **7 ms**

---

## 🔐 Seguridad

- Se utiliza autenticación dual mediante **JWT Bearer Token** y **API Key**
- Los endpoints se protegen con `[Authorize]`
- El endpoint de login genera el JWT y se encuentra en `/api/auth/login`

---

## 📦 Patrón CQRS aplicado con MediatR

- Comandos: PostTweetCommand, CreateUserCommand, FollowUserCommand, etc.
- Queries: GetUserTweetsQuery, GetTimelineQuery, GetAllUsersQuery, etc.
- Cada handler interactúa con servicios que utilizan el UnitOfWork

---

## 🔄 Cache

Se implementó cache con `IMemoryCache` encapsulado en la clase `CacheHelper`, lo cual permite:

- Cachear timelines por usuario (`timeline:{userId}`)
- Cachear followers y followings
- El cache se invalida en operaciones de escritura (follow, post tweet)

---

## 🧪 Testing y Cobertura

- 100% cobertura de código (unitaria + integración)
- Se utiliza Coverlet + ReportGenerator
- Script automatizado `run-tests-with-coverage.ps1` genera el reporte

---

## 🐳 Docker (en progreso)

- Se configuró el `Dockerfile` y `docker-compose.override.yml` para levantar la API y base de datos PostgreSQL
- Por falta de tiempo no se logró completar la ejecución satisfactoria del entorno en contenedor

---

## 🧠 Principios y Buenas Prácticas

- **Single Responsibility** por comando/servicio
- **Separation of Concerns** entre infraestructura y dominio
- **DRY / SOLID**
- **Soft Delete** en usuarios (propiedad `IsDeleted`)
- **API consistente** mediante `ApiResponse<T>` estándar

---

> Esta es la arquitectura actual del proyecto FS.FakeTwitter. Fue diseñada para escalar horizontalmente.