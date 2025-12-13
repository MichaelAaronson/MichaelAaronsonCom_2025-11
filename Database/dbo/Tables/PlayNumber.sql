CREATE TABLE [dbo].[PlayNumber] (
    [Id]    INT        IDENTITY (1, 1) NOT NULL,
    [Name]  NCHAR (10) NOT NULL,
    [Value] INT        NOT NULL,
    CONSTRAINT [PK_PlayNumbers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

