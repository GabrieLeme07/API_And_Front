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

CREATE TABLE [LoteEntregas] (
    [Id] uniqueidentifier NOT NULL,
    [DescricaoLoteArquivo] varchar(50) NOT NULL,
    [DataImportacao] datetime NOT NULL,
    [NumeroRegistros] int NOT NULL,
    [NumeroTotalProdutos] int NOT NULL,
    [ValorTotalImportado] numeric(10,2) NOT NULL,
    [DataEntregaMenor] date NOT NULL,
    CONSTRAINT [PK_LoteEntregas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ExcelDatas] (
    [Id] uniqueidentifier NOT NULL,
    [IdLoteEntrega] uniqueidentifier NOT NULL,
    [DataEntrega] date NOT NULL,
    [DescricaoProduto] varchar(50) NOT NULL,
    [ValorUnitario] numeric(10,2) NOT NULL,
    [QtdProduto] int NOT NULL,
    CONSTRAINT [PK_ExcelDatas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExcelDatas_LoteEntregas_IdLoteEntrega] FOREIGN KEY ([IdLoteEntrega]) REFERENCES [LoteEntregas] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ExcelDatas_IdLoteEntrega] ON [ExcelDatas] ([IdLoteEntrega]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201204222301_inicial', N'5.0.0');
GO

COMMIT;
GO

