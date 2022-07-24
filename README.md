# PuraVidaStore
Es importante usar visual studio 2022

# Como usar db contex con swagger
1) instalar Microsoft.EntityFrameworkCore.SqlServer
2) Microsoft.EntityFrameworkCore.Tools

# comando para actualizar desde una base de datos
Scaffold-DbContext "Server=<servidor>;Database=<baseDatos>;User ID=<usuario>;Password=<contraseña>;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir <carpetaDesinoModels>

ejemplo
Scaffold-DbContext "Server=.\SQLExpress;Database=SchoolDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models