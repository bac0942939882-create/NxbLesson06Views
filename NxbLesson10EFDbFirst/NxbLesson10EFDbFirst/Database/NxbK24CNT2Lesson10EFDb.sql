-- Chạy toàn bộ trong SQL Server Management Studio (SSMS).
IF DB_ID(N'NxbK24CNT2Lesson10EFDb') IS NULL
    CREATE DATABASE [NxbK24CNT2Lesson10EFDb];
GO
USE [NxbK24CNT2Lesson10EFDb];
GO

IF OBJECT_ID(N'dbo.NxbMember', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[NxbMember]
    (
        [Id] BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_NxbMember] PRIMARY KEY,
        [NxbUserName] VARCHAR(20) NULL,
        [NxbPassword] VARCHAR(50) NULL,
        [NxbFullName] NVARCHAR(50) NULL,
        [NxbEmail] VARCHAR(50) NULL,
        [NxbPhone] CHAR(12) NULL,
        [NxbStatus] BIT NULL
    );
END;
GO

-- Một bản ghi để xem ngay trên trang Danh sách thành viên.
IF NOT EXISTS (SELECT 1 FROM [dbo].[NxbMember] WHERE [NxbUserName] = 'nxb')
BEGIN
    INSERT INTO [dbo].[NxbMember]
        ([NxbUserName], [NxbPassword], [NxbFullName], [NxbEmail], [NxbPhone], [NxbStatus])
    VALUES
        ('nxb', NULL, N'Nguyễn Xuân Bắc', 'bac0942939882@gmail.com', '0336924130', 1);
END;
GO
