CREATE TABLE [Transactions] (
    [TransactionId] BIGINT NOT NULL IDENTITY,
    [DateTime] DATETIME2 NOT NULL,
    [AccountId] BIGINT NOT NULL,
    [Description] NVARCHAR(255) NOT NULL,
    [TransactionType] NVARCHAR(7) NOT NULL,
    [Amount] DECIMAL(8, 2) NOT NULL,
    [TransferId] BIGINT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_Transactions_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transactions_Transfers_TransferId] FOREIGN KEY ([TransferId]) REFERENCES [Transfers] ([TransferId]) ON DELETE NO ACTION
);
GO
CREATE INDEX [IX_Transactions_AccountId] ON [Transactions] ([AccountId]);
GO

CREATE INDEX [IX_Transactions_DateTime] ON [dbo].[Transactions] ([DateTime] DESC)
GO