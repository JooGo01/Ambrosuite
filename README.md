# 🍽️ Ambrosuite – Sistema de Gestión para Restaurantes

Ambrosuite es un sistema web desarrollado para optimizar la operativa interna de restaurantes. Ofrece herramientas para la gestión de pedidos, mesas, productos, inventarios y usuarios, con control de roles y funcionalidades específicas para administradores, cocineros y mozos.

## 🚀 Tecnologías utilizadas

- **Backend:** ASP.NET Core
- **Frontend:** Razor Pages
- **Base de datos:** MySQL (Aurora RDS en AWS)
- **ORM:** Entity Framework Core
- **Autenticación:** JWT
- **Infraestructura:** AWS EC2 / RDS

## 📦 Estructura del proyecto

```
Ambrosuite.ApiService/
├── Controllers/           # Endpoints por entidad (Pedidos, Usuarios, Caja, etc.)
├── Entities/              # Modelos de datos
├── Data/                  # Contexto EF
├── certificado/           # Certificados SSL de desarrollo
├── appsettings.json       # Configuraciones
└── Program.cs             # Punto de entrada
```

## 🔐 Roles y funcionalidades

| Rol          | Funcionalidades principales                                   |
|--------------|---------------------------------------------------------------|
| Administrador| Gestión completa (ABMs, reportes, inventarios, usuarios)     |
| Cocinero     | Visualiza comandas, cambia estado, accede a recetas          |
| Mozo         | Visualiza y edita pedidos desde el mapa de mesas, consulta pedidos activos|

## 📋 Funcionalidades principales

- ✅ Toma de pedidos desde mapa interactivo
- ✅ Vista de comandas por parte de cocina
- ✅ Gestión de productos, recetas e inventario
- ✅ Sistema de login con control de roles
- ✅ Consulta de historial de pedidos
- ⚠️ Módulo de caja y control de desperdicios (parcialmente implementado)

## 🧑‍💻 Autores

- Matías Molina – [@matias.molina@davinci.edu.ar](mailto:matias.molina@davinci.edu.ar)
- Esteban Inturri – [@esteban.inturri@davinci.edu.ar](mailto:esteban.inturri@davinci.edu.ar)
- Sebastián Paz Friaz – [@sebastian.paz@davinci.edu.ar](mailto:sebastian.paz@davinci.edu.ar)
- José Gómez – [@jose.gomez@davinci.edu.ar](mailto:jose.gomez@davinci.edu.ar)

---

📌 _Este proyecto fue desarrollado como parte de la Tesis Final de la carrera de Analista de Sistemas – Escuela Da Vinci, 2025._
