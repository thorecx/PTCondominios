IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Estados] (
    [IdEstado] int NOT NULL IDENTITY,
    [Desc] nvarchar(50) NULL,
    CONSTRAINT [PK_Estados] PRIMARY KEY ([IdEstado])
);
GO

CREATE TABLE [Clientes] (
    [IdCliente] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NULL,
    [Apellido] nvarchar(50) NULL,
    [Cedula] nvarchar(13) NULL,
    [Direccion] nvarchar(100) NULL,
    [Telefono] nvarchar(12) NULL,
    [IdEstado] int NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([IdCliente]),
    CONSTRAINT [FK_Clientes_Estados_IdEstado] FOREIGN KEY ([IdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Residenciales] (
    [IdResidencial] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NULL,
    [Direccion] nvarchar(100) NULL,
    [Precio] float NOT NULL,
    [Descripcion] nvarchar(200) NULL,
    [IdEstado] int NULL,
    CONSTRAINT [PK_Residenciales] PRIMARY KEY ([IdResidencial]),
    CONSTRAINT [FK_Residenciales_Estados_IdEstado] FOREIGN KEY ([IdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Pagos] (
    [IdPago] int NOT NULL IDENTITY,
    [Monto] int NOT NULL,
    [IdCliente] int NULL,
    [IdEstado] int NULL,
    CONSTRAINT [PK_Pagos] PRIMARY KEY ([IdPago]),
    CONSTRAINT [FK_Pagos_Clientes_IdCliente] FOREIGN KEY ([IdCliente]) REFERENCES [Clientes] ([IdCliente]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Pagos_Estados_IdEstado] FOREIGN KEY ([IdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Cuotas] (
    [IdCuota] int NOT NULL IDENTITY,
    [Monto] int NOT NULL,
    [Cantidad] int NOT NULL,
    [Fecha] datetime2 NOT NULL,
    [Mora] int NOT NULL,
    [IdPago] int NULL,
    [IdEstado] int NULL,
    CONSTRAINT [PK_Cuotas] PRIMARY KEY ([IdCuota]),
    CONSTRAINT [FK_Cuotas_Estados_IdEstado] FOREIGN KEY ([IdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Cuotas_Pagos_IdPago] FOREIGN KEY ([IdPago]) REFERENCES [Pagos] ([IdPago]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Clientes_IdEstado] ON [Clientes] ([IdEstado]);
GO

CREATE INDEX [IX_Cuotas_IdEstado] ON [Cuotas] ([IdEstado]);
GO

CREATE INDEX [IX_Cuotas_IdPago] ON [Cuotas] ([IdPago]);
GO

CREATE INDEX [IX_Pagos_IdCliente] ON [Pagos] ([IdCliente]);
GO

CREATE INDEX [IX_Pagos_IdEstado] ON [Pagos] ([IdEstado]);
GO

CREATE INDEX [IX_Residenciales_IdEstado] ON [Residenciales] ([IdEstado]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210825223652_InicialCreate', N'5.0.9');
GO

COMMIT;
GO

