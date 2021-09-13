CREATE TABLE [Accounts] (
    [AccountId] BIGINT NOT NULL IDENTITY,
    [Number] BIGINT NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Accounts_Number]
    ON [dbo].[Accounts]([Number] ASC);
