# 🧠 API de Gestión de Micros

API REST desarrollada en **.NET Core 8**, encargada de gestionar entidades como micros, choferes y chicos. Utiliza una arquitectura limpia basada en MediatR, Entity Framework, AutoMapper y FluentValidation. Incluye logging centralizado con ElasticSearch + Kibana y está dockerizada para desarrollo y producción.

---

## 🚀 Tecnologías utilizadas

- **.NET Core 8**
- **Entity Framework Core + Migrations**
- **MySQL** como base de datos
- **MediatR** para orquestación de peticiones
- **FluentValidation** para validaciones declarativas
- **AutoMapper** para mapeo entre entidades y DTOs
- **Swagger / Swagger UI** para documentación de endpoints
- **Serilog + ElasticSearch + Kibana** para logging estructurado
- **Docker + Docker Compose** para despliegue y entorno de desarrollo

---

## 🧩 Arquitectura

```
Controller → MediatR → Handler → Service → EF Core → Entity Repository → AutoMapper → DTO
```

- Los **Controllers** reciben las peticiones HTTP.
- Se delega la lógica a **Handlers** mediante **MediatR**.
- Las validaciones se realizan con **FluentValidation** antes de ejecutar la lógica.
- Los **Handlers** orquestan los **Services**, que acceden a la base de datos vía **Entity Framework**.
- Las entidades se transforman en DTOs mediante **AutoMapper**.

---

## 📦 Requisitos para desarrollo

- [.NET SDK 8](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- [Docker](https://www.docker.com/) + [Docker Compose](https://docs.docker.com/compose/)

---


## 🛠️ Entorno de desarrollo

### 🔧 Cómo levantar los servicios

Usá el archivo `docker-compose.dev.yml` para levantar los servicios necesarios para desarrollo local:

```bash
docker-compose -f docker-compose.dev.yml up --build
```

Esto levanta:

- **MySQL** en el puerto `3306`
- **ElasticSearch** en los puertos `9200` y `9300`
- **Kibana** en el puerto `5601`

La API se ejecuta desde Visual Studio para facilitar debugging y hot reload.

---

### 🗄️ Cómo conectarse a MySQL con una UI

Podés usar cualquier cliente MySQL, pero recomendamos [**DBeaver Community**](https://dbeaver.io/download/) por ser gratuito, multiplataforma y potente.

#### 📌 Configuración para conectarse (por defecto)

- **Host**: `localhost`
- **Puerto**: `3306`
- **Base de datos**: `compra_gamer_db`
- **Usuario**: `gameruser`
- **Contraseña**: `gamerpass123`

#### 🪜 Pasos en DBeaver

1. Abrí DBeaver y creá una Nueva Conexión → MySQL.
2. Completá los campos con la configuración anterior.
3. Testeá la conexión y guardá. Puede que te pida instalar drivers con una ventana emergente, dale a la opción para descargar que te brinda la misma ventana y espera a que finalice.
4. Ya podés explorar tablas, ejecutar queries y revisar datos.

---

### 🧱 Aplicar migraciones de base de datos

Antes de ejecutar la API, asegurate de aplicar las migraciones para crear las tablas necesarias en MySQL:

```bash
dotnet ef database update InitialCreate --startup-project ChallengeCompraGamer_Backend.App --project ChallengeCompraGamer_Backend.DataAccess
```

Este comando:

- Aplica todos las migraciones al esquema de `compra_gamer_db` para que la base de datos este al día.
- Usa el proyecto `ChallengeCompraGamer_Backend.App` como punto de entrada y `ChallengeCompraGamer_Backend.DataAccess` como origen de las migraciones.

> 💡 Asegurate de que los contenedores estén corriendo (`docker-compose.dev.yml`) antes de ejecutar este comando.


---


### 📊 Cómo consultar logs en Kibana

Una vez levantado el entorno, accedé a Kibana desde tu navegador:

```
http://localhost:5601
```

- No requiere autenticación (la seguridad está desactivada para desarrollo).
- Podés ir a **Discover** para explorar los logs.
- Usá filtros por `@timestamp`, `log.level`, `message`, etc.
- Si configuraste index patterns como `logs-*`, seleccionalos para visualizar los eventos.
- Para información mas detalla sobre cómo usar Kibana, consultá la [documentación oficial de Kibana](https://www.elastic.co/guide/en/kibana/current/index.html).

---

### ❌ Documentación de errores de validación

Cuando una petición HTTP falla por errores de validación (por ejemplo, campos requeridos vacíos o valores fuera de rango), la API responde con un objeto JSON estructurado según el estándar [RFC 7807](https://tools.ietf.org/html/rfc7807), usando el formato `application/problem+json`.

#### 📄 Ejemplo de respuesta 400 por validación

```json
{
  "type": "https://tools.ietf.org/html/rfc7807",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "instance": "/api/Micro",
  "errors": {
    "Body.Modelo": [
      "El modelo no puede estar vacío."
    ],
    "Body.CantidadAsientos": [
      "La cantidad de asientos debe ser mayor que cero."
    ]
  }
}
```

#### ⚙️ ¿Cómo se genera esta respuesta?

La API utiliza un middleware personalizado (`ExceptionHandlingMiddleware`) que intercepta excepciones de tipo `FluentValidation.ValidationException`. Cuando ocurre una validación fallida:

- Se agrupan los errores por propiedad.
- Se construye un objeto `ValidationProblemDetails`.
- Se devuelve con status `400 Bad Request` y tipo `application/problem+json`.

Este enfoque garantiza respuestas consistentes, estructuradas y fácilmente parseables por el frontend.

#### 📌 ¿Por qué se documenta en el README y no en Swagger?

Swagger (via `[SwaggerResponse]`) documenta los tipos de respuesta esperados por endpoint, pero **no puede mostrar ejemplos dinámicos de errores de validación generados por middlewares**. Como esta respuesta no está directamente ligada a un `return` en el controller, sino que se genera en tiempo de ejecución por el pipeline de ASP.NET Core, se documenta explícitamente en el `README.md` para que los desarrolladores del frontend sepan cómo manejarla.

> 💡 Esta estructura es especialmente útil para mostrar múltiples errores simultáneos y asociarlos a campos específicos del request.

---


## 🐳 Despliegue productivo

La API está preparada para ser desplegada en entornos productivos mediante Docker. Podés levantar la imagen localmente o en plataformas como **Azure Container Instances**, **Azure App Service (Linux)** o cualquier orquestador compatible con Docker.

### 🔧 Build y ejecución local

Para construir la imagen y levantar el contenedor:

```bash
docker-compose up --build
```

Esto levanta solo la API, sin servicios auxiliares. Está lista para conectarse a:

- Una **base de datos MySQL externa o gestionada** (ej. Azure Database for MySQL)
- Un stack de **ElasticSearch + Kibana externo o gestionado** (ej. Elastic Cloud, Azure Marketplace)

> 💡 Asegurate de configurar las variables de entorno en `appsettings.Production.json` o mediante `ENV` en el `Dockerfile` para apuntar a los servicios remotos.

> Para conectarse a servicios externos, asegurate de que las IPs o redes estén permitidas en los firewalls de MySQL y Elastic.

---

## 📁 Estructura del proyecto

```
ChallengeCompraGamer_Backend/
├── ChallengeCompraGamer_Backend.sln         # Solución principal
├── Dockerfile                               # Imagen para despliegue productivo
├── docker-compose.yml                       # Orquestación de la API en producción
├── docker-compose.dev.yml                   # Servicios auxiliares para desarrollo (MySQL, Elastic, Kibana)

├── ChallengeCompraGamer_Backend.App/        # Proyecto principal de la API
│   ├── Program.cs                           # Punto de entrada de la aplicación
│   ├── LoggerConfig.cs                      # Configuración de logging con Elastic
│   ├── appsettings.*.json                   # Configuraciones por entorno
│
│   ├── Controllers/                         # Endpoints HTTP
│
│   ├── Commands/                            # Peticiones MediatR organizadas por entidad y acción
│   │   ├── Entidad1/Entidad2/Entidad3/...   # Subcarpetas por entidad
│   │   │   ├── Create/Update/Delete/...     # Acciones con Command, Handler y Validator (si se requiere)
│
│   ├── Middlewares/                         # Middleware para manejo de login, excepciones, validaciones, etc. (Por ejemplo)

├── ChallengeCompraGamer_Backend.DataAccess/ # Acceso a datos con Entity Framework
│   ├── Context/                             # DbContext principal
│   ├── Entities/                            # Entidades del dominio
│   ├── Configurations/                      # Configuración de entidades (fluent API)
│   ├── Migrations/                          # Migraciones EF Core para versionado de esquema

├── ChallengeCompraGamer_Backend.Models/     # DTOs de entrada/salida organizados por entidad y acción
│   ├── Entidad1/Entidad2/Entidad3/...       # Subcarpetas por entidad
│   │   ├── Create/Update/Delete/...         # Subacciones con RequestDTO, ResponseDTO u otros que se requieran.
│   └── Result.cs                            # Modelo base para respuestas estándar

├── ChallengeCompraGamer_Backend.Services/   # Lógica de negocio y acceso a base de datos
│   ├── Entidad1Service.cs / Entidad2Service.cs / ...
│   └── Maps/                                # Configuración de AutoMapper por entidad
```

---
## 📐 Diagrama Entidad-Relación (DER)

Este proyecto modela tres entidades principales: **Chico**, **Micro** y **Chofer**, con relaciones bien definidas entre ellas.

### 🧑‍🎓 Chico

- Identificador único: `DNI`
- Cada chico puede estar asignado a **un único micro**
- Relación: **Muchos a Uno** con `Micro`

### 🚌 Micro

- Identificador único: `Patente`
- Puede tener **muchos chicos asignados**
- Debe tener **un único chofer asignado**
- Relación:
  - **Uno a Muchos** con `Chico`
  - **Uno a Uno** con `Chofer`

### 🧑‍✈️ Chofer

- Identificador único: `DNI`
- Puede ser asignado a **un único micro**
- No puede estar asignado a más de un micro a la vez
- Relación: **Uno a Uno** con `Micro`

### 🔗 Relaciones

```
Chico (N) ──────┐
                │
                ▼
              Micro (1) ────── Chofer (1)
```

- Un **Micro** puede tener **muchos chicos**.
- Un **Micro** debe tener **un chofer**.
- Un **Chofer** solo puede manejar **un micro**.
- Un **Chico** solo puede viajar en **un micro**.

> 💡 Estas relaciones están reflejadas en las entidades EF Core y configuradas mediante fluent API

---

## 🧭 Guía técnica y convenciones



### 📦 Versionado y changelog

Esta sección documenta cómo se gestionan las versiones del proyecto.

#### 🪛 Convenciones de ramas

Para mantener una estructura clara en el desarrollo, se siguen estas convenciones de nombres de ramas:

- `feature/nombre-feature`: para nuevas funcionalidades
- `fix/nombre-fix`: para correcciones de bugs
- `refactor/nombre-refactor`: para mejoras internas sin cambio funcional

Todas estas ramas se crean desde `develop`:

```bash
git checkout develop
git pull origin develop
git checkout -b feature/asignar-chicos-a-micro
```

Una vez finalizado el desarrollo, se hace un Pull Request hacia `develop`.

---

#### 🔥 Hotfixes en producción

Cuando se necesita aplicar una corrección urgente sobre `main` (ya desplegado en producción), se sigue este flujo:

1. Crear una rama desde `main`:
   ```bash
   git checkout main
   git pull origin main
   git checkout -b hotfix/fix-patente-duplicada
   ```

2. Aplicar el fix y testear.

3. Hacer PR hacia `main` y mergear.

4. Taggear una nueva versión:
   ```bash
   git tag release-1.0.1
   git push origin release-1.0.1
   ```

5. Mergear también hacia `develop` para mantener consistencia:
   ```bash
   git checkout develop
   git pull origin develop
   git merge hotfix/fix-patente-duplicada
   git push origin develop
   ```

> 💡 Esto evita que los fixes se pierdan en futuras versiones y mantiene `main` como fuente confiable de releases.


#### 🏷️ Convenciones de versionado

- Se utiliza **versionado semántico**: `MAJOR.MINOR.PATCH`
  - `MAJOR`: cambios incompatibles o reestructuración significativa
  - `MINOR`: nuevas funcionalidades compatibles
  - `PATCH`: correcciones menores o ajustes internos

#### 🧩 Etiquetado de releases

- Los tags se crean desde `develop` una vez estabilizada la funcionalidad:
  ```bash
  git tag release-1.0.0
  git push origin release-1.0.0
  ```
- Se crea una rama temporal desde el tag para hacer el PR hacia `main`:
  ```bash
  git checkout -b release/1.0.0 release-1.0.0
  git push origin release/1.0.0
  ```

---

### 🧑‍💻 Guía para nuevos desarrolladores

Esta sección sirve como onboarding rápido para cualquier persona que se sume al equipo.

#### 🪜 Pasos iniciales

1. Clonar el repositorio:
   ```bash
   git clone <repo-url>
   cd ChallengeCompraGamer_Backend
   ```

2. Instalar [.NET SDK 8](https://dotnet.microsoft.com/en-us/download)

3. Levantar los servicios auxiliares:
   ```bash
   docker-compose -f docker-compose.dev.yml up -d
   ```

4. Aplicar migraciones:
   ```bash
   dotnet ef database update InitialCreate --startup-project ChallengeCompraGamer_Backend.App --project ChallengeCompraGamer_Backend.DataAccess
   ```

5. Ejecutar la API desde Visual Studio abriendo la solución.

#### 🧭 ¿Dónde mirar primero?

- `Controllers/`: para entender los endpoints
- `Commands/`: para ver cómo se orquestan las peticiones
- `Services/`: para la lógica de negocio
- `Models/`: para los DTOs de entrada/salida

---

### 🧠 Convenciones de arquitectura

Esta sección formaliza las decisiones técnicas que estructuran el proyecto.

#### 🧱 Capas y responsabilidades

- **Controller**: solo recibe la petición y delega a MediatR
- **Command/Handler**: orquesta la lógica, valida y llama al servicio
- **Service**: contiene la lógica de negocio y acceso a datos
- **DataAccess**: configura EF Core y las entidades
- **Models**: define los DTOs de entrada/salida

#### 📐 Convenciones de nombres

- `CreateXCommand`, `UpdateXCommand`, etc.
- `XService.cs` para cada entidad
- `XRequestDTO` y `XResponseDTO` por acción
- `XCommandValidator.cs` para validaciones con FluentValidation

#### 🚫 Qué evitar

- Lógica en los controllers
- Acceso directo a EF desde los handlers
- Validaciones manuales en los servicios pudiendo realizarse en FluentValidation


## 📝 TODO

- [ ] **Integración con SignalR**  
  Incorporar comunicación en tiempo real para actualizaciones de estado y eventos. Esto permitirá que el frontend reciba notificaciones automáticas ante cambios en micros, asignaciones o disponibilidad.

- [ ] **Documentar endpoints faltantes en SwaggerUI**  
  Algunos endpoints aún no están reflejados correctamente en Swagger. Se deben agregar anotaciones `[ProducesResponseType]` y verificar que los DTOs estén bien representados. También se puede extender la documentación con ejemplos de request/response y errores comunes.

- [ ] **Integrar xUnit para tests unitarios**  
  Crear un proyecto `ChallengeCompraGamer_Backend.Tests` con xUnit para validar la lógica de negocio en los `Service`, `Handler` y `Validator`. Incluir casos de éxito, fallos esperados y mocks de dependencias con `Moq` y `Bogus` para definir perfiles de generación de datos de prueba.

- [ ] **Automatizar migraciones en entorno local**  
  Crear un script o comando que aplique automáticamente las migraciones al levantar el entorno de desarrollo, evitando errores por base de datos vacía.

- [ ] **Definir protocolo para cambios de firma en endpoints**  
  Establecer un procedimiento formal para cuando se modifique la estructura de entrada (`RequestDTO`) o salida (`ResponseDTO`) de un endpoint.

---