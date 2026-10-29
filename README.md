# Examen-Final

API REST de Gestión de Accidentes de Tránsito de Tarija (`TransitoTarija`).

## Configuración local

Las credenciales no se guardan en el repositorio. Desde la carpeta `TransitoTarija`:

```powershell
dotnet user-secrets set "ConnectionStrings:TransitoConnection" "Host=localhost;Port=5432;Database=TransitoTarija;Username=postgres;Password=TU_PASSWORD"
dotnet user-secrets set "Jwt:Key" "una-clave-larga-de-al-menos-32-caracteres"
dotnet user-secrets set "Auth:Password" "CLAVE_DE_ACCESO_PARA_EL_LOGIN"
```

## Base de datos y datos de prueba

```powershell
dotnet ef database update
dotnet run -- --seed
```

`--seed` completa hasta 300 registros por tabla y no duplica los datos ya existentes. Incluye el oficial `ESC-78921` para iniciar sesión.

## Ejecución

```powershell
dotnet run
```

Swagger: http://localhost:5000/swagger
