CREATE TABLE [dbo].[Dish] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [CategoryId] INT            NOT NULL,
    [Sort]       INT            DEFAULT ((0)) NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [Img]        NVARCHAR (50)  NOT NULL,
    [CreateTime] DATETIME2 (0)  DEFAULT (sysdatetime()) NULL,
    [Code]       NVARCHAR (MAX) NULL,
    [State]      INT            DEFAULT ((1)) NULL,
    [UpdateTime] DATETIME2 (0)  DEFAULT (sysdatetime()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

