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

