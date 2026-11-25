IF DB_ID(N'RestauranteDB') IS NULL
    CREATE DATABASE RestauranteDB;
GO

USE RestauranteDB;
GO

CREATE TABLE dbo.Usuario
(
    IdUsuario INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Usuario PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL,
    Clave NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Usuario_Activo DEFAULT (1),
    CONSTRAINT UQ_Usuario_NombreUsuario UNIQUE (NombreUsuario)
);
GO

CREATE TABLE dbo.Cliente
(
    IdCliente INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Cliente PRIMARY KEY,
    Documento NVARCHAR(20) NOT NULL,
    Nombres NVARCHAR(80) NOT NULL,
    Apellidos NVARCHAR(80) NOT NULL,
    Telefono NVARCHAR(20) NULL,
    Correo NVARCHAR(100) NULL,
    FechaRegistro DATETIME2 NOT NULL CONSTRAINT DF_Cliente_FechaRegistro DEFAULT (SYSDATETIME()),
    Activo BIT NOT NULL CONSTRAINT DF_Cliente_Activo DEFAULT (1),
    CONSTRAINT UQ_Cliente_Documento UNIQUE (Documento)
);
GO

CREATE TABLE dbo.Categoria
(
    IdCategoria INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Categoria PRIMARY KEY,
    Nombre NVARCHAR(60) NOT NULL,
    Descripcion NVARCHAR(200) NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Categoria_Activo DEFAULT (1),
    CONSTRAINT UQ_Categoria_Nombre UNIQUE (Nombre)
);
GO

CREATE TABLE dbo.Producto
(
    IdProducto INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Producto PRIMARY KEY,
    IdCategoria INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Disponible BIT NOT NULL CONSTRAINT DF_Producto_Disponible DEFAULT (1),
    CONSTRAINT CK_Producto_Precio CHECK (Precio >= 0),
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (IdCategoria)
        REFERENCES dbo.Categoria (IdCategoria)
);
GO

CREATE TABLE dbo.Mesa
(
    IdMesa INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Mesa PRIMARY KEY,
    Numero INT NOT NULL,
    Capacidad INT NOT NULL,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_Mesa_Estado DEFAULT (N'Disponible'),
    CONSTRAINT UQ_Mesa_Numero UNIQUE (Numero),
    CONSTRAINT CK_Mesa_Numero CHECK (Numero > 0),
    CONSTRAINT CK_Mesa_Capacidad CHECK (Capacidad > 0),
    CONSTRAINT CK_Mesa_Estado CHECK (Estado IN (N'Disponible', N'Ocupada', N'Reservada'))
);
GO

CREATE TABLE dbo.Pedido
(
    IdPedido INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Pedido PRIMARY KEY,
    IdCliente INT NULL,
    IdMesa INT NULL,
    IdUsuario INT NOT NULL,
    FechaPedido DATETIME2 NOT NULL CONSTRAINT DF_Pedido_FechaPedido DEFAULT (SYSDATETIME()),
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_Pedido_Estado DEFAULT (N'Pendiente'),
    Total DECIMAL(10,2) NOT NULL CONSTRAINT DF_Pedido_Total DEFAULT (0),
    CONSTRAINT CK_Pedido_Total CHECK (Total >= 0),
    CONSTRAINT CK_Pedido_Estado CHECK (Estado IN (N'Pendiente', N'Preparando', N'Listo', N'Entregado', N'Cancelado')),
    CONSTRAINT FK_Pedido_Cliente FOREIGN KEY (IdCliente)
        REFERENCES dbo.Cliente (IdCliente) ON DELETE SET NULL,
    CONSTRAINT FK_Pedido_Mesa FOREIGN KEY (IdMesa)
        REFERENCES dbo.Mesa (IdMesa) ON DELETE SET NULL,
    CONSTRAINT FK_Pedido_Usuario FOREIGN KEY (IdUsuario)
        REFERENCES dbo.Usuario (IdUsuario)
);
GO

CREATE TABLE dbo.DetallePedido
(
    IdDetallePedido INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DetallePedido PRIMARY KEY,
    IdPedido INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT CK_DetallePedido_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_DetallePedido_PrecioUnitario CHECK (PrecioUnitario >= 0),
    CONSTRAINT UQ_DetallePedido_Pedido_Producto UNIQUE (IdPedido, IdProducto),
    CONSTRAINT FK_DetallePedido_Pedido FOREIGN KEY (IdPedido)
        REFERENCES dbo.Pedido (IdPedido) ON DELETE CASCADE,
    CONSTRAINT FK_DetallePedido_Producto FOREIGN KEY (IdProducto)
        REFERENCES dbo.Producto (IdProducto)
);
GO

CREATE TABLE dbo.Pago
(
    IdPago INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Pago PRIMARY KEY,
    IdPedido INT NOT NULL,
    Monto DECIMAL(10,2) NOT NULL,
    FechaPago DATETIME2 NOT NULL CONSTRAINT DF_Pago_FechaPago DEFAULT (SYSDATETIME()),
    MetodoPago NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Pago_Monto CHECK (Monto > 0),
    CONSTRAINT FK_Pago_Pedido FOREIGN KEY (IdPedido)
        REFERENCES dbo.Pedido (IdPedido)
);
GO
