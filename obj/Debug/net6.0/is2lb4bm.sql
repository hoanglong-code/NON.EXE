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

CREATE TABLE [SuperHeroes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Firstname] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Place] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SuperHeroes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UploadImages] (
    [ImageId] int NOT NULL IDENTITY,
    [origin_url] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UploadImages] PRIMARY KEY ([ImageId])
);
GO

CREATE TABLE [Users] (
    [UserID] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [PassWord] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserID])
);
GO

CREATE TABLE [LOLs] (
    [TeamId] int NOT NULL IDENTITY,
    [Team] nvarchar(max) NOT NULL,
    [Top] nvarchar(max) NOT NULL,
    [Jungle] nvarchar(max) NOT NULL,
    [Mid] nvarchar(max) NOT NULL,
    [Adc] nvarchar(max) NOT NULL,
    [Sp] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_LOLs] PRIMARY KEY ([TeamId])
);
GO

CREATE TABLE [MailRequests] (
    [MailId] int NOT NULL IDENTITY,
    [ToEmail] nvarchar(max) NOT NULL,
    [Subject] nvarchar(max) NOT NULL,
    [Body] nvarchar(max) NOT NULL,
    [Attachments] nvarchar(max) NOT NULL,
    [MailFile] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MailRequests] PRIMARY KEY ([MailId])
);
GO
ALTER TABLE [MailRequests]
ADD [MailFile] nvarchar(max);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221008041659_InitialCreate', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221017004858_VVVVV', N'6.0.10');
GO

COMMIT;
GO

