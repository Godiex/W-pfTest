-- Crear Base de Datos
CREATE DATABASE Employees;
GO

-- Usar la Base de Datos
USE Employees;
GO

-- Tabla Users
CREATE TABLE [dbo].[Users] (
    [Identification] NVARCHAR(50) NOT NULL PRIMARY KEY,
    [FullName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL UNIQUE,
    [Phone] NVARCHAR(20) NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE()
    );
GO

-- Tabla Areas
CREATE TABLE [dbo].[Areas] (
    [Identification] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL UNIQUE
    );
GO

-- Tabla UserAreas
CREATE TABLE [dbo].[UserAreas] (
    [UserId] NVARCHAR(50) NOT NULL,
    [AreaId] INT NOT NULL,
    PRIMARY KEY ([UserId], [AreaId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Identification]),
    FOREIGN KEY ([AreaId]) REFERENCES [dbo].[Areas] ([Identification])
    );
GO

-- Insertar Datos en Areas
INSERT INTO [dbo].[Areas] ([Name])
VALUES 
    ('Facturación'),
    ('IT'),
    ('Logística'),
    ('Marketing'),
    ('Nomina'),
    ('Operaciones'),
    ('Recursos Humanos'),
    ('Servicio al Cliente'),
    ('Soporte Técnico'),
    ('Ventas');
GO
