# FS.FakeTwitter

> Challenge técnico de Ualá: Plataforma estilo Twitter desarrollada en **.NET 8 + C#**, sin frontend, utilizando arquitectura Onion, EF Core InMemory, CQRS con MediatR, Unit of Work y manejo de excepciones personalizadas.

---

✅ **Nota importante**  
Este proyecto ahora **incluye autenticación**, utilizando:

- 🔐 **API Key** (modo rápido para testeo).
- 🔐 **JWT** (modo seguro y realista con login).

Podés consumir los endpoints protegidos usando cualquiera de las dos estrategias.

---

## 🔐 Seguridad y Autenticación

### 🔑 API Key

Agregá el siguiente header para autenticarte con API Key:

```http
X-API-KEY: super-secret-key
```

> Este modo es útil para pruebas rápidas en Swagger o Postman.

---

### 🔓 JWT (Login real con credenciales)

1. Hacé login en:

```
POST /api/Auth/login
```

2. Usá el siguiente `body` de prueba:

```json
{
  "email": "admin",
  "password": "admin123"
}
```

3. El response incluirá un token JWT:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR..."
}
```

4. Usalo en los headers como:

```http
Authorization: Bearer {token}
```

> JWT válido por 3 horas. El payload incluye `sub`, `jti` y `name`.

---

## ✅ Validaciones con FluentValidation

Se integró **FluentValidation** para manejar reglas de negocio de forma declarativa. Incluye:

- Validaciones sincrónicas en todos los comandos (`PostTweet`, `FollowUser`, etc.).
- Validaciones asíncronas (como evitar seguir dos veces al mismo usuario).
- Mensajes personalizados (ej: "juan ya sigue a pedro").

> Las validaciones asíncronas se manejan dentro de los `Handlers` para evitar conflictos con la validación automática de ASP.NET Core.

---

## 🧪 Ver cobertura de tests (usando script automatizado)

Para ejecutar los tests y generar el reporte de cobertura en formato HTML con un solo comando, podés usar el script incluido:

```bash
./run-tests-with-coverage.ps1
```

Este script:

1. Ejecuta todos los tests (unitarios e integración).
2. Recolecta la cobertura con Coverlet.
3. Genera un reporte visual en HTML con `reportgenerator`.
4. Abre automáticamente el reporte en tu navegador.

📁 El archivo `run-tests-with-coverage.ps1` se encuentra en la raíz del proyecto.

> ⚠️ Asegurate de tener permisos de ejecución habilitados para scripts en PowerShell. Si es la primera vez que lo usás, podés ejecutar:

```powershell
Set-ExecutionPolicy RemoteSigned -Scope Process
```
---

## 📁 Estructura del Proyecto

```plaintext

FS.FakeTwitter.sln
│
├── src/
│   ├── FS.FakeTwitter.Api             # API + Swagger + Auth
│   ├── FS.FakeTwitter.Application     # CQRS + Reglas de negocio
│   ├── FS.FakeTwitter.Domain          # Entidades + Interfaces
│   └── FS.FakeTwitter.Infrastructure  # Repositorios, servicios, DbContext, UnitOfWork
│
├── tests/
│   ├── FS.FakeTwitter.UnitTests         # Unit tests
│   └── FS.FakeTwitter.IntegrationTests  # Integration tests + coverage
│
├── Collections/
│   └── 🧪 FS.FakeTwitter API.postman_collection.json
│       Colección de Postman para probar los endpoints de la API.
│       Incluye ejemplos de login, tweets, timeline, follow/unfollow.
│
├── Docs/
    ├── 📄 api-documentation.pdf
    │   Documentación de la API generada con Swagger.
    │   Incluye ejemplos de uso y respuesta de cada endpoint.
    │
    ├── 📘 architecture.md
    │   Documento explicando la arquitectura Onion actual del proyecto.
    │   Detalla las capas: Domain, Application, Infrastructure y Api.
    │
    ├── 🧠 Bd-Escalabilidad.md
    │   Consideraciones técnicas para escalar la base de datos.
    │   Incluye detalles sobre PostgreSQL, JSONB, sharding y uso de Redis/Mongo.
    │
    └── 💼 BUSINESS.md
       Documento con decisiones técnicas y reglas asumidas.
       Incluye objetivos del sistema, tecnologías elegidas y posibles mejoras futuras.
```

---

## 🪧 Tecnologías Utilizadas

- .NET 8
- C#
- MediatR (CQRS)
- Entity Framework Core (InMemory)
- FluentValidation
- Swagger (OpenAPI)
- Autenticación: API Key + JWT
- Arquitectura Onion
- Unit of Work
- Excepciones personalizadas

---

## 🌐 Endpoints principales

### Tweets

- `POST /api/tweet` - Publicar tweet
- `GET /api/tweet/user/{userId}` - Ver tweets propios
- `GET /api/tweet/timeline/{userId}` - Ver timeline  con tweets de los seguidos

### Follows

- `POST /api/follow` - Seguir usuario
- `GET /api/follow/followers/{userId}` - Ver seguidores
- `GET /api/follow/following/{userId}` - Ver seguidos

### Auth

- `POST /api/auth/login` - Obtener JWT

### Users

- `POST /api/user` - Crear un nuevo usuario
- `GET /api/user` - Obtener todos los usuarios
- `GET /api/user/{id}` - Obtener un usuario por ID
- `PUT /api/user/{id}` - Actualizar un usuario
- `DELETE /api/user/{id}` - Eliminar un usuario (soft delete)

---

## 🤖 CQRS con MediatR

Todos los casos de uso se modelan como comandos/queries:

- `PostTweetCommand` / `FollowUserCommand`
- `GetUserTweetsQuery` / `GetTimelineQuery`
- `GetFollowersQuery` / `GetFollowingQuery`

---

## 🛠️ Unit of Work

Patrón `IUnitOfWork` para mantener consistencia entre operaciones de DB.

---

## 🛑 Excepciones Personalizadas

Errores centralizados en middleware:

- `NotFoundException`
- `ValidationException`
- `UnauthorizedException`

---

## 📖 Documentación Swagger

Toda la API está documentada en Swagger UI, accesible desde:

```
http://localhost:5000/swagger
```

---

## ✅ Ejecutar el proyecto localmente

```bash
cd FS.FakeTwitter.Api
dotnet run
```

---

## 🌟 Estado actual

- [x] Arquitectura Onion
- [x] CQRS con MediatR
- [x] FluentValidation integrado
- [x] Validaciones asíncronas en handlers
- [x] Autenticación JWT + API Key
- [x] Documentación Swagger + Tests
- [x] 97% cobertura con reporte automatizado
- [x] Swagger documentado y funcional
- [x] EF Core InMemory + UoW operativo
- [x] Control de errores con excepciones personalizadas
- [x] Script para test coverage automatizado
- [x] Archivos Dockerfile y docker-compose.yml creados (funcionalidad pendiente de validación completa)
- [x] Se logró una **reducción de tiempo de respuesta de 19 ms a 3ms** (milisegundos) promedio en endpoints de lectura intensiva mediante la incorporación de cache (`IMemoryCache`) en consultas de timelines y seguidores.

---

> Proyecto desarrollado como parte del proceso técnico de Ualá por Franco Damián Sosa