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

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BankAccounts] (
    [Id] int NOT NULL IDENTITY,
    [AccountNumber] nvarchar(max) NOT NULL,
    [Balance] decimal(18,2) NOT NULL,
    [customerId] int NOT NULL,
    CONSTRAINT [PK_BankAccounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BankAccounts_Customers_customerId] FOREIGN KEY ([customerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transactions] (
    [Id] int NOT NULL IDENTITY,
    [Amount] decimal(18,2) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [BankAccountId] int NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_BankAccounts_BankAccountId] FOREIGN KEY ([BankAccountId]) REFERENCES [BankAccounts] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BankAccounts_customerId] ON [BankAccounts] ([customerId]);
GO

CREATE INDEX [IX_Transactions_BankAccountId] ON [Transactions] ([BankAccountId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260514145839_InitialCreate', N'8.0.27');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Transactions] ADD [Type_New] int NOT NULL DEFAULT 0;
GO


        UPDATE Transactions
        SET Type_New =
            CASE Type
                WHEN 'Deposit' THEN 1
                WHEN 'Withdraw' THEN 2
                WHEN 'Transfer' THEN 3
                ELSE 0
            END
    
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transactions]') AND [c].[name] = N'Type');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Transactions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Transactions] DROP COLUMN [Type];
GO

EXEC sp_rename N'[Transactions].[Type_New]', N'Type', N'COLUMN';
GO

ALTER TABLE [Customers] ADD [Address] nvarchar(250) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Customers] ADD [PhoneNumber] nvarchar(10) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Customers] ADD [ZipCode] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [BankAccounts] ADD [AccountType] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260515180507_UpdatedCustomer_Transaction_BankAccountFields', N'8.0.27');
GO

COMMIT;
GO

