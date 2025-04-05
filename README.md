# FS.FakeTwitter

> Challenge técnico de Ualá: Plataforma estilo Twitter desarrollada con arquitectura Onion, EF Core InMemory, CQRS con MediatR, Unit of Work y manejo de excepciones personalizadas.

---

## 📁 Estructura del Proyecto

```plaintext
FS.FakeTwitter.sln
│
├── FS.FakeTwitter.Api             # Capa de presentación (controllers, Swagger, middlewares)
├── FS.FakeTwitter.Application     # Casos de uso, servicios, interfaces, CQRS (commands, queries)
├── FS.FakeTwitter.Domain          # Entidades, interfaces de repositorio (contratos del dominio)
├── FS.FakeTwitter.Infrastructure  # Repositorios, UnitOfWork, acceso a datos, servicios
├── FS.Framework                   # Librería base compartida si se desea escalar
```

---

## 🪧 Tecnologías Utilizadas

- .NET 8
- MediatR (CQRS)
- Entity Framework Core (InMemory)
- Swagger (OpenAPI)
- Arquitectura Onion
- Unit of Work
- Excepciones personalizadas

---

## 🌐 Endpoints principales

### Tweets

- `POST /api/tweet` - Publicar tweet
- `GET /api/tweet/user/{userId}` - Ver tweets propios
- `GET /api/tweet/timeline/{userId}` - Ver timeline con tweets de los seguidos

### Follows

- `POST /api/follow` - Seguir usuario
- `GET /api/follow/followers/{userId}` - Ver seguidores
- `GET /api/follow/following/{userId}` - Ver seguidos

---

## 🤖 CQRS con MediatR

Todos los accesos a la lógica de negocio se realizan a través de comandos y queries:

- `PostTweetCommand` + `PostTweetCommandHandler`
- `GetUserTweetsQuery`, `GetTimelineQuery`
- `FollowUserCommand` + `GetFollowersQuery`, `GetFollowingQuery`

---

## 📛 Configuración de Swagger

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FS.FakeTwitter API V1");
    c.RoutePrefix = string.Empty; // Swagger en la página principal
});
```

En `.csproj`:
```xml
<GenerateDocumentationFile>true</GenerateDocumentationFile>
<NoWarn>$(NoWarn);1591</NoWarn>
```

---

## ⚡ Unit of Work

Se implementó el patrón `IUnitOfWork` para coordinar los cambios entre repositorios:

```csharp
public interface IUnitOfWork
{
    ITweetRepository Tweets { get; }
    IFollowRepository Follows { get; }
    Task<int> SaveChangesAsync();
}
```

Y se utiliza en los servicios como:

```csharp
await _unitOfWork.Tweets.AddAsync(tweet);
await _unitOfWork.SaveChangesAsync();
```

---

## 🛑 Excepciones Personalizadas

Se centraliza el manejo de errores con excepciones personalizadas:

- `NotFoundException`
- `ValidationException`
- `UnauthorizedException`

---

## 📖 Documentación Swagger

Todos los endpoints están documentados con:

```csharp
/// <summary>Descripción</summary>
/// <param name="..."></param>
/// <returns>...</returns>
[ProducesResponseType(typeof(...), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
```

---

## ✅ Para ejecutar el proyecto

```bash
cd FS.FakeTwitter.Api

dotnet run
```

Y acceder a:

```
http://localhost:5000  (Swagger UI)
```

---

## 🌟 Estado actual

- [x] Arquitectura Onion 100% aplicada
- [x] CQRS con MediatR implementado
- [x] Swagger documentado y funcional
- [x] EF Core InMemory + UoW operativo
- [x] Control de errores con excepciones personalizadas

---

> Proyecto desarrollado como parte del proceso técnico de Ualá por Franco Damián Sosa
