CREATE TABLE [dbo].[FlashcardsPrev] (
    [Learned]       BIT            NOT NULL,
    [ImportantPrev] NVARCHAR (50)  NOT NULL,
    [English]       NVARCHAR (50)  NOT NULL,
    [Maori]         NVARCHAR (150) NOT NULL,
    [Sequence]      FLOAT (53)     NOT NULL,
    [PowerAppsId]   NVARCHAR (50)  NOT NULL,
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Important]     BIT            CONSTRAINT [DF_Flashcards1_Important] DEFAULT ((1)) NOT NULL
);

