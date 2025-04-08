# 🧩 Base de Datos y Estrategias de Escalabilidad

## 🛢️ Motor de Base de Datos (Producción)

Durante el desarrollo se utilizó `EF Core InMemory` para permitir una experiencia de desarrollo ligera y enfocada en la lógica de negocio. Para entornos reales de producción se recomienda:

### ✅ PostgreSQL (Relacional)
- Soporte avanzado para relaciones complejas
- Transacciones ACID garantizadas
- Escalabilidad con particionado, materialized views y índices GIN
- Open Source, seguro y ampliamente utilizado

### ✅ MongoDB (No Relacional)
- Ideal para queries de lectura rápidas
- Flexible, schema-less y orientado a documentos
- Escalabilidad horizontal nativa mediante sharding

# 🧩 Base de Datos y Estrategias de Escalabilidad

## 🛢️ Motor de Base de Datos (Producción)

Durante el desarrollo se utilizó `EF Core InMemory` para permitir una experiencia de desarrollo ligera y enfocada en la lógica de negocio. Para entornos reales de producción se recomienda:

### ✅ PostgreSQL (Relacional)
- Soporte avanzado para relaciones complejas
- Transacciones ACID garantizadas
- Escalabilidad con particionado, materialized views y índices GIN
- Open Source, seguro y ampliamente utilizado

### ✅ MongoDB (No Relacional)
- Ideal para queries de lectura rápidas
- Flexible, schema-less y orientado a documentos
- Escalabilidad horizontal nativa mediante sharding

---

## 🤔 ¿Por qué PostgreSQL frente a otras opciones?

| Característica | PostgreSQL | SQL Server | Oracle |
| --- | --- | --- | --- |
| **Licencia** | 100% Open Source (sin costos) | Licencia propietaria (MS) | Licencia propietaria (muy costosa) |
| **Compatibilidad multiplataforma** | Total (Linux, Windows, macOS) | Limitada fuera de Windows | Requiere configuraciones avanzadas |
| **Escalabilidad horizontal** | Muy buena (sharding, partitions) | Limitada, foco en vertical | Buena pero compleja de implementar |
| **Índices avanzados (GIN, GiST, etc.)** | ✅ Incluidos por defecto | ❌ Limitados o ausentes | ✅ Pero requieren tuning avanzado |
| **JSON nativo y consultas mixtas** | ✅ Excelente soporte | Parcial | Bueno pero menos intuitivo |
| **Extensibilidad (PostGIS, etc.)** | Muy alta | Limitada | Alta, pero con licencias costosas |
| **Comunidad y soporte** | Muy activa, constante innovación | Activa pero controlada por MS | Activa, orientada a grandes empresas |

> Elegimos PostgreSQL por su equilibrio ideal entre robustez, rendimiento, flexibilidad, escalabilidad y costo (cero). Su comunidad lo mantiene actualizado y preparado para desafíos de alto volumen de datos.

---

## 🔄 Sincronización Relacional + NoSQL (CQRS Read Store)

La arquitectura implementada (Onion + CQRS + Unit of Work) nos permite desacoplar fácilmente las operaciones de lectura y escritura, por lo que podemos configurar:

| Uso | Motor de DB | Objetivo |
|-----|-------------|----------|
| Commands | PostgreSQL | Persistencia confiable y transaccional |
| Queries  | MongoDB o Redis | Lecturas ultra rápidas, escalables |

### Sincronización (básica):
1. Cada vez que se ejecuta un comando (ej: seguir usuario), se persiste en PostgreSQL.
2. Se genera un evento de "usuario seguido".
3. Ese evento lo consume un listener que actualiza la proyección en MongoDB o Redis.

Esto permite mantener ambas bases sincronizadas sin afectar la performance del usuario.

---

## 📈 Escalabilidad horizontal

Nuestra arquitectura soporta fácilmente escenarios de millones de usuarios:

- API escalable mediante contenedores (Docker, Kubernetes)
- Capa de aplicación desacoplada: cada Handler es independiente
- Escalado de lectura mediante:
  - Base de datos NoSQL
  - Caching con Redis
  - Indexación específica (ej: timeline por ID de seguido)
- Tolerancia a fallos con colas (RabbitMQ / Kafka) en procesamiento de eventos

---

## 🧠 Decisiones Clave

| Decisión | Justificación |
|----------|----------------|
| Onion Architecture | Permite escalar en complejidad sin afectar el core del sistema |
| CQRS | Separación clara entre lectura y escritura |
| MediatR | Flujo claro, trazabilidad y testabilidad |
| InMemory DB | Testing rápido y sin dependencias |
| PostgreSQL + MongoDB | Persistencia robusta + performance en consultas |

---

## 🚀 Mejoras Futuras

- Incorporar autenticación con JWT y roles
- Incorporar eventos de dominio + Event Sourcing
- Utilizar ElasticSearch para búsquedas complejas (hashtags, usuarios)
- Sharding por ID de usuario para separar datos entre regiones o clusters

---

> Esta arquitectura fue pensada para crecer: nuevos servicios (como notificaciones, mensajes directos o hashtags) pueden agregarse sin afectar lo ya construido.




---

## 🔄 Sincronización Relacional + NoSQL (CQRS Read Store)

La arquitectura implementada (Onion + CQRS + Unit of Work) nos permite desacoplar fácilmente las operaciones de lectura y escritura, por lo que podemos configurar:

| Uso | Motor de DB | Objetivo |
|-----|-------------|----------|
| Commands | PostgreSQL | Persistencia confiable y transaccional |
| Queries  | MongoDB o Redis | Lecturas ultra rápidas, escalables |

### Sincronización (básica):
1. Cada vez que se ejecuta un comando (ej: seguir usuario), se persiste en PostgreSQL.
2. Se genera un evento de "usuario seguido".
3. Ese evento lo consume un listener que actualiza la proyección en MongoDB o Redis.

Esto permite mantener ambas bases sincronizadas sin afectar la performance del usuario.

---

## 📈 Escalabilidad horizontal

Nuestra arquitectura soporta fácilmente escenarios de millones de usuarios:

- API escalable mediante contenedores (Docker, Kubernetes)
- Capa de aplicación desacoplada: cada Handler es independiente
- Escalado de lectura mediante:
  - Base de datos NoSQL
  - Caching con Redis
  - Indexación específica (ej: timeline por ID de seguido)
- Tolerancia a fallos con colas (RabbitMQ / Kafka) en procesamiento de eventos

---

## 🧠 Decisiones Clave

| Decisión | Justificación |
|----------|----------------|
| Onion Architecture | Permite escalar en complejidad sin afectar el core del sistema |
| CQRS | Separación clara entre lectura y escritura |
| MediatR | Flujo claro, trazabilidad y testabilidad |
| InMemory DB | Testing rápido y sin dependencias |
| PostgreSQL + MongoDB | Persistencia robusta + performance en consultas |

---

## 🚀 Mejoras Futuras

- Incorporar autenticación con JWT y roles
- Incorporar eventos de dominio + Event Sourcing
- Utilizar ElasticSearch para búsquedas complejas (hashtags, usuarios)
- Sharding por ID de usuario para separar datos entre regiones o clusters

---

> Esta arquitectura fue pensada para crecer: nuevos servicios (como notificaciones, mensajes directos o hashtags) pueden agregarse sin afectar lo ya construido.
