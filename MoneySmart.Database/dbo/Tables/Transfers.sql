CREATE TABLE [dbo].[Transfers]
(
	[TransferId] BIGINT NOT NULL IDENTITY, 
    [Notes] VARCHAR(4096) NULL,
    CONSTRAINT [PK_Transfers] PRIMARY KEY ([TransferId])
)
