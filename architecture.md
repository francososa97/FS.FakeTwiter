## Base de datos

Durante el desarrollo y testing se utiliza `Microsoft.EntityFrameworkCore.InMemory` para mantener el proyecto ligero y sin dependencias externas. Esta implementaci�n permite levantar y testear el sistema f�cilmente, persistiendo datos en memoria.

### Alternativa para producci�n

Para producci�n se sugiere el uso de **PostgreSQL**, por su soporte a relaciones complejas, facilidad de escalar horizontalmente y robustez frente a cargas altas.
