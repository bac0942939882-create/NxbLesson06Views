-- NxbLesson14: cấu trúc SQL Server sinh trực tiếp từ model Entity Framework Core.
-- CSDL được ứng dụng tự tạo ở lần chạy đầu; file này dùng để xem hoặc tạo thủ công.
-- Chỉ chạy phần CREATE TABLE khi CSDL chưa có các bảng này.
IF DB_ID(N'NxbLesson14Db') IS NULL CREATE DATABASE [NxbLesson14Db];
GO
USE [NxbLesson14Db];
GO
CREATE TABLE [Banner] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Status] tinyint NOT NULL DEFAULT CAST(1 AS tinyint),
    [Image] varchar(100) NULL,
    [Description] nvarchar(350) NULL,
    [Prioty] int NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Banner] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Blog] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Status] tinyint NOT NULL DEFAULT CAST(1 AS tinyint),
    [Image] varchar(100) NULL,
    [Description] nvarchar(350) NULL,
    [CreatedDate] date NOT NULL DEFAULT (CONVERT(date, GETDATE())),
    CONSTRAINT [PK_Blog] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Status] tinyint NOT NULL DEFAULT CAST(1 AS tinyint),
    [Image] varchar(100) NULL,
    [Description] nvarchar(350) NULL,
    [CreatedDate] date NOT NULL DEFAULT (CONVERT(date, GETDATE())),
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Status] tinyint NOT NULL DEFAULT CAST(1 AS tinyint),
    [Image] varchar(100) NULL,
    [Description] nvarchar(350) NULL,
    [CreatedDate] date NOT NULL DEFAULT (CONVERT(date, GETDATE())),
    [Price] float NOT NULL,
    [salePrice] float NOT NULL DEFAULT 0.0E0,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE NO ACTION
);
GO


CREATE UNIQUE INDEX [IX_Banner_Name] ON [Banner] ([Name]);
GO


CREATE UNIQUE INDEX [IX_Blog_Name] ON [Blog] ([Name]);
GO


CREATE UNIQUE INDEX [IX_Category_Name] ON [Category] ([Name]);
GO


CREATE INDEX [IX_Product_CategoryId] ON [Product] ([CategoryId]);
GO


CREATE UNIQUE INDEX [IX_Product_Name] ON [Product] ([Name]);
GO


