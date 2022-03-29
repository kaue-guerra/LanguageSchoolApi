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

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [NumberClass] nvarchar(max) NOT NULL,
    [Year] datetime2 NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Cpf] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220326150754_InitialCreate', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'Email');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Students] ALTER COLUMN [Email] nvarchar(20) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'Cpf');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Students] ALTER COLUMN [Cpf] nvarchar(11) NOT NULL;
GO

ALTER TABLE [Courses] ADD [StudentId] int NULL;
GO

CREATE TABLE [Matriculates] (
    [Id] int NOT NULL IDENTITY,
    [CpfStudent] nvarchar(max) NOT NULL,
    [NumberClass] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Matriculates] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Courses_StudentId] ON [Courses] ([StudentId]);
GO

ALTER TABLE [Courses] ADD CONSTRAINT [FK_Courses_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220327160324_addMatriculate', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Courses] ADD [Name] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220327161007_addNameCourse', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_Students_StudentId];
GO

DROP INDEX [IX_Courses_StudentId] ON [Courses];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Courses]') AND [c].[name] = N'StudentId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Courses] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Courses] DROP COLUMN [StudentId];
GO

ALTER TABLE [Matriculates] ADD [StudentId] int NULL;
GO

CREATE INDEX [IX_Matriculates_StudentId] ON [Matriculates] ([StudentId]);
GO

ALTER TABLE [Matriculates] ADD CONSTRAINT [FK_Matriculates_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220327162402_addCourseMatriculate', N'6.0.3');
GO

COMMIT;
GO

