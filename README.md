# microservicio

----


## 🚀 Creación del Proyecto Paso a Paso

### 1️⃣ Crear un nuevo proyecto ASP.NET Core Web API


### 2️⃣ Agregar Entity Framework Core  -  SQL Server -  Tools  - Design
```sh
dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 3️⃣ Crear el modelo de datos
Crea una carpeta `Models` y dentro un archivo `Producto.cs`:

```csharp


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
    "DefaultConnection": "Server=DESKTOP-U0C9MEA\\SQLEXPRESS; Database=microserviciosDB; Trusted_Connection=True; TrustServerCertificate=True"

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


  -----------------------------


  ![image](https://github.com/user-attachments/assets/6f33323a-798a-44f9-9331-4561eccaba69)

![image](https://github.com/user-attachments/assets/57f3b6cc-3518-441d-8bc0-737aeaa124ad)

![image](https://github.com/user-attachments/assets/d619ec68-b6a4-40c2-ad7c-dc51c8d08456)

![image](https://github.com/user-attachments/assets/170f9084-777e-4614-9ebc-d037684d23ac)

![image](https://github.com/user-attachments/assets/86775532-ed84-4a47-b823-aad399c74501)


---------------------------
# Store Procedures SP

```sh
CREATE PROCEDURE ConsultarProductos
AS
BEGIN
    SELECT * FROM Productos;
END;
```

```sh

CREATE PROCEDURE EliminarProducto
    @Id INT
AS
BEGIN
    DELETE FROM Productos WHERE Id = @Id;
END;
```

```sh
CREATE PROCEDURE ObtenerUltimos5Productos
AS
BEGIN
    SELECT TOP 5 * FROM Productos ORDER BY Id DESC;
END;
```


Para Agregar los SP a ASP.Net
1.en el contexto de la base de datos AppDbContext
![image](https://github.com/user-attachments/assets/e290532e-477a-46ad-986c-ad695a94ad54)
2. En el controllador
![image](https://github.com/user-attachments/assets/8b0e9537-1bfb-4873-9940-4265e293ae59)
![image](https://github.com/user-attachments/assets/6c8bebe5-34f8-456c-8f16-6aa59beeb9a2)



