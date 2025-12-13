CREATE TABLE [dbo].[Goal] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Goal] PRIMARY KEY CLUSTERED ([Id] ASC)
);

