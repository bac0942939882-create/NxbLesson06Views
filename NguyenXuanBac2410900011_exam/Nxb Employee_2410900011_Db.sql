USE [master];
GO
IF DB_ID(N'NxbEmployee_2410900011_Db') IS NULL
    CREATE DATABASE [NxbEmployee_2410900011_Db];
GO
USE [NxbEmployee_2410900011_Db];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF OBJECT_ID(N'dbo.NxbEmployee', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NxbEmployee
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_NxbEmployee PRIMARY KEY,
        NxbName NVARCHAR(100) NOT NULL,
        NxbGender NVARCHAR(10) NOT NULL,
        NxbBirthDay DATE NOT NULL,
        NxbEmail NVARCHAR(254) NOT NULL,
        NxbPhone NVARCHAR(20) NOT NULL,
        NxbActive BIT NOT NULL CONSTRAINT DF_NxbEmployee_Active DEFAULT (1),
        CONSTRAINT CK_NxbEmployee_Gender CHECK (NxbGender IN (N'Nam', N'Nữ', N'Khác'))
    );
    INSERT INTO dbo.NxbEmployee (NxbName, NxbGender, NxbBirthDay, NxbEmail, NxbPhone, NxbActive)
    VALUES (N'Nhân viên minh họa', N'Nam', '19950101', N'employee@example.com', N'0900000099', 1);
END;

IF OBJECT_ID(N'dbo.NxbStudent', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NxbStudent
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_NxbStudent PRIMARY KEY,
        NxbName NVARCHAR(100) NOT NULL,
        NxbGender NVARCHAR(10) NOT NULL,
        NxbBirthDay DATE NOT NULL,
        NxbEmail NVARCHAR(254) NOT NULL,
        NxbPhone NVARCHAR(20) NOT NULL,
        NxbActive BIT NOT NULL CONSTRAINT DF_NxbStudent_Active DEFAULT (1),
        CONSTRAINT CK_NxbStudent_Gender CHECK (NxbGender IN (N'Nam', N'Nữ', N'Khác'))
    );
    INSERT INTO dbo.NxbStudent (NxbName, NxbGender, NxbBirthDay, NxbEmail, NxbPhone, NxbActive)
    VALUES
        (N'Trần Minh Anh', N'Nữ', '20050315', N'minhanh@example.com', N'0900000001', 1),
        (N'Lê Hoàng Nam', N'Nam', '20040822', N'hoangnam@example.com', N'0900000002', 1),
        (N'Phạm Thu Hà', N'Nữ', '20051106', N'thuha@example.com', N'0900000003', 1),
        (N'Đỗ Hải Đăng', N'Nam', '20040118', N'haidang@example.com', N'0900000004', 0),
        (N'Vũ Ngọc Linh', N'Khác', '20050609', N'ngoclinh@example.com', N'0900000005', 1),
        (N'Bùi Quang Huy', N'Nam', '20041202', N'quanghuy@example.com', N'0900000006', 0);
END;

COMMIT TRANSACTION;
GO
SELECT N'NxbEmployee' AS TenBang, COUNT(*) AS SoBanGhi FROM dbo.NxbEmployee
UNION ALL
SELECT N'NxbStudent', COUNT(*) FROM dbo.NxbStudent;
GO
