CREATE TABLE [dbo].Step (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Priority]   INT            NOT NULL,
    [StartDate]  DATE           NOT NULL,
    [IsComplete] BIT            NOT NULL,
    [Title]      NVARCHAR (200) NOT NULL,
    [Comments]   NVARCHAR (MAX) NULL,
    [ProjectId]  INT            NOT NULL,
    [TimeStamp]  ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Id] ASC)
);

