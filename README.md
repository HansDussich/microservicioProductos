# microservicio

----


## 🚀 Creación del Proyecto Paso a Paso

### 1️⃣ Crear un nuevo proyecto Web API en .NET
```sh
dotnet new webapi -n MiApiCrud
cd MiApiCrud
```

### 2️⃣ Agregar Entity Framework Core y el proveedor de SQL Server
```sh
dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 3️⃣ Crear el modelo de datos
Crea una carpeta `Models` y dentro un archivo `Producto.cs`:

```csharp
namespace MiApiCrud.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
}
```

### 4️⃣ Crear el contexto de base de datos
Crea la carpeta `Data` y dentro un archivo `AppDbContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using MiApiCrud.Models;

namespace MiApiCrud.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Producto> Productos { get; set; }
}
```

### 5️⃣ Configurar la conexión a SQL Server
Edita `appsettings.json` y agrega la conexión:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MiApiDb;User Id=sa;Password=TuContraseña;"
}
```

### 6️⃣ Registrar Entity Framework en `Program.cs`
Edita `Program.cs` y agrega:

```csharp
using MiApiCrud.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
```

### 7️⃣ Crear la base de datos con Entity Framework
```sh
dotnet ef migrations add Inicial

dotnet ef database update
```

### 8️⃣ Crear el controlador CRUD
```sh
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet tool install -g dotnet-aspnet-codegenerator

dotnet aspnet-codegenerator controller -name ProductosController -async -api -m Producto -dc AppDbContext -outDir Controllers
```

### 9️⃣ Ejecutar la aplicación
```sh
dotnet run
```

### 🔟 Acceder a Swagger
Abre en el navegador:
```
http://localhost:5000/swagger
```
(Si el puerto es diferente, verifica el terminal)

---

## 🧪 Pruebas de la API
Puedes probar los endpoints en **Postman** o desde la línea de comandos:

- **Obtener productos:**
  ```sh
  curl -X GET http://localhost:5000/api/productos
  ```

- **Agregar producto:**
  ```sh
  curl -X POST http://localhost:5000/api/productos -H "Content-Type: application/json" -d '{"nombre":"Laptop", "precio": 999.99}'
  ```
