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

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Pagos]') AND [c].[name] = N'Monto');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Pagos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Pagos] ALTER COLUMN [Monto] float NOT NULL;
GO

ALTER TABLE [Pagos] ADD [FechaVencimiento] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cuotas]') AND [c].[name] = N'Mora');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Cuotas] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Cuotas] ALTER COLUMN [Mora] float NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cuotas]') AND [c].[name] = N'Monto');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Cuotas] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Cuotas] ALTER COLUMN [Monto] float NOT NULL;
GO

ALTER TABLE [Cuotas] ADD [MontoTotal] float NOT NULL DEFAULT 0.0E0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210826021831_V2', N'5.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Clientes] DROP CONSTRAINT [FK_Clientes_Estados_IdEstado];
GO

ALTER TABLE [Cuotas] DROP CONSTRAINT [FK_Cuotas_Estados_IdEstado];
GO

ALTER TABLE [Cuotas] DROP CONSTRAINT [FK_Cuotas_Pagos_IdPago];
GO

ALTER TABLE [Pagos] DROP CONSTRAINT [FK_Pagos_Clientes_IdCliente];
GO

ALTER TABLE [Pagos] DROP CONSTRAINT [FK_Pagos_Estados_IdEstado];
GO

ALTER TABLE [Residenciales] DROP CONSTRAINT [FK_Residenciales_Estados_IdEstado];
GO

DROP INDEX [IX_Residenciales_IdEstado] ON [Residenciales];
GO

DROP INDEX [IX_Pagos_IdCliente] ON [Pagos];
GO

DROP INDEX [IX_Pagos_IdEstado] ON [Pagos];
GO

DROP INDEX [IX_Cuotas_IdEstado] ON [Cuotas];
GO

DROP INDEX [IX_Cuotas_IdPago] ON [Cuotas];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Residenciales]') AND [c].[name] = N'IdEstado');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Residenciales] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Residenciales] ALTER COLUMN [IdEstado] int NOT NULL;
ALTER TABLE [Residenciales] ADD DEFAULT 0 FOR [IdEstado];
GO

ALTER TABLE [Residenciales] ADD [EstadoIdEstado] int NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Pagos]') AND [c].[name] = N'IdEstado');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Pagos] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Pagos] ALTER COLUMN [IdEstado] int NOT NULL;
ALTER TABLE [Pagos] ADD DEFAULT 0 FOR [IdEstado];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Pagos]') AND [c].[name] = N'IdCliente');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Pagos] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Pagos] ALTER COLUMN [IdCliente] int NOT NULL;
ALTER TABLE [Pagos] ADD DEFAULT 0 FOR [IdCliente];
GO

ALTER TABLE [Pagos] ADD [ClienteIdCliente] int NULL;
GO

ALTER TABLE [Pagos] ADD [EstadoIdEstado] int NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cuotas]') AND [c].[name] = N'IdPago');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Cuotas] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Cuotas] ALTER COLUMN [IdPago] int NOT NULL;
ALTER TABLE [Cuotas] ADD DEFAULT 0 FOR [IdPago];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cuotas]') AND [c].[name] = N'IdEstado');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Cuotas] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Cuotas] ALTER COLUMN [IdEstado] int NOT NULL;
ALTER TABLE [Cuotas] ADD DEFAULT 0 FOR [IdEstado];
GO

ALTER TABLE [Cuotas] ADD [EstadoIdEstado] int NULL;
GO

ALTER TABLE [Cuotas] ADD [PagoIdPago] int NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Nombre');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Clientes] ALTER COLUMN [Nombre] nvarchar(max) NULL;
GO

DROP INDEX [IX_Clientes_IdEstado] ON [Clientes];
DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'IdEstado');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Clientes] ALTER COLUMN [IdEstado] int NOT NULL;
ALTER TABLE [Clientes] ADD DEFAULT 0 FOR [IdEstado];
CREATE INDEX [IX_Clientes_IdEstado] ON [Clientes] ([IdEstado]);
GO

CREATE INDEX [IX_Residenciales_EstadoIdEstado] ON [Residenciales] ([EstadoIdEstado]);
GO

CREATE INDEX [IX_Pagos_ClienteIdCliente] ON [Pagos] ([ClienteIdCliente]);
GO

CREATE INDEX [IX_Pagos_EstadoIdEstado] ON [Pagos] ([EstadoIdEstado]);
GO

CREATE INDEX [IX_Cuotas_EstadoIdEstado] ON [Cuotas] ([EstadoIdEstado]);
GO

CREATE INDEX [IX_Cuotas_PagoIdPago] ON [Cuotas] ([PagoIdPago]);
GO

ALTER TABLE [Clientes] ADD CONSTRAINT [FK_Clientes_Estados_IdEstado] FOREIGN KEY ([IdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE CASCADE;
GO

ALTER TABLE [Cuotas] ADD CONSTRAINT [FK_Cuotas_Estados_EstadoIdEstado] FOREIGN KEY ([EstadoIdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE NO ACTION;
GO

ALTER TABLE [Cuotas] ADD CONSTRAINT [FK_Cuotas_Pagos_PagoIdPago] FOREIGN KEY ([PagoIdPago]) REFERENCES [Pagos] ([IdPago]) ON DELETE NO ACTION;
GO

ALTER TABLE [Pagos] ADD CONSTRAINT [FK_Pagos_Clientes_ClienteIdCliente] FOREIGN KEY ([ClienteIdCliente]) REFERENCES [Clientes] ([IdCliente]) ON DELETE NO ACTION;
GO

ALTER TABLE [Pagos] ADD CONSTRAINT [FK_Pagos_Estados_EstadoIdEstado] FOREIGN KEY ([EstadoIdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE NO ACTION;
GO

ALTER TABLE [Residenciales] ADD CONSTRAINT [FK_Residenciales_Estados_EstadoIdEstado] FOREIGN KEY ([EstadoIdEstado]) REFERENCES [Estados] ([IdEstado]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210826031237_V3', N'5.0.9');
GO

COMMIT;
GO

