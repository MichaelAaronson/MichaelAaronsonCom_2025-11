CREATE TABLE [dbo].[Flashcards.2025-04] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Sequence]    FLOAT (53)     NOT NULL,
    [Important]   BIT            NOT NULL,
    [Learned]     BIT            NOT NULL,
    [English]     NVARCHAR (50)  NOT NULL,
    [Maori]       NVARCHAR (150) NOT NULL,
    [PowerAppsId] NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Flashcards] PRIMARY KEY CLUSTERED ([Id] ASC)
);

