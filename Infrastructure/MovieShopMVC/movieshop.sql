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

CREATE TABLE [Movies] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(512) NOT NULL,
    [Overview] nvarchar(max) NOT NULL,
    [Tagline] nvarchar(512) NOT NULL,
    [Budget] decimal(18,4) NULL DEFAULT 9.9,
    [Revenue] decimal(18,4) NULL DEFAULT 9.9,
    [ImdbUrl] nvarchar(2084) NOT NULL,
    [TmdbUrl] nvarchar(2084) NOT NULL,
    [PosterUrl] nvarchar(2084) NOT NULL,
    [BackdropUrl] nvarchar(2084) NOT NULL,
    [OriginalLanguage] nvarchar(64) NOT NULL,
    [ReleaseDate] datetime2 NULL,
    [RunTime] int NULL,
    [Price] decimal(5,2) NULL DEFAULT 9.9,
    [CreatedDate] datetime2 NULL DEFAULT (getdate()),
    [UpdateDate] datetime2 NULL,
    [UpdatedBy] nvarchar(512) NULL,
    [CreatedBy] nvarchar(512) NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Movies_Budget] ON [Movies] ([Budget]);
GO

CREATE INDEX [IX_Movies_Price] ON [Movies] ([Price]);
GO

CREATE INDEX [IX_Movies_Revenue] ON [Movies] ([Revenue]);
GO

CREATE INDEX [IX_Movies_Title] ON [Movies] ([Title]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825075843_InitialMigration', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Movies].[UpdateDate]', N'UpdatedDate', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825082544_ChangingMovieTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(24) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825084915_CreatingGenreTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieGenres] (
    [MovieId] int NOT NULL,
    [GenreId] int NOT NULL,
    CONSTRAINT [PK_MovieGenres] PRIMARY KEY ([MovieId], [GenreId]),
    CONSTRAINT [FK_MovieGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieGenres_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieGenres_GenreId] ON [MovieGenres] ([GenreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825085611_CreatingMovieGenre', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Trailers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(2084) NOT NULL,
    [TrailerUrl] nvarchar(2084) NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_Trailers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Trailers_MovieId] ON [Trailers] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825090135_CreatingTrailTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Casts] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(128) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [ProfilePath] nvarchar(2084) NOT NULL,
    [TmdbUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Casts] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825093303_CreatingCastTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieCasts] (
    [MovieId] int NOT NULL,
    [CastId] int NOT NULL,
    [Character] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MovieCasts] PRIMARY KEY ([MovieId], [CastId]),
    CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieCasts_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieCasts_CastId] ON [MovieCasts] ([CastId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825093840_CreatingMovieCastTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieCasts]') AND [c].[name] = N'Character');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [MovieCasts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [MovieCasts] ALTER COLUMN [Character] nvarchar(450) NOT NULL;
GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Email] nvarchar(256) NOT NULL,
    [HashedPassword] nvarchar(1024) NOT NULL,
    [PhoneNumber] nvarchar(16) NOT NULL,
    [Salt] nvarchar(1024) NOT NULL,
    [IsLocked] bit NOT NULL,
    [ProfilePictureUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825205136_CreatingUserTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'DateOfBirth');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [DateOfBirth] datetime2 NULL;
GO

CREATE TABLE [Favorites] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Favorites] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Favorites_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Favorites_UserId] ON [Favorites] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825205635_CreatingFavoriteTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Reviews] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [Rating] decimal(18,2) NOT NULL,
    [ReviewText] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825210021_CreatingViewsTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'Rating');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [Rating] decimal(3,2) NOT NULL;
ALTER TABLE [Reviews] ADD DEFAULT 9.9 FOR [Rating];
GO

DROP INDEX [IX_Movies_Title] ON [Movies];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(256) NOT NULL;
CREATE INDEX [IX_Movies_Title] ON [Movies] ([Title]);
GO

CREATE TABLE [Purchases] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [PurchaseDateTime] datetime2 NOT NULL,
    [PurchaseNumber] uniqueidentifier NOT NULL,
    [TotalPrice] decimal(5,2) NOT NULL DEFAULT 9.9,
    CONSTRAINT [PK_Purchases] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Purchases_Genres_UserId] FOREIGN KEY ([UserId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Purchases_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Purchases_UserId] ON [Purchases] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825211508_CreatingPurchaseTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRoles] (
    [RoleId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Movies_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825212626_CreatingRolesTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220828012447_UdaptaingCastTable', N'6.0.8');
GO

COMMIT;
GO

