CREATE TABLE [dbo].[Project] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (50) NOT NULL,
    [DomainId] INT           NOT NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC)
);

