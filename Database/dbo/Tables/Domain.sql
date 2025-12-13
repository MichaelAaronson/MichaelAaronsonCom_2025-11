CREATE TABLE [dbo].[Domain] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED ([Id] ASC)
);

