CREATE TABLE [dbo].[Flashcard] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Learned]   BIT            NOT NULL,
    [Important] TINYINT        NOT NULL,
    [English]   NVARCHAR (50)  NOT NULL,
    [Maori]     NVARCHAR (150) NOT NULL,
    [Sequence]  INT            NULL,
    [Tag]       NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Flashcards_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

