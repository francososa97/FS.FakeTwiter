# FS Framework API

Este repositorio contiene la API del proyecto FS Framework, construida para proporcionar servicios robustos, escalables y eficientes. La API está desarrollada utilizando tecnologías modernas para facilitar la integración y el desarrollo de aplicaciones.

## Requisitos

- Node.js (v18 o superior)
- NPM / Yarn
- Base de datos PostgreSQL

## Instalación

1. **Clona el repositorio:**

```bash
git clone https://github.com/francososa97/fs-framework-api.git


Instala las dependencias:
bash
Copiar
Editar
npm install
# o
yarn install
Configura las variables de entorno:
Crea un archivo .env en la raíz del proyecto con la siguiente estructura:

env
Copiar
Editar
PORT=3000
DATABASE_URL=postgres://usuario:contraseña@localhost:5432/fs_framework
JWT_SECRET=tu_secreto_para_jwt
Ejecuta las migraciones:
bash
Copiar
Editar
npm run migrate
# o
yarn migrate
Ejecución
Para ejecutar la aplicación en modo desarrollo:

bash
Copiar
Editar
npm run dev
# o
yarn dev
Para ejecutar en modo producción:

bash
Copiar
Editar
npm run build
npm start
# o
yarn build
yarn start
Documentación de la API
La documentación está generada con Swagger. Para acceder localmente visita:

bash
Copiar
Editar
http://localhost:3000/api-docs
Estructura del proyecto
bash
Copiar
Editar
fs-framework-api/
├── controllers/    # Controladores
├── routes/         # Rutas
├── services/       # Lógica del negocio
├── models/         # Modelos de datos
├── middlewares/    # Middlewares
├── utils/          # Utilidades
└── tests/          # Pruebas unitarias e integración
Pruebas
Ejecuta las pruebas usando:

bash
Copiar
Editar
npm test
# o
yarn test
Contribución
Las contribuciones son bienvenidas. Por favor, realiza un pull request describiendo los cambios propuestos y asegurándote que el código cumple con las guías del proyecto.

Autor
Franco Sosa (GitHub)
Licencia
Este proyecto está bajo licencia MIT. Consulta el archivo LICENSE para más información.

go
Copiar
Editar



Puedes copiar y pegar directamente este contenido en tu archivo `README.md`.

