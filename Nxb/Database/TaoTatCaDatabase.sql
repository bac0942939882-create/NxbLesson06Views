-- SQL Server: tạo database + cấu trúc + dữ liệu mẫu + lịch sử migration.
-- Không xóa database đang có; chạy lại sẽ bỏ qua migration đã áp dụng.
USE [master];
GO
IF DB_ID(N'Nxb') IS NULL
    EXEC(N'CREATE DATABASE [Nxb]');
GO
USE [Nxb];
GO
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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    CREATE TABLE [Banner] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(150) NOT NULL,
        [Image] varchar(150) NOT NULL,
        [Description] nvarchar(1000) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Status] tinyint NOT NULL,
        CONSTRAINT [PK_Banner] PRIMARY KEY ([Id]),
        CONSTRAINT [CK_Banner_Status] CHECK ([Status] IN (0,1))
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    CREATE TABLE [Category] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Status] tinyint NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([Id]),
        CONSTRAINT [CK_Category_Status] CHECK ([Status] IN (0,1))
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    CREATE TABLE [Product] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(150) NOT NULL,
        [Image] varchar(150) NOT NULL,
        [Price] real NOT NULL,
        [SalePrice] real NOT NULL,
        [Status] tinyint NOT NULL,
        [Descriptions] ntext NULL,
        [CategoryId] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
        CONSTRAINT [CK_Product_Price] CHECK ([Price] >= 0 AND [SalePrice] >= 0 AND [SalePrice] <= [Price]),
        CONSTRAINT [CK_Product_Status] CHECK ([Status] IN (0,1)),
        CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedDate', N'Description', N'Image', N'Name', N'Status') AND [object_id] = OBJECT_ID(N'[Banner]'))
        SET IDENTITY_INSERT [Banner] ON;
    EXEC(N'INSERT INTO [Banner] ([Id], [CreatedDate], [Description], [Image], [Name], [Status])
    VALUES (1, ''2026-09-30T08:00:00.0000000'', N''Khám phá sản phẩm mới tại Nxb'', ''demo-1.png'', N''Bộ sưu tập mới'', CAST(1 AS tinyint)),
    (2, ''2026-09-30T08:00:00.0000000'', N''Các mẫu túi và balo dành cho bạn'', ''demo-2.png'', N''Ưu đãi hôm nay'', CAST(1 AS tinyint)),
    (3, ''2026-09-30T08:00:00.0000000'', N''Bật trạng thái để hiển thị trên trang chủ'', ''demo-1.png'', N''Banner đang ẩn'', CAST(0 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedDate', N'Description', N'Image', N'Name', N'Status') AND [object_id] = OBJECT_ID(N'[Banner]'))
        SET IDENTITY_INSERT [Banner] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedDate', N'Name', N'Status') AND [object_id] = OBJECT_ID(N'[Category]'))
        SET IDENTITY_INSERT [Category] ON;
    EXEC(N'INSERT INTO [Category] ([Id], [CreatedDate], [Name], [Status])
    VALUES (1, ''2026-09-30T08:00:00.0000000'', N''Túi xách'', CAST(1 AS tinyint)),
    (2, ''2026-09-30T08:00:00.0000000'', N''Balo'', CAST(1 AS tinyint)),
    (3, ''2026-09-30T08:00:00.0000000'', N''Phụ kiện'', CAST(1 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedDate', N'Name', N'Status') AND [object_id] = OBJECT_ID(N'[Category]'))
        SET IDENTITY_INSERT [Category] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'CreatedDate', N'Descriptions', N'Image', N'Name', N'Price', N'SalePrice', N'Status') AND [object_id] = OBJECT_ID(N'[Product]'))
        SET IDENTITY_INSERT [Product] ON;
    EXEC(N'INSERT INTO [Product] ([Id], [CategoryId], [CreatedDate], [Descriptions], [Image], [Name], [Price], [SalePrice], [Status])
    VALUES (1, 1, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-1.png'', N''Túi xách đen'', CAST(500000 AS real), CAST(450000 AS real), CAST(1 AS tinyint)),
    (2, 1, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-2.png'', N''Túi xách đỏ'', CAST(550000 AS real), CAST(500000 AS real), CAST(1 AS tinyint)),
    (3, 1, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-3.png'', N''Túi xách xanh'', CAST(600000 AS real), CAST(550000 AS real), CAST(1 AS tinyint)),
    (4, 1, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-4.png'', N''Túi xách nâu'', CAST(650000 AS real), CAST(600000 AS real), CAST(1 AS tinyint)),
    (5, 2, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-5.png'', N''Balo đen'', CAST(700000 AS real), CAST(650000 AS real), CAST(1 AS tinyint)),
    (6, 2, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-6.png'', N''Balo xanh'', CAST(750000 AS real), CAST(700000 AS real), CAST(1 AS tinyint)),
    (7, 3, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-7.png'', N''Ví nhỏ'', CAST(800000 AS real), CAST(750000 AS real), CAST(1 AS tinyint)),
    (8, 3, ''2026-09-30T08:00:00.0000000'', N''Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.'', ''demo-8.png'', N''Túi mini'', CAST(850000 AS real), CAST(800000 AS real), CAST(1 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'CreatedDate', N'Descriptions', N'Image', N'Name', N'Price', N'SalePrice', N'Status') AND [object_id] = OBJECT_ID(N'[Product]'))
        SET IDENTITY_INSERT [Product] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    CREATE INDEX [IX_Product_CategoryId] ON [Product] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035114_InitialNxb'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260930035114_InitialNxb', N'8.0.31');
END;
GO

COMMIT;
GO



-- SQL Server: tạo database + cấu trúc + dữ liệu mẫu + lịch sử migration.
-- Không xóa database đang có; chạy lại sẽ bỏ qua migration đã áp dụng.
USE [master];
GO
IF DB_ID(N'StudentManager') IS NULL
    EXEC(N'CREATE DATABASE [StudentManager]');
GO
USE [StudentManager];
GO
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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE TABLE [StdClass] (
        [Id] int NOT NULL IDENTITY,
        [ClassName] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_StdClass] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE TABLE [Subjects] (
        [Id] int NOT NULL IDENTITY,
        [SubjectName] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE TABLE [Student] (
        [Id] int NOT NULL IDENTITY,
        [StudentName] nvarchar(100) NOT NULL,
        [StudentEmail] nvarchar(100) NOT NULL,
        [StudentPhone] nvarchar(50) NOT NULL,
        [StudentAddress] nvarchar(150) NOT NULL,
        [StudentAvatar] nvarchar(100) NOT NULL,
        [StudentBirthday] date NOT NULL,
        [ClassId] int NOT NULL,
        CONSTRAINT [PK_Student] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Student_StdClass_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [StdClass] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE TABLE [Marks] (
        [SubjectId] int NOT NULL,
        [StudentId] int NOT NULL,
        [Score] float NOT NULL,
        CONSTRAINT [PK_Marks] PRIMARY KEY ([SubjectId], [StudentId]),
        CONSTRAINT [CK_Marks_Score] CHECK ([Score] >= 0 AND [Score] <= 10),
        CONSTRAINT [FK_Marks_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Marks_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClassName') AND [object_id] = OBJECT_ID(N'[StdClass]'))
        SET IDENTITY_INSERT [StdClass] ON;
    EXEC(N'INSERT INTO [StdClass] ([Id], [ClassName])
    VALUES (1, N''K24CNT2''),
    (2, N''K24CNT3'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClassName') AND [object_id] = OBJECT_ID(N'[StdClass]'))
        SET IDENTITY_INSERT [StdClass] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'SubjectName') AND [object_id] = OBJECT_ID(N'[Subjects]'))
        SET IDENTITY_INSERT [Subjects] ON;
    EXEC(N'INSERT INTO [Subjects] ([Id], [SubjectName])
    VALUES (1, N''Lập trình ASP.NET Core''),
    (2, N''Cơ sở dữ liệu''),
    (3, N''Tiếng Anh'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'SubjectName') AND [object_id] = OBJECT_ID(N'[Subjects]'))
        SET IDENTITY_INSERT [Subjects] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClassId', N'StudentAddress', N'StudentAvatar', N'StudentBirthday', N'StudentEmail', N'StudentName', N'StudentPhone') AND [object_id] = OBJECT_ID(N'[Student]'))
        SET IDENTITY_INSERT [Student] ON;
    EXEC(N'INSERT INTO [Student] ([Id], [ClassId], [StudentAddress], [StudentAvatar], [StudentBirthday], [StudentEmail], [StudentName], [StudentPhone])
    VALUES (1, 1, N''Hà Nội'', N''demo.png'', ''2006-03-10'', N''an@example.com'', N''Nguyễn An'', N''0900000001''),
    (2, 1, N''Hải Phòng'', N''demo.png'', ''2006-05-20'', N''binh@example.com'', N''Trần Bình'', N''0900000002''),
    (3, 2, N''Đà Nẵng'', N''demo.png'', ''2006-08-15'', N''chi@example.com'', N''Lê Chi'', N''0900000003''),
    (4, 2, N''Hà Nội'', N''demo.png'', ''2006-11-02'', N''dung@example.com'', N''Phạm Dũng'', N''0900000004'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClassId', N'StudentAddress', N'StudentAvatar', N'StudentBirthday', N'StudentEmail', N'StudentName', N'StudentPhone') AND [object_id] = OBJECT_ID(N'[Student]'))
        SET IDENTITY_INSERT [Student] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StudentId', N'SubjectId', N'Score') AND [object_id] = OBJECT_ID(N'[Marks]'))
        SET IDENTITY_INSERT [Marks] ON;
    EXEC(N'INSERT INTO [Marks] ([StudentId], [SubjectId], [Score])
    VALUES (1, 1, 8.5E0),
    (2, 1, 7.5E0),
    (1, 2, 9.0E0),
    (3, 2, 8.0E0),
    (2, 3, 8.0E0),
    (4, 3, 7.0E0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StudentId', N'SubjectId', N'Score') AND [object_id] = OBJECT_ID(N'[Marks]'))
        SET IDENTITY_INSERT [Marks] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE INDEX [IX_Marks_StudentId] ON [Marks] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE INDEX [IX_Student_ClassId] ON [Student] ([ClassId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Student_StudentEmail] ON [Student] ([StudentEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Student_StudentPhone] ON [Student] ([StudentPhone]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Subjects_SubjectName] ON [Subjects] ([SubjectName]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260930035120_InitialStudentManager'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260930035120_InitialStudentManager', N'8.0.31');
END;
GO

COMMIT;
GO

