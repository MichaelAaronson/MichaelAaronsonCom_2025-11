CREATE TABLE [dbo].[FlashcardTopic] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FlashcardTopic] PRIMARY KEY CLUSTERED ([Id] ASC)
);

